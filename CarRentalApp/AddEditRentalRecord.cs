using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class AddEditRentalRecord : Form
    {
        private bool isEditMode;
        private ManageRentalRecords _manageRentalRecords;
        private readonly CarRentalEntities _db;
        public AddEditRentalRecord(ManageRentalRecords manageRentalRecords = null)
        {
            InitializeComponent();
            lblTitle.Text = "Add New Rental Record";
            this.Text = "Add New Rental Record";
            isEditMode = false;
            _db = new CarRentalEntities();
            _manageRentalRecords = manageRentalRecords;
        }

        public AddEditRentalRecord(RentalData recordToEdit, ManageRentalRecords manageRentalRecords = null)
        {
            InitializeComponent();
            lblTitle.Text = "Edit Rental Record";
            this.Text = "Edit Rental Record";
            if (recordToEdit == null)
            {
                MessageBox.Show("Please ensure you have selected a valid record to edit");
                Close();
            }
            else
            {
                isEditMode = true;
                _manageRentalRecords = manageRentalRecords;
                _db = new CarRentalEntities();
                PopulateFields(recordToEdit);
                
            }
        }

        private void PopulateFields(RentalData recordToEdit)
        {
            tbCustomerName.Text = recordToEdit.CustomerName;
            dtpDateRented.Value = (DateTime)recordToEdit.DateRented;
            dtpDateReturned.Value = (DateTime)recordToEdit.DateReturned;
            tbCost.Text = recordToEdit.Cost.ToString();
            lblRentalDataRef.Text = recordToEdit.RentalDataRef.ToString("D");
        }

        private void btnSubmit_Click(object sender, EventArgs e)
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
                    if (isEditMode)
                    {
                        
                        var id = Guid.Parse(lblRentalDataRef.Text);
                        var rentalRecord = _db.RentalDatas.FirstOrDefault(q => q.RentalDataRef == id);
                        rentalRecord.CustomerName = customerName;
                        rentalRecord.DateRented = dateOut;
                        rentalRecord.DateReturned = dateIn;
                        rentalRecord.Cost = (decimal)cost;
                        rentalRecord.CarTypeRef = (Guid)cbTypeOfCar.SelectedValue;

                        MessageBox.Show($"Customer Name: {customerName}\n\r" +
                        $"Date Rented: {dateOut}\n\r" +
                        $"Date Returned: {dateIn}\n\r" +
                        $"Cost: £{cost}\n\r" +
                        $"Car Type: {carType}\n\r\n\r" +
                        $"THANK YOU");
                    }
                    else
                    {
                        var rentalRecord = new RentalData();
                        rentalRecord.RentalDataRef = Guid.NewGuid();
                        rentalRecord.CustomerName = customerName;
                        rentalRecord.DateRented = dateOut;
                        rentalRecord.DateReturned = dateIn;
                        rentalRecord.Cost = (decimal)cost;
                        rentalRecord.CarTypeRef = (Guid)cbTypeOfCar.SelectedValue;

                        _db.RentalDatas.Add(rentalRecord);

                        MessageBox.Show($"Customer Name: {customerName}\n\r" +
                       $"Date Rented: {dateOut}\n\r" +
                       $"Date Returned: {dateIn}\n\r" +
                       $"Cost: £{cost}\n\r" +
                       $"Car Type: {carType}\n\r\n\r" +
                       $"THANK YOU");
                    }
                    _db.SaveChanges();
                    //_manageRentalRecords.PopulateRecordGrid(); //why is this not updating the grid?
                    Close();
                  
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
            var cars = _db.CarTypes
                .Select(q => new { CarTypeRef = q.CarTypeRef, CarName = q.CarMake + " " + q.CarModel })
                .ToList();
            cbTypeOfCar.DisplayMember = "CarName";
            cbTypeOfCar.ValueMember = "CarTypeRef";
            cbTypeOfCar.DataSource = cars;
        }
    }
}
