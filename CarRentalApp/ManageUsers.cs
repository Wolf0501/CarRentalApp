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
    public partial class ManageUsers : Form
    {
        private readonly CarRentalEntities _db;
        public ManageUsers()
        {
            InitializeComponent();
            _db = new CarRentalEntities();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {

        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                //get UsersRef of selected row
                var usersRef = (Guid)gvUserList.SelectedRows[0].Cells["UsersRef"].Value;

                //Query Database for record
                var user = _db.Users.FirstOrDefault(q => q.UsersRef == usersRef);
                // hash password and save to database
                var genericPassword = "Password@123";
                var hashed_password = Utils.HashPassword(genericPassword);
                user.Password = hashed_password;
                _db.SaveChanges();
                
                MessageBox.Show($"{user.UserName}'s Password has been reset!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            PopulateUserGrid();
        }

        private void btnDeactivateUser_Click(object sender, EventArgs e)
        {
            try
            {
                //get UsersRef of selected row
                var usersRef = (Guid)gvUserList.SelectedRows[0].Cells["UserRef"].Value;

                //Query Database for record
                var user = _db.Users.FirstOrDefault(q => q.UsersRef == usersRef);
                // if   (user.isActive == True)
                //      user.isActive = false;
                //else
                //      user.isActive = true
                user.isActive = user.isActive == true ? false : true;
                _db.SaveChanges();

                if (user.isActive)
                {
                    MessageBox.Show($"{user.UserName} is now activated.");
                }
                else
                {
                    MessageBox.Show($"{user.UserName} is now deactivated.");
                }
                PopulateUserGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            PopulateUserGrid();
        }
        public void PopulateUserGrid()
        {
                        var users = _db.Users.
            Select(q => new
            {
                Name = q.UserName,
                UserRef = q.UsersRef,
                Password = q.Password,
                isActive = q.isActive
            }).ToList();

            gvUserList.DataSource = users;
            gvUserList.Columns["UserRef"].Visible = false;
            gvUserList.Columns["Password"].Visible = false;
            gvUserList.Columns["isActive"].HeaderText = "User Active";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulateUserGrid();
        }
    }
}
