using BusinessObjects;
using BusinessObjects.Entities;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.CustomerRepo.Commands.CreateCustomerCommand;
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
using System.Xml.Serialization;

namespace NguyenQuangHuyWPF
{
    /// <summary>
    /// Interaction logic for CustomerCreate.xaml
    /// </summary>
    public partial class CustomerCreate : Window
    {
        private AdminAccount adminAccount;
        private readonly IService service;
        public CustomerCreate(AdminAccount adminAccount, IService service)
        {
            InitializeComponent();
            this.adminAccount = adminAccount;
            this.service = service;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            string name = txtCustomerName.Text;
            string email = txtEmail.Text;
            string password = pwbPassword.Password;
            string telephone = txtTelephone.Text;
            DateTime? birthday = dtpCustomerBirthday.SelectedDate;

            string emailValidate = "";
            string validateString = "";

            if (name.IsNullOrEmpty()) validateString += "| name ";
            if (email.IsNullOrEmpty()) validateString += "| email ";
            else
            {
                //email regrex
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                if (!match.Success) emailValidate = "email need to have format like example@abc.com";
                else if(email.Equals(adminAccount.AdminUsername))
                {
                    emailValidate = "email duplicate with admin email";
                }
            }
            if (telephone.IsNullOrEmpty()) validateString += "|telephone ";
            if (!birthday.HasValue) validateString += "| birthday |";

            if (!emailValidate.IsNullOrEmpty()) MessageBox.Show(emailValidate);
            if (!validateString.IsNullOrEmpty()) MessageBox.Show(validateString + "are empty. \nThese fields are required!!!");
            else if(emailValidate.IsNullOrEmpty())
            {
                if (!password.Equals(pwbPasswordConfirm.Password)) MessageBox.Show("Confirm password is not matched!!!");
                else {
                    Customer model = new Customer()
                    {
                        CustomerName = name,
                        Password = password,
                        Email = email,
                        Telephone = telephone,
                        CustomerBirthday = birthday,
                        CustomerStatus = 1
                    };


                    var result = service.CreateCustomer(model);

                    if (!result.ResponseObject) MessageBox.Show(result.Message);
                    else
                    {
                        MessageBox.Show("Create successfully!!!");
                        this.Close();
                    }
                }
            }
        }
    }
}
