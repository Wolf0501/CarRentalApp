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
        private Login _login;
        public string _roleName;
        public User _user;
        public MainWindow(Login login, User user)
        {
            InitializeComponent();
            _login = login;
            _user = user;
            _roleName = user.UserRoles.FirstOrDefault().Role.ShortName;
        }
        private void addRentalRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var addRentalRecord = new AddEditRentalRecord();
            addRentalRecord.ShowDialog();
            //addRentalRecord.MdiParent = this; - this will not activate with the above dialog
            
        }

        private void manageVehicleListingToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var openForms = Application.OpenForms.Cast<Form>();
            var isOpen = openForms.Any(q => q.Name == "ManageVehicleListing");
            if (!isOpen)
            {
                var vehieListing = new ManageVehicleListing();
                vehieListing.MdiParent = this;
                vehieListing.Show();
            }
        }

        private void viewArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openForms = Application.OpenForms.Cast<Form>();
            var isOpen = openForms.Any(q => q.Name == "ManageRentalRecords");
            if (!isOpen)
            {
                var manageRentalRecords = new ManageRentalRecords();
                manageRentalRecords.MdiParent = this;
                manageRentalRecords.Show();
            }

        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            _login.Close();
        }

        private void manageUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openForms = Application.OpenForms.Cast<Form>();
            var isOpen = openForms.Any(q => q.Name == "ManageUsers");
            if (!isOpen)
            {
                var manageUsers = new ManageUsers();
                manageUsers.MdiParent = this;
                manageUsers.Show();
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            if(_user.Password == Utils.DefaultHashPassword())
            {
                var resetPassword = new ResetPassword(_user);
                resetPassword.ShowDialog();
            }

            var username = _user.UserName;
            tsiLoginText.Text = $"Logged in as: {username}";
            if (_roleName != "admin")
            {
                manageUsersToolStripMenuItem.Visible = false;
            }
        }
    }
}
