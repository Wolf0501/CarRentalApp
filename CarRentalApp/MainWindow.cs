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
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void addRentalRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var addRentalRecord = new AddEditRentalRecord();
            addRentalRecord.MdiParent = this;
            addRentalRecord.Show();
        }

        private void ManageVehicleListingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageVehicleListingToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var vehieListing = new ManageVehicleListing();
            vehieListing.MdiParent = this;
            vehieListing.Show();
        }

        private void viewArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var manageRentalRecords = new ManageRentalRecords();
            manageRentalRecords.MdiParent = this;
            manageRentalRecords.Show();
        }
    }
}
