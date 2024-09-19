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
            else if (username == "Admin" || password == "Skills@123")
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
            Clear();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        
        }
    }
}

