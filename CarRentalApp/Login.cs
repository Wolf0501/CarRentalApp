﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class Login : Form
    {
        private readonly CarRentalEntities _db;
        public Login()
        {
            InitializeComponent();
            _db = new CarRentalEntities();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                SHA256 sha = SHA256.Create();

                var username = tbUsername.Text.Trim();
                var password = tbPassword.Text;

                // Convert the input string to a byte array and compute the hash
                byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Create a new stringbuilder to collect the bytes
                // and create a string
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                var hashed_password = sBuilder.ToString();

                var user = _db.Users.FirstOrDefault(q => q.UserName == username && q.Password == hashed_password && q.isActive == true);
                if(user == null)
                {
                    MessageBox.Show("Please provide valid credentials");
                }
                else
                {
                    var role = user.UserRoles.FirstOrDefault();
                    var roleShortName = role.Role.ShortName;
                    var mainWindow = new MainWindow(this, roleShortName);
                    mainWindow.Show();
                    Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong please try again");
            }
        }
    }
}
