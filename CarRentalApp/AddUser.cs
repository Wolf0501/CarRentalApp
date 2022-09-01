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
    public partial class AddUser : Form
    {
        private readonly CarRentalEntities _db;
        private ManageUsers _manageUsers;
        public AddUser(ManageUsers manageUsers)
        {
            InitializeComponent();
            _db = new CarRentalEntities();
            _manageUsers = manageUsers;
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            var roles = _db.Roles.ToList();
            cbRole.DataSource = roles;
            cbRole.ValueMember = "RolesRef";
            cbRole.DisplayMember = "Name";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var username = tbUsername.Text;
                var roleRef = (Guid)cbRole.SelectedValue;
                var password = Utils.DefaultHashPassword();
                var user = new User
                {
                    UsersRef = Guid.NewGuid(),
                    UserName = username,
                    Password = password,
                    isActive = true,
                };
                _db.Users.Add(user);
                _db.SaveChanges();

                var userRef = user.UsersRef;

                var userRole = new UserRole
                {
                    RolesRef = roleRef,
                    UsersRef = userRef,
                    UserRolesRef = Guid.NewGuid()
                };

                _db.UserRoles.Add(userRole);
                _db.SaveChanges();

                MessageBox.Show("New User Added Successfully");
                _manageUsers.PopulateUserGrid();
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("An Error has occoured");
            }
            
        }
    }
}
