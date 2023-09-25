using BusinessObjects;
using BusinessObjects.Entities;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.CustomerRepo.Commands.CreateCustomerCommand;
using Repository.CustomerRepo.Commands.UpdateCustomerCommand;
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
    /// Interaction logic for CustomerUpdate.xaml
    /// </summary>
    public partial class CustomerUpdate : Window
    {
        private AdminAccount adminAccount;
        private readonly IService service;
        private CustomerViewModel updateModel;
        public CustomerUpdate(AdminAccount adminAccount, IService service, CustomerViewModel updateModel)
        {
            InitializeComponent();
            this.adminAccount = adminAccount;
            this.service = service;
            this.updateModel = updateModel;
            Load();
        }

        private void Load()
        {
            txtCustomerName.Text = updateModel.CustomerName;
            txtEmail.Text = updateModel?.Email;
            txtTelephone.Text = updateModel?.Telephone;
            txtPassword.Text = updateModel?.Password;
            dtpCustomerBirthday.SelectedDate = updateModel?.CustomerBirthday;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string name = txtCustomerName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
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
                if (!match.Success) emailValidate = "\nemail need to have format like example@abc.com";
                else if(email.Equals(adminAccount.AdminUsername))
                {
                    emailValidate = "\nemail duplicate with admin email";
                }
            }

            if (telephone.IsNullOrEmpty()) validateString += "|telephone ";
            if (!birthday.HasValue) validateString += "| birthday |";

            if (!emailValidate.IsNullOrEmpty()) MessageBox.Show(emailValidate);
            if (!validateString.IsNullOrEmpty()) MessageBox.Show(validateString + "are empty. \nThese fields are required!!!");
            else if(emailValidate.IsNullOrEmpty())
            {
                Customer model = new Customer()
                {
                    CustomerId = updateModel.CustomerId,
                    CustomerName = name,
                    Password = password,
                    Email = updateModel.Email,
                    Telephone = telephone,
                    CustomerBirthday = birthday,
                    CustomerStatus = 1
                };


                var result = service.UpdateCustomer(model);

                if (!result.ResponseObject) MessageBox.Show(result.Message);
                else
                {
                    MessageBox.Show("Update successfully!!!");
                    this.Close();
                }
            }
        }
    }
}
