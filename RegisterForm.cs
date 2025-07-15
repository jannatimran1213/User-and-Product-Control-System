using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace CafeShopManagementSystem
{
    public partial class RegisterForm : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");
        public RegisterForm()
        {
            InitializeComponent();
        }
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            //set back color of form

            this.BackColor = Color.White;

            //set labels text

            label2.Text = "REGISTER";
            label3.Text = "Username:";
            label4.Text = "Password:";
            label8.Text = "Confirm Password:";
            label5.Text = "Already have an Account";
            label1.Text = "U";
            label6.Text = "D";
            label7.Text = "R";
            label9.Text = "C";
            label10.Text = " PRODUCT";
            label11.Text = "   Control System";
            label12.Text = "USER AND";

            //set checkbox text

            register_showpass.Text = "Show Password";

            //set button text

            register_btn.Text = "Sign Up";
            register_loginbtn.Text = "SIGN IN";

            //set labels text color

            label2.ForeColor = Color.Black;
            label3.ForeColor = Color.Black;
            label4.ForeColor = Color.Black;
            label5.ForeColor = Color.White;
            label1.ForeColor = Color.White;
            label6.ForeColor = Color.White;
            label7.ForeColor = Color.White;
            label7.ForeColor = Color.White;
            label9.ForeColor = Color.White;
            label10.ForeColor = Color.White;
            label11.ForeColor = Color.White;
            label12.ForeColor = Color.White;

            //set checkbox text color

            register_showpass.ForeColor = Color.Black;


            //set buttons text color

            register_btn.ForeColor = Color.White;
            register_loginbtn.ForeColor = Color.White;

            //set buttons back color

            register_loginbtn.BackColor = Color.DodgerBlue;
            register_btn.BackColor = Color.DodgerBlue;

            //set buttons font

            register_btn.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            register_loginbtn.Font = new Font("Times New Roman", 12, FontStyle.Bold);


            //set labels back color

            label5.BackColor = Color.DodgerBlue;
            label1.BackColor = Color.DodgerBlue;
            label6.BackColor = Color.DodgerBlue;
            label7.BackColor = Color.DodgerBlue;
            label9.BackColor = Color.DodgerBlue;
            label10.BackColor = Color.DodgerBlue;
            label11.BackColor = Color.DodgerBlue;
            label12.BackColor = Color.DodgerBlue;

            //edit panel back color

            panel1.BackColor = Color.DodgerBlue;


            //set labels font

            label1.Font = new Font("Times New Roman", 16, FontStyle.Bold);
            label2.Font = new Font("Times New Roman", 14, FontStyle.Bold);
            label3.Font = new Font("Times New Roman", 12, FontStyle.Regular);
            label4.Font = new Font("Times New Roman", 12, FontStyle.Regular);
            label5.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            label6.Font = new Font("Times New Roman", 16, FontStyle.Bold);
            label7.Font = new Font("Times New Roman", 16, FontStyle.Bold);
            label8.Font = new Font("Times New Roman", 12, FontStyle.Regular);
            label9.Font = new Font("Times New Roman", 16, FontStyle.Bold);
            label10.Font = new Font("Stencil", 20, FontStyle.Italic);
            label11.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            label12.Font = new Font("Stencil", 20, FontStyle.Italic);


            //insert picture

            pictureBox1.Image = Image.FromFile(@"D:\newpic.JPEG");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 loginForm=new Form1();
            loginForm.Show();
            this.Hide();
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void register_showpass_CheckedChanged(object sender, EventArgs e)
        {
            register_password.PasswordChar = register_showpass.Checked ? '\0' : '*';
            register_cPassword.PasswordChar = register_showpass.Checked ? '\0' : '*';
        }
        public bool emptyFields()
        {
            if (register_username.Text == "" || register_password.Text == "" || register_cPassword.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void register_btn_Click(object sender, EventArgs e)
        {

            if (emptyFields())
            {
                MessageBox.Show("All fields are required to be filled.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State == ConnectionState.Closed)
                {
                    try
                    {
                        connect.Open();
                        string selectUsername = "SELECT * FROM users WHERE username = @usern"; // LETS CHECK IF THE USERNAME YOU WANT TO USE IS TAI
                        using (SqlCommand checkUsername = new SqlCommand(selectUsername, connect))
                        {
                            checkUsername.Parameters.AddWithValue("@usern", register_username.Text.Trim());
                            SqlDataAdapter adapter = new SqlDataAdapter(checkUsername);
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            if (table.Rows.Count >= 1)
                            {
                                string usern = register_username.Text.Substring(0, 1).ToUpper() + register_username.Text.Substring(1);
                                MessageBox.Show(usern + " is already taken", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (register_password.Text != register_cPassword.Text)
                            {
                                MessageBox.Show("Password does not match.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (register_password.Text.Length < 8)
                            {
                                MessageBox.Show("Invalid password, at least 8 characters are needed", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string insertData = "INSERT INTO users (username, password, profile_image, role, status, date_reg) " +
                                "VALUES (@usern, @pass, @image, @role, @status, @date)";
                                DateTime today = DateTime.Today;
                                using (SqlCommand cmd = new SqlCommand(insertData, connect))
                                {
                                    cmd.Parameters.AddWithValue("@usern", register_username.Text.Trim());
                                    cmd.Parameters.AddWithValue("@pass", register_password.Text.Trim());
                                    cmd.Parameters.AddWithValue("@image", ""); 
                                    cmd.Parameters.AddWithValue("@role", "Cashier");
                                    cmd.Parameters.AddWithValue("@status", "Approval");
                                    cmd.Parameters.AddWithValue("@date", today);
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("Registered successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Form1 loginForm = new Form1();
                                    loginForm.Show();
                                    this.Hide();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Connection failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }

        
    }
}
