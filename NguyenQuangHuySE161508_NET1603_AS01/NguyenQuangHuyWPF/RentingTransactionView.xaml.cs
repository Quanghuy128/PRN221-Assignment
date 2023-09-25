using BusinessObjects;
using Microsoft.IdentityModel.Tokens;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NguyenQuangHuyWPF
{
    /// <summary>
    /// Interaction logic for RentingTransactionView.xaml
    /// </summary>
    public partial class RentingTransactionView : Window
    {
        private readonly IService service;
        private readonly AdminAccount adminAccount;
        public RentingTransactionView(IService service, AdminAccount adminAccount)
        {
            InitializeComponent();
            this.service = service;
            this.adminAccount = adminAccount;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            this.LoadItems(DateTime.MinValue, DateTime.MaxValue);
        }

        private void LoadItems(DateTime? startDate, DateTime? endDate)
        {
            var result = service.GetAllRentingTransaction(startDate, endDate);
            if (!result.Message.IsNullOrEmpty()) MessageBox.Show(result.Message);
            else
            {
                dtgRenting.Items.Refresh();
                dtgRenting.ItemsSource = result.ResponseObject;
            }
        }

        private void btnCarView_Click(object sender, RoutedEventArgs e)
        {
            CarManagementView cmv = new CarManagementView(service, adminAccount);
            cmv.Show();
            this.Close();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login(adminAccount, service);
            login.Show();
            this.Close();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = dtpStartDate.SelectedDate ?? DateTime.UtcNow;
            DateTime endDate = dtpEndDate.SelectedDate ?? DateTime.UtcNow;
            bool checkDate = DateTime.Compare(startDate, endDate) < 0;
            if (!dtpStartDate.SelectedDate.HasValue) MessageBox.Show("start date required");
            else if (!dtpEndDate.SelectedDate.HasValue) MessageBox.Show("end date required");
            else
            {
                if (DateTime.Compare(startDate, endDate) > 0)
                {
                    MessageBox.Show("end date needs to be earlier!!!");
                }
                this.LoadItems(startDate, endDate);
            }
        }
    }
}
