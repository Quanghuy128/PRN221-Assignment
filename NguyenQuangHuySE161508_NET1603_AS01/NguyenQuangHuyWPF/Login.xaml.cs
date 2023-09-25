using BusinessObjects;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.CustomerRepo.Commands.CreateCustomerCommand;
using Repository.CustomerRepo.Commands.DeleteCustomerCommand;
using Repository.CustomerRepo.Commands.UpdateCustomerCommand;
using Repository.CustomerRepo.Queries.GetCustomersQuery;
using Repository.CustomerRepo.Queries.LoginQuery;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private IService service;

        private readonly AdminAccount adminAccount;
        public Login(AdminAccount adminAccount, IService service)
        {
            InitializeComponent();
            this.adminAccount = adminAccount;

            this.service = service;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtEmail.Text;
            string password = txtPassword.Password;
            if(username.Equals(adminAccount.AdminUsername) && password.Equals(adminAccount.AdminPassword))
            {
//                InvalidOperationException edititem is not allowed
                try
                {
                    CustomerView customerView = new CustomerView(adminAccount, service);

                    this.Close();
                    customerView.ShowDialog();
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message + "\nPlease try again!!!");
                }
            }
            else
            {
                var response = this.service.GetCustomerByEmail(username);
                if (!response.Message.IsNullOrEmpty())
                    MessageBox.Show(response.Message);
                else if (!response.ResponseObject.Password.Equals(password))
                    MessageBox.Show("Wrong Password!!!");
                else
                {
                    CustomerProfile cpv = new CustomerProfile(adminAccount, username, service);
                    this.Close();
                    cpv.ShowDialog();
                }
            }
        }
    }
}
