using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StudentRegistrationSystem
{
    public partial class Form1 : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // check if the username and password are admin and 1234, display the message box

            string username = txtUsername.Text;
            string password = txtPassword.Text;

            cn = new SqlConnection(dbcon.MyConnection());
            cn.Open();
            cm = new SqlCommand("select * from Logins where username = @username and password = @password", cn);
            cm.Parameters.AddWithValue("@username", username);
            cm.Parameters.AddWithValue("@password", password);
            SqlDataReader dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                this.Hide();
                frmRegistration frm = new frmRegistration();
                frm.Show();
            }
            else if (username == "" || password == "")
            {
                MessageBox.Show("Please enter Username and Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Clear();
            }
            else if (username == "admin" || password == "1234")
            {
                this.Hide();
                frmRegistration frm = new frmRegistration();
                frm.Show();
            }
            else
            {
                MessageBox.Show("Invalid Login credentials, Please check Username or Password and try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Clear();
            }
            dr.Close();
            cn.Close();
        }

        private void Clear() 
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //clear the textboxes
            txtUsername.Clear();
            txtPassword.Clear();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //exit the application
            Application.Exit();
        }
    }
}

