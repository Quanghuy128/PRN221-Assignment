using BusinessObjects;
using BusinessObjects.Entities;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.CustomerRepo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CustomerProfile.xaml
    /// </summary>
    public partial class CustomerProfile : Window
    {
        private readonly AdminAccount adminAccount;
        private string username;
        IService service;
        CustomerViewModel currentModel;
        public CustomerProfile(AdminAccount adminAccount, string username, IService service)
        {
            InitializeComponent();
            this.adminAccount = adminAccount;
            this.username = username;
            this.service = service;
        }
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            this.LoadItem();
        }
        private void LoadItem()
        {
            var response = this.service.GetCustomerByEmail(username);
            if (response.Message != "") MessageBox.Show(response.Message);
            else
            {
                currentModel = response.ResponseObject;

                txtCustomerName.Text = currentModel.CustomerName;
                txtEmail.Text = currentModel.Email;
                txtTelephone.Text = currentModel.Telephone;
                dtpCustomerBirthday.SelectedDate = currentModel.CustomerBirthday;
                pwbPassword.Password = currentModel.Password;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            txtCustomerName.IsEnabled = false;
            txtEmail.IsEnabled = false;
            pwbPassword.IsEnabled = false;
            txtTelephone.IsEnabled = false;
            dtpCustomerBirthday.IsEnabled = false;

            btnDone.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            txtCustomerName.IsEnabled = true;
            txtEmail.IsEnabled = true;
            txtTelephone.IsEnabled = true;
            dtpCustomerBirthday.IsEnabled = true;
            pwbPassword.IsEnabled = true;

            btnDone.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login(adminAccount, service);
            login.Show();
            this.Close();
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            string validateString = string.Empty;
            string email = txtEmail.Text;

            if (email.IsNullOrEmpty()) validateString += "| email ";
            else
            {
                //email regrex
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                if (!match.Success) validateString = "\nemail need to have format like example@abc.com";
                else if (email.Equals(adminAccount.AdminUsername))
                {
                    validateString = "\nemail duplicate with admin email";
                }
            }

            if (validateString != "")
            {
                MessageBox.Show(validateString);
            }
            else
            {
                var result = this.service.UpdateCustomer(new Customer
                {
                    CustomerId = currentModel.CustomerId,
                    CustomerName = txtCustomerName.Text,
                    Email = email,
                    Password = pwbPassword.Password,
                    Telephone = txtTelephone.Text,
                    CustomerBirthday = dtpCustomerBirthday.SelectedDate,
                    CustomerStatus = 1
                });
                if (result.Message != "") MessageBox.Show(result.Message);
                else
                {
                    MessageBox.Show("Update Successfully!!!");
                }
            }
        }

        private void btnViewTransaction_Click(object sender, RoutedEventArgs e)
        {
            TrackTransaction ttv = new TrackTransaction(currentModel.CustomerId, service);
            ttv.ShowDialog();
        }
    }
}
