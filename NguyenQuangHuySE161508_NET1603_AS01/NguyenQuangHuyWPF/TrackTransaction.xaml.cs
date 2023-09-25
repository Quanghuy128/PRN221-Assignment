using Repository;
using Repository.CustomerRepo.Models;
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
    /// Interaction logic for TrackTransaction.xaml
    /// </summary>
    public partial class TrackTransaction : Window
    {
        private readonly int id;
        private readonly IService service;
        public TrackTransaction(int id, IService service)
        {
            InitializeComponent();
            this.id = id;
            this.service = service;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            this.LoadItems();
        }
        private void LoadItems()
        {
            var response = this.service.GetAllRentingTransactionCustomer(id);
            if (response.Message != "") dtgTransaction.ItemsSource = response.Message;
            else
            {
                dtgTransaction.ItemsSource = response.ResponseObject;
            }
        }
    }
}
