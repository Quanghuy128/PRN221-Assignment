using BusinessObjects;
using BusinessObjects.Entities;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.CarInformationRepo.Models;
using Repository.CarInformationRepo.Queries.GetCarsQuery;
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
    /// Interaction logic for CarManagementView.xaml
    /// </summary>
    public partial class CarManagementView : Window
    {
        private readonly AdminAccount adminAccount;
        CarViewModel currentModel;
        private readonly IService service;
        public CarManagementView(IService service, AdminAccount adminAccount)
        {
            InitializeComponent();
            this.service = service;
            currentModel = new CarViewModel();
            this.adminAccount = adminAccount;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            this.LoadItems();
        }

        private void LoadItems()
        {
            var result = service.GetAllCars(txtSearch.Text);
            if (!result.Message.IsNullOrEmpty()) MessageBox.Show(result.Message);
            else
            {
                dtgCars.Items.Refresh();
                dtgCars.ItemsSource = result.ResponseObject;
            }
        }

        private void dtgCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgCars.SelectedItem != null)
                currentModel = (CarViewModel)dtgCars.SelectedItem;

            txtCarId.Text = "";
            txtCarName.Text = "";
            txtCarDesciption.Text = "";
            txtNumberOfDoors.Text = "";
            txtSeatingCapacity.Text = "";
            txtSupplierName.Text = "";
            txtManufacturerName.Text = "";
            cbFuelType.Text = "";
            txtYear.Text = "";
            txtCarRentingPerDay.Text = "";
            ckCarStatus.IsChecked = false;

            txtCarId.Text = currentModel.CarId.ToString();
            txtCarName.Text = currentModel.CarName;
            txtCarDesciption.Text = currentModel.CarDescription;
            txtNumberOfDoors.Text = currentModel.NumberOfDoors.ToString();
            txtSeatingCapacity.Text = currentModel.SeatingCapacity.ToString();
            txtSupplierName.Text = currentModel.SupplierId.ToString();
            txtManufacturerName.Text = currentModel.ManufacturerId.ToString();
            foreach (ComboBoxItem item in cbFuelType.Items)
            {
                if (currentModel.FuelType.Trim().Equals(item.Content))
                    item.IsSelected = true;
            }
            txtYear.Text = currentModel.Year.ToString();
            txtCarRentingPerDay.Text = currentModel.CarRentingPricePerDay.ToString();
            ckCarStatus.IsChecked = currentModel!.CarStatus == 1;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CarCreate ccv = new CarCreate(service);
            ccv.ShowDialog();
            this.LoadItems();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string errorMessages = string.Empty;

            string carName = txtCarName.Text;
            string description = txtCarDesciption.Text;
            string seatingCapacity = txtSeatingCapacity.Text;
            string numberOfDoors = txtNumberOfDoors.Text;
            string fuelType = cbFuelType.Text;
            string year = txtYear.Text;
            string carRentingPrice = txtCarRentingPerDay.Text;
            bool? active = ckCarStatus.IsChecked;
            string manufacturerId = txtManufacturerName.Text;
            string supplierId = txtSupplierName.Text;

            int intCheck = 0;
            decimal decimalCheck;


            if (!int.TryParse(seatingCapacity, out intCheck) || intCheck <= 0 || seatingCapacity is null) errorMessages += "\nseating capacity required or number > 0!!!";
            if (!int.TryParse(numberOfDoors, out intCheck) || intCheck <= 0 || numberOfDoors is null) errorMessages += "\nnumber of doors required or number > 0!!!";
            if (!int.TryParse(year, out intCheck) || intCheck < 1900 || intCheck >= 2024 || year is null) errorMessages += "\nyear required or number in (1900 - 2024)!!!";
            if (!decimal.TryParse(year, out decimalCheck) || decimalCheck <= 0 || year is null) errorMessages += "\nrenting price required or number > 0!!!";
            if (!int.TryParse(manufacturerId, out intCheck) || intCheck <= 0 || manufacturerId is null) errorMessages += "\nmanufacturerId required or number > 0!!!";
            if (!int.TryParse(supplierId, out intCheck) || intCheck <= 0 || supplierId is null) errorMessages += "\nsupplierId required or number > 0!!!";

            if (errorMessages == "")
            {
                CarInformation car = new CarInformation()
                {
                    CarId = currentModel!.CarId,
                    CarName = carName,
                    CarDescription = description,
                    SeatingCapacity = Int32.Parse(seatingCapacity),
                    NumberOfDoors = Int32.Parse(numberOfDoors),
                    FuelType = fuelType,
                    Year = Int32.Parse(year),
                    CarRentingPricePerDay = Decimal.Parse(carRentingPrice),
                    CarStatus = Convert.ToByte(active),
                    ManufacturerId = Int32.Parse(manufacturerId),
                    SupplierId = Int32.Parse(supplierId),
                };
                var result = this.service.UpdateCar(car);
                if (!result.ResponseObject) MessageBox.Show(result.Message);
                else
                {
                    MessageBox.Show("Update successfully!!!");
                    this.LoadItems();
                }
            }
            else
            {
                MessageBox.Show(errorMessages);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string err = string.Empty;
            string id = txtCarId.Text;
            if (id == "") err = "Cant delete. not found car id !!!";

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var result = this.service.DeleteCar(Int32.Parse(id));
                MessageBox.Show(result.Message);
                if (result.ResponseObject) this.LoadItems();
            }
        }

        private void btnCustomerView_Click(object sender, RoutedEventArgs e)
        {
            CustomerView cv = new CustomerView(adminAccount, service);
            cv.Show();
            this.Close();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login(adminAccount, service);
            login.Show();
            this.Close();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            this.LoadItems();
        }

        private void btnViewManufacturer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRenting_Click(object sender, RoutedEventArgs e)
        {
            RentingTransactionView rtv = new RentingTransactionView(service, adminAccount);
            rtv.Show();
            this.Close();
        }
    }
}
