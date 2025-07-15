using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeShopManagementSystem
{
    public partial class Form1 : Form
    {
        // Establish SQL connection to the local database

        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //set back color of form

            this.BackColor = Color.White;

            //set labels text

            label2.Text = "SIGN IN";
            label3.Text = "Username:";
            label4.Text = "Password:";
            label5.Text = "Create an Account";
            label6.Text = "U";
            label7.Text = "D";
            label8.Text = "R";
            label9.Text = "C";
            label10.Text = " PRODUCT";
            label11.Text = "   Control System";
            label12.Text = "USER AND";

            //set checkbox text

            checkBox1.Text = "Show Password";

            //set button text

            login_btn.Text = "LOGIN";
            login_registerbtn.Text = "REGISTER";
            

            //set labels text color

            label2.ForeColor = Color.Black;
            label3.ForeColor = Color.Black;
            label4.ForeColor = Color.Black;
            label5.ForeColor = Color.White;
            label6.ForeColor = Color.White;
            label7.ForeColor = Color.White;
            label8.ForeColor = Color.White;
            label9.ForeColor = Color.White;
            label10.ForeColor = Color.White;
            label11.ForeColor = Color.White;
            label12.ForeColor = Color.White;

            //set checkbox text color

            checkBox1.ForeColor = Color.Black;

            //set buttons text color

            login_btn.ForeColor = Color.White;
            login_registerbtn.ForeColor = Color.White;

            //set labels back color

            label5.BackColor = Color.DodgerBlue;
            label6.BackColor = Color.DodgerBlue;
            label7.BackColor = Color.DodgerBlue;
            label8.BackColor = Color.DodgerBlue;
            label9.BackColor = Color.DodgerBlue;
            label10.BackColor = Color.DodgerBlue;
            label11.BackColor = Color.DodgerBlue;
            label12.BackColor = Color.DodgerBlue;

            //edit panel back color

            panel1.BackColor = Color.DodgerBlue;
           
            //set buttons back color

            login_registerbtn.BackColor = Color.DodgerBlue;
            login_btn.BackColor = Color.DodgerBlue;

            //set labels font

            label2.Font = new Font("Times New Roman", 14, FontStyle.Bold);
            label3.Font = new Font("Times New Roman", 12, FontStyle.Regular);
            label4.Font = new Font("Times New Roman", 12, FontStyle.Regular);
            label5.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            label6.Font = new Font("Times New Roman", 16, FontStyle.Bold);
            label7.Font = new Font("Times New Roman", 16, FontStyle.Bold);
            label8.Font = new Font("Times New Roman", 16, FontStyle.Bold);
            label9.Font = new Font("Times New Roman", 16, FontStyle.Bold);
            label10.Font = new Font("Stencil", 20, FontStyle.Italic);
            label11.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            label12.Font = new Font("Stencil", 20, FontStyle.Italic);

            //set buttons font

            login_btn.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            login_registerbtn.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            
            //insert picture

            pictureBox1.Image = Image.FromFile(@"D:\newpic.JPEG");
        }



        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public bool emptyFields()
        {
            if (login_username.Text == "" || login_password.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

           
            if (emptyFields())
            {
                MessageBox.Show("All fields are required to be filled.", "Error Message", MessageBoxButtons.OK);
            }
            else
            {
                if (connect.State == ConnectionState.Closed)
                    try
                    {
                        connect.Open();
                        string selectAccount = "SELECT * FROM users WHERE username = @usern AND password = @pass AND status = @status";
                        using (SqlCommand cmd = new SqlCommand(selectAccount, connect))
                        {
                            // Add user input parameters

                            cmd.Parameters.AddWithValue("@usern", login_username.Text.Trim());
                            cmd.Parameters.AddWithValue("@pass", login_password.Text.Trim());
                            cmd.Parameters.AddWithValue("@status", "Active");
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            if (table.Rows.Count >= 1)
                            {
                                MessageBox.Show("Login successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                AdminMainForm adminForm = new AdminMainForm();
                                adminForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Incorrect Username/Password or there is no admin approval" +
                                    "", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Connection failed:" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
            }
        }
        private void login_registerbtn_Click(object sender, EventArgs e)
        {
            //open another form
            RegisterForm regForm = new RegisterForm();
            regForm.Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            login_password.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
