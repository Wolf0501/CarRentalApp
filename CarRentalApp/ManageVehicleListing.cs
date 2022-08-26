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
    public partial class ManageVehicleListing : Form
    {
        private readonly CarRentalEntities _db;
        public ManageVehicleListing()
        {
            InitializeComponent();
            _db = new CarRentalEntities();
        }

        private void ManageVehicleListing_Load(object sender, EventArgs e)
        {
            //slect * from CarTypes
            //var cars = _db.CarTypes.ToList();

            //Select CarRef as CarRef, CarName as CarName FROM CarTypes
            //var cars = _db.CarTypes
            //    .Select(q => new { CarRef = q.CarTypeRef, CarName = q.CarMake,  })
            //    .ToList();
            var cars = _db.CarTypes
                .Select(q => new
                {
                    Make = q.CarMake,
                    CarModel = q.CarModel,
                    VIN = q.VIN,
                    YEAR = q.Year,
                    LicencePlateNumber = q.LicencePlateNumber,
                    q.CarTypeRef
                })
                .ToList();
            gvVehicleList.DataSource = cars;
            gvVehicleList.Columns[4].HeaderText = "Licence Plate Number";
            gvVehicleList.Columns[5].Visible = false;
            //gvVehicleList.Columns[0].HeaderText = "Referance";
            //gvVehicleList.Columns[1].HeaderText = "Name";
        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            var addEditVehicle = new AddEditVehicle(this);
            addEditVehicle.MdiParent = this.MdiParent;
            addEditVehicle.Show();
        }

        private void btnEditCar_Click(object sender, EventArgs e)
        {
            try
            {
                // Get CarTypeRef of selected row
                var carTypeRef = (Guid)gvVehicleList.SelectedRows[0].Cells["CarTypeRef"].Value;

                // Query database for record
                var car = _db.CarTypes.FirstOrDefault(q => q.CarTypeRef == carTypeRef);

                // Launch AddEditVehicle window with Data
                var addEditVehicle = new AddEditVehicle(car, this);
                addEditVehicle.MdiParent = this.MdiParent;
                addEditVehicle.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a row");
            }
        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            try
            {
                // Get CarTypeRef of selected row
                var carTypeRef = (Guid)gvVehicleList.SelectedRows[0].Cells["CarTypeRef"].Value;
                // Query database for record
                var car = _db.CarTypes.FirstOrDefault(q => q.CarTypeRef == carTypeRef);

                //confirm delete
                DialogResult dr = MessageBox.Show("Are you sure you want to delete this record?",
                    "Delete", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                // Delete vehicle from table
                if (dr == DialogResult.Yes)
                {
                    _db.CarTypes.Remove(car);
                    _db.SaveChanges();
                }
                PopulateGrid();
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a row");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        public void PopulateGrid()
        {
            var cars = _db.CarTypes
                .Select(q => new
                {
                    CarMake = q.CarMake,
                    CarModel = q.CarModel,
                    VIN = q.VIN,
                    Year = q.Year,
                    LicencePlateNumber = q.LicencePlateNumber,
                    q.CarTypeRef
                })
                .ToList();

            gvVehicleList.DataSource = cars;
            gvVehicleList.Columns[4].HeaderText = "Licence Plate Number";
            gvVehicleList.Columns["CarTypeRef"].Visible = false;
        }
    }
}
