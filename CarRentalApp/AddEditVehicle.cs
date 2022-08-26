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
    public partial class AddEditVehicle : Form
    {
        private bool isEditMode;
        private ManageVehicleListing _manageVehicleListing;
        private readonly CarRentalEntities _db;
        public AddEditVehicle(ManageVehicleListing manageVehicleListing = null)
        {
            InitializeComponent();
            lblTitle.Text = "Add New Vehicle";
            this.Text = "Add New Vehicle";
            isEditMode = false;
            _db = new CarRentalEntities();
            _manageVehicleListing = manageVehicleListing;
        }

        public AddEditVehicle(CarType carToEdit, ManageVehicleListing manageVehicleListing = null)
        {
            InitializeComponent();
            lblTitle.Text = "Edit Vehicle";
            this.Text = "Edit Vehicle";
            isEditMode = true;
            _manageVehicleListing = manageVehicleListing;
            _db = new CarRentalEntities();
            PopulateFields(carToEdit);
        }

        private void PopulateFields(CarType car)
        {
            lblCarTypeRef.Text = car.CarTypeRef.ToString("D");
            tbMake.Text = car.CarMake;
            tbModel.Text = car.CarModel;
            tbVIN.Text = car.VIN;
            tbYear.Text = car.Year.ToString();
            tbLicencePlateNumber.Text = car.LicencePlateNumber;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isEditMode)
            {
                // Edit Code Here
                var carTypeRef = Guid.Parse(lblCarTypeRef.Text);
                var car = _db.CarTypes.FirstOrDefault(q => q.CarTypeRef == carTypeRef);
                car.CarModel = tbModel.Text;
                car.CarMake = tbMake.Text;
                car.VIN = tbVIN.Text;
                car.Year = int.Parse(tbYear.Text);
                car.LicencePlateNumber = tbLicencePlateNumber.Text;
            }
            else
            {
                // Add Code Here
                try
                {
               
                    if (String.IsNullOrEmpty(tbMake.Text) || String.IsNullOrEmpty(tbModel.Text) || String.IsNullOrEmpty(tbYear.Text))
                    {
                        MessageBox.Show("Vehicle Make, Model and Year are required");
                    }
                    else
                    {
                        var newCar = new CarType
                        {
                            CarTypeRef = Guid.NewGuid(),
                            LicencePlateNumber = tbLicencePlateNumber.Text,
                            CarMake = tbMake.Text,
                            CarModel = tbModel.Text,
                            VIN = tbVIN.Text,
                            Year = int.Parse(tbYear.Text)

                        };

                        _db.CarTypes.Add(newCar);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}");
                }
            }
            _db.SaveChanges();
            _manageVehicleListing.PopulateGrid();
            MessageBox.Show("Operation Completed");
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
