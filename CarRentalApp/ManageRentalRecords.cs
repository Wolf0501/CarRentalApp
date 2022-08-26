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
    public partial class ManageRentalRecords : Form
    {
        private readonly CarRentalEntities _db;
        public ManageRentalRecords()
        {
            InitializeComponent();
            _db = new CarRentalEntities();
        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            var addRentalRecord = new AddEditRentalRecord
            {
                MdiParent = this.MdiParent
            };
            addRentalRecord.Show();
            
        }

        private void btnEditRecord_Click(object sender, EventArgs e)
        {
            try
            {
                Guid rentalDataRef = (Guid)gvRecordList.SelectedRows[0].Cells["RentalDataRef"].Value;
                var record = _db.RentalDatas.FirstOrDefault(q => q.RentalDataRef == rentalDataRef);

                var addEditRecord = new AddEditRentalRecord(record);
                addEditRecord.MdiParent = this.MdiParent;
                addEditRecord.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Please select a row");
            }
        }

        private void btnDeleteRecord_Click(object sender, EventArgs e)
        {
            try
            {
                // Get CarTypeRef of selected row
                var rentalDataRef = (Guid)gvRecordList.SelectedRows[0].Cells["RentalDataRef"].Value;
                // Query database for record
                var record = _db.RentalDatas.FirstOrDefault(q => q.RentalDataRef == rentalDataRef);
                // Delete vehicle from table
                _db.RentalDatas.Remove(record);
                _db.SaveChanges();

                PopulateRecordGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
          
        }

        private void ManageRentalRecords_Load(object sender, EventArgs e)
        {
            try
            {
                PopulateRecordGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        public void PopulateRecordGrid()
        {
            var records = _db.RentalDatas
                .Select(q => new
            {
                Customer = q.CustomerName,
                DateOut = q.DateRented,
                DateIn = q.DateReturned,
                q.RentalDataRef,
                q.Cost,
                Car = q.CarType.CarMake + " " + q.CarType.CarModel
            }
                ).ToList();

            gvRecordList.DataSource = records;
            gvRecordList.Columns["DateIn"].HeaderText = "Date In";
            gvRecordList.Columns["DateOut"].HeaderText = "Date Out";
            gvRecordList.Columns["RentalDataRef"].Visible = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulateRecordGrid();
        }
    }
}
