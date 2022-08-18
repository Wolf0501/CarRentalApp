using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class AddRentalRecord : Form
    {
        private readonly CarRentalEntities carRentalEntities;
        public AddRentalRecord()
        {
            InitializeComponent();
            carRentalEntities = new CarRentalEntities();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string customerName = tbCustomerName.Text;
                var dateOut = dtpDateRented.Value;
                var dateIn = dtpDateReturned.Value;
                double cost = Convert.ToDouble(tbCost.Text);
                var carType = cbTypeOfCar.Text;
                var isValid = true;
                var errorMessage = "";

                if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(carType))
                {
                    isValid = false;
                    errorMessage += "Error: Please enter missing data.\n\r";
                }

                if (dateOut > dateIn)
                {
                    isValid = false;
                    errorMessage += "Error: Illegal date selection.\n\r";
                }

                if (cost == 0)
                {
                    isValid = false;
                    errorMessage += "Error: Cost cannot be 0.\n\r";
                }

                //if(isValid == true) or
                if (isValid)
                {
                    var rentalRecord = new RentalData();
                    rentalRecord.RentalDataRef = Guid.NewGuid();
                    rentalRecord.CustomerName = customerName;
                    rentalRecord.DateReturned = dateOut;
                    rentalRecord.DateReturned = dateIn;
                    rentalRecord.Cost = (decimal)cost;
                    rentalRecord.CarTypeRef = (Guid)cbTypeOfCar.SelectedValue;

                    carRentalEntities.RentalDatas.Add(rentalRecord);
                    carRentalEntities.SaveChanges();

                    MessageBox.Show($"Customer Name: {customerName}\n\r" +
                   $"Dare Rented: {dateOut}\n\r" +
                   $"Date Returned: {dateIn}\n\r" +
                   $"Cost: £{cost}\n\r" +
                   $"Car Type: {carType}\n\r\n\r" +
                   $"THANK YOU");
                }
                else
                {
                    MessageBox.Show(errorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
           
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var cars = carRentalEntities.CarTypes.ToList();
            var cars = carRentalEntities.CarTypes
                .Select(q => new { CarTypeRef = q.CarTypeRef, CarName = q.CarMake + " " + q.CarModel })
                .ToList();
            cbTypeOfCar.DisplayMember = "CarName";
            cbTypeOfCar.ValueMember = "CarTypeRef";
            cbTypeOfCar.DataSource = cars;
        }
    }
}
