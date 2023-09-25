using BusinessObjects.Entities;
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
    /// Interaction logic for CarCreate.xaml
    /// </summary>
    public partial class CarCreate : Window
    {
        private readonly IService service;
        public CarCreate(IService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            string errorMessages = string.Empty;

            string carName = txtCarName.Text;   
            string description = txtDecription.Text;
            string seatingCapacity = txtSeatingCapacity.Text;
            string numberOfDoors = txtNumberOfDoors.Text;
            string fuelType = cbFuelType.Text;
            string year = txtYear.Text;
            string carRentingPrice = txtRetingPricePerDay.Text;

            int intCheck = 0;
            decimal decimalCheck;


            if (!int.TryParse(seatingCapacity, out intCheck) || intCheck <= 0 || seatingCapacity is null) errorMessages += "\nseating capacity required or number > 0!!!";
            if (!int.TryParse(numberOfDoors, out intCheck) || intCheck <= 0 || numberOfDoors is null) errorMessages += "\nnumber of doors required or number > 0!!!";
            if (!int.TryParse(year, out intCheck) || intCheck < 1900 || intCheck >= 2024 || year is null) errorMessages += "\nyear required or number in (1900 - 2024)!!!";
            if (!decimal.TryParse(year, out decimalCheck) || decimalCheck <= 0 || year is null) errorMessages += "\nrenting price required or number > 0!!!";
            if(errorMessages == "")
            {
                CarInformation car = new CarInformation()
                {
                    CarName = carName,
                    CarDescription = description,
                    SeatingCapacity = Int32.Parse(seatingCapacity),
                    NumberOfDoors = Int32.Parse(numberOfDoors),
                    FuelType = fuelType,
                    Year = Int32.Parse(year),
                    CarRentingPricePerDay = Int32.Parse(carRentingPrice),
                    CarStatus = 1,
                    SupplierId = 4,
                    ManufacturerId = 1
                };

                var result = service.CreateCar(car);

                if (!result.ResponseObject) MessageBox.Show(result.Message);
                else
                {
                    MessageBox.Show("Create successfully!!!");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show(errorMessages);
            }
        }
    }
}
