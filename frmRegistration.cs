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
using System.Windows;

namespace StudentRegistrationSystem
{
    public partial class frmRegistration : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();

        public frmRegistration()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            comboRegNo.Text = "";
            txtFirstname.Clear();
            txtLastname.Clear();
            dateTimePicker1.Value = DateTime.Now;
            radioMale.Checked = true;
            txtAddress.Clear();
            txtEmail.Clear();
            txtMobilePhone.Clear();
            txtHomePhone.Clear();
            txtParentName.Clear();
            txtNic.Clear();
            txtContactNo.Clear();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //get the values from the textboxes and save the data to the database table name Registration
            string regno = comboRegNo.Text;
            string firstname = txtFirstname.Text;
            string lastname = txtLastname.Text;
            string dob = dateTimePicker1.Value.ToString();
            string gender = radioMale.Checked ? radioMale.Text : radioFemale.Text;
            string address = txtAddress.Text;
            string email = txtEmail.Text;
            string mobilephone = txtMobilePhone.Text;
            string homephone = txtHomePhone.Text;
            string parentname = txtParentName.Text;
            string nic = txtNic.Text;
            string contactno = txtContactNo.Text;

            try
            {
                cn.ConnectionString = dbcon.MyConnection(); // Your connection string function
                cn.Open();

                // Insert query with parameters
                cm = new SqlCommand("INSERT INTO register (regNo, firstName, lastName, dateOfBirth, gender, address, email, mobilePhone, homePhone, parentName, nic, contactNo) " +
                                    "VALUES (@regNo, @firstName, @lastName, @dateOfBirth, @gender, @address, @email, @mobilePhone, @homePhone, @parentName, @nic, @contactNo)", cn);

                // Assigning values to the parameters
                cm.Parameters.AddWithValue("@regNo", regno);
                cm.Parameters.AddWithValue("@firstName", firstname);
                cm.Parameters.AddWithValue("@lastName", lastname);
                cm.Parameters.AddWithValue("@dateOfBirth", dob);
                cm.Parameters.AddWithValue("@gender", gender);
                cm.Parameters.AddWithValue("@address", address);
                cm.Parameters.AddWithValue("@email", email);
                cm.Parameters.AddWithValue("@mobilePhone", mobilephone);
                cm.Parameters.AddWithValue("@homePhone", homephone);
                cm.Parameters.AddWithValue("@parentName", parentname);
                cm.Parameters.AddWithValue("@nic", nic);
                cm.Parameters.AddWithValue("@contactNo", contactno);

                // Execute the query
                cm.ExecuteNonQuery();

                MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

        }

        private void comboRegNo_DropDown(object sender, EventArgs e)
        {
            
            try
            {
                comboRegNo.Items.Clear();
                cn.ConnectionString = dbcon.MyConnection();
                cn.Open();

                cm = new SqlCommand("SELECT regNo FROM register", cn);
                SqlDataReader dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    comboRegNo.Items.Add(dr["regNo"].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }

        private void comboRegNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cn.ConnectionString = dbcon.MyConnection();
                cn.Open();

                cm = new SqlCommand("SELECT * FROM register WHERE regNo = @regNo", cn);
                cm.Parameters.AddWithValue("@regNo", comboRegNo.Text);
                SqlDataReader dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    txtFirstname.Text = dr["firstName"].ToString();
                    txtLastname.Text = dr["lastName"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(dr["dateOfBirth"].ToString());

                    string databaseGender = dr["gender"].ToString(); // Assuming "gender" is your column name
                    
                    if (databaseGender == "Male")
                    {
                        radioMale.Checked = true;
                    }
                    else if (databaseGender == "Female")
                    {
                        radioFemale.Checked = true;
                    }


                    txtAddress.Text = dr["address"].ToString();
                    txtEmail.Text = dr["email"].ToString();
                    txtMobilePhone.Text = dr["mobilePhone"].ToString();
                    txtHomePhone.Text = dr["homePhone"].ToString();
                    txtParentName.Text = dr["parentName"].ToString();
                    txtNic.Text = dr["nic"].ToString();
                    txtContactNo.Text = dr["contactNo"].ToString();

                }
                dr.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //get the values from the textboxes and update the data to the database table name register

            string regno = comboRegNo.Text;
            string firstname = txtFirstname.Text;
            string lastname = txtLastname.Text;
            string dob = dateTimePicker1.Value.ToString();
            string gender = radioMale.Checked ? radioMale.Text : radioFemale.Text;
            string address = txtAddress.Text;
            string email = txtEmail.Text;
            string mobilephone = txtMobilePhone.Text;
            string homephone = txtHomePhone.Text;
            string parentname = txtParentName.Text;
            string nic = txtNic.Text;
            string contactno = txtContactNo.Text;

            try
            {
                cn.ConnectionString = dbcon.MyConnection();
                cn.Open();

                cm = new SqlCommand("UPDATE register SET firstName = @firstName, lastName = @lastName, dateOfBirth = @dateOfBirth, gender = @gender, address = @address, email = @email, mobilePhone = @mobilePhone, homePhone = @homePhone, parentName = @parentName, nic = @nic, contactNo = @contactNo WHERE regNo = @regNo",
         cn);

                cm.Parameters.AddWithValue("@firstName", firstname);
                cm.Parameters.AddWithValue("@lastName", lastname);
                cm.Parameters.AddWithValue("@dateOfBirth",
         dob);
                cm.Parameters.AddWithValue("@gender", gender);
                cm.Parameters.AddWithValue("@address", address);
                cm.Parameters.AddWithValue("@email", email);
                cm.Parameters.AddWithValue("@mobilePhone", mobilephone);
                cm.Parameters.AddWithValue("@homePhone", homephone);
                cm.Parameters.AddWithValue("@parentName", parentname);
                cm.Parameters.AddWithValue("@nic", nic);
                cm.Parameters.AddWithValue("@contactNo", contactno);
                cm.Parameters.AddWithValue("@regNo", regno);

                int rowsAffected = cm.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Record Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                }
                else
                {
                    MessageBox.Show("No records updated!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                cn.Close();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //get the values from the textboxes and delete the data from the database table name register
            string regno = comboRegNo.Text;

            try
            {
                cn.ConnectionString = dbcon.MyConnection();
                cn.Open();

                cm = new SqlCommand("DELETE FROM register WHERE regNo = @regNo", cn);
                cm.Parameters.AddWithValue("@regNo", regno);

                MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DialogResult == DialogResult.Yes)
                {
                    int rowsAffected = cm.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record Deleted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("No records deleted!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }
    }
}
