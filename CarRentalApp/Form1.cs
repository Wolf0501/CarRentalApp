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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string customerName = tbCustomerName.Text;
            string dateOut = dtpDateRented.Value.ToString();
            string dateIn = dtpDateReturned.Value.ToString();
            var carType = cbTypeOfCar.SelectedItem.ToString();

            //THIS IS MY ATTEMPT
            //MessageBox.Show($"Thank you for renting: {tbCustomerName.Text}" +
            //    $"\n Your pickup date for the car will be: {dtpDateRented.Text}" +
            //    $"\n The return date for your car will be: {dtpDateReturned.Text}" +
            //    $"\n The type of car you rented is: {cbTypeOfCar.Text}");

            MessageBox.Show($"Customer Name: {customerName}\n\r" +
                $"Dare Rented: {dateOut}\n\r" +
                $"Date Returned: {dateIn}\n\r" +
                $"Car Type: {carType}\n\r\n\r" +
                $"THANK YOU");
        }
    }
}
