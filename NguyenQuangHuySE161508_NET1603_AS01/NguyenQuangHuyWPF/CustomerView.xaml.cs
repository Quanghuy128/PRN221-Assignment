using BusinessObjects;
using BusinessObjects.Entities;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.CustomerRepo.Commands.CreateCustomerCommand;
using Repository.CustomerRepo.Commands.DeleteCustomerCommand;
using Repository.CustomerRepo.Commands.UpdateCustomerCommand;
using Repository.CustomerRepo.Models;
using Repository.CustomerRepo.Queries.GetCustomersQuery;
using Repository.CustomerRepo.Queries.LoginQuery;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : Window
    {
        private readonly AdminAccount adminAccount;

        private IService service;


        private CustomerViewModel currentModel;
        public CustomerView(AdminAccount adminAccount, IService service)
        {
            InitializeComponent();
            this.currentModel = new CustomerViewModel();

            this.adminAccount = adminAccount;
            this.service = service;
        }
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            this.LoadItems();
        }

        private void LoadItems()
        {
            var respone = service.GetAllCustomers(txtSearch.Text);
            if (!respone.Message.IsNullOrEmpty()) MessageBox.Show(respone.Message);
            else
            {
                dtgCustomers.Items.Refresh();
                dtgCustomers.ItemsSource = respone.ResponseObject;
            }
        }

        private void btnCarView_Click(object sender, RoutedEventArgs e)
        {
            CarManagementView cmv = new CarManagementView(service, adminAccount);
            cmv.Show();
            this.Close();
        }

        private void dtgCustomers_SelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dtgCustomers.SelectedItem != null)
            currentModel = (CustomerViewModel)dtgCustomers.SelectedItem;

            //clear text
            txtCustomerId.Text = "";
            txtCustomerName.Text = "";
            txtEmail.Text = "";
            txtTelephone.Text = "";
            txtPassword.Text = "";
            dtpCustomerBirthday.SelectedDate = DateTime.UtcNow;
            ckCustomerStatus.IsChecked = false;

            //fill text
            txtCustomerId.Text = currentModel.CustomerId!.ToString();
            txtCustomerName.Text = currentModel!.CustomerName;
            txtEmail.Text = currentModel!.Email;
            txtPassword.Text = currentModel!.Password;
            txtTelephone.Text = currentModel!.Telephone;
            dtpCustomerBirthday.SelectedDate = currentModel!.CustomerBirthday;
            ckCustomerStatus.IsChecked = currentModel!.CustomerStatus == 1;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            CustomerUpdate customerViewUpdate = new CustomerUpdate(adminAccount, service, currentModel);
            customerViewUpdate.ShowDialog();
            this.LoadItems();

        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CustomerCreate customerViewCreate = new CustomerCreate(adminAccount, service);
            customerViewCreate.ShowDialog();
            this.LoadItems();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var result = service.DeleteCustomer(Int32.Parse(txtCustomerId.Text));
                if (!result.Message.IsNullOrEmpty()) MessageBox.Show(result.Message);
                if (!result.ResponseObject) MessageBox.Show("Delete Failed !!!");
                else
                {
                    MessageBox.Show("Delete Successfully!!!");
                    LoadItems();
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            this.LoadItems();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Login loginView = new Login(adminAccount, service);
            loginView.Show();
            this.Close();
        }
    }
}
