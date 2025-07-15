using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeShopManagementSystem
{
    public partial class AdminMainForm : Form
    {
        private void LoadUserControl(UserControl uc)
        {
            panel2.Controls.Clear();   // Clear any existing controls
            uc.Dock = DockStyle.Fill;          // Make it fill the panel
            panel2.Controls.Add(uc);   // Add the new user control to the panel
        }

        public AdminMainForm()
        {
            InitializeComponent();
        }

        private void AdminMainForm_Load(object sender, EventArgs e)
        {
            //set back color of form

            this.BackColor = Color.White;

            //edit panel back color

            panel1.BackColor = Color.DodgerBlue;

            //insert picture

            pictureBox1.Image = Image.FromFile(@"D:\newpic.JPEG");

            //set labels text

            label2.Text = "C";
            label3.Text = "R";
            label4.Text = "D";
            label5.Text = "U";
            label1.Text = " PRODUCT";
            label7.Text = "   Control System";
            label6.Text = "USER AND";

            // SET TEXT OF BUTTONS

            logout_btn.Text = "Log Out";
            button2.Text = "Add Users";
            button3.Text = "Add Products";

            //set font of labels

            label2.Font = new Font("Times New Roman", 16, FontStyle.Bold);
            label3.Font = new Font("Times New Roman", 16, FontStyle.Bold);
            label4.Font = new Font("Times New Roman", 16, FontStyle.Bold);
            label5.Font = new Font("Times New Roman", 16, FontStyle.Bold);
            label6.Font = new Font("Stencil", 22, FontStyle.Italic);
            label7.Font = new Font("Times New Roman", 14, FontStyle.Bold);
            label1.Font = new Font("Stencil", 22, FontStyle.Italic);

            // set font of buttons

            logout_btn.Font = new Font("Times New Roman", 14, FontStyle.Bold);
            button2.Font = new Font("Times New Roman", 14, FontStyle.Bold);
            button3.Font = new Font("Times New Roman", 14, FontStyle.Bold);

            //  set backcolor of labels

            label1.BackColor = Color.DodgerBlue;
            label2.BackColor = Color.DodgerBlue;
            label3.BackColor = Color.DodgerBlue;
            label4.BackColor = Color.DodgerBlue;
            label5.BackColor = Color.DodgerBlue;
            label6.BackColor = Color.DodgerBlue;
            label7.BackColor = Color.DodgerBlue;

            //  set backcolor of buttons

            button2.BackColor = Color.DodgerBlue;
            button3.BackColor = Color.DodgerBlue;
            logout_btn.BackColor = Color.DodgerBlue;

            //  set fore color of labels

            label1.ForeColor = Color.White;
            label2.ForeColor = Color.White;
            label3.ForeColor = Color.White;
            label4.ForeColor = Color.White;
            label5.ForeColor = Color.White;
            label6.ForeColor = Color.White;
            label7.ForeColor = Color.White;

            //  set fore color of buttons

            button2.ForeColor = Color.White;
            button3.ForeColor = Color.White;
            logout_btn.ForeColor = Color.White;

        }

        private void adminAddUsers1_Load(object sender, EventArgs e)
        {

        }

        private void adminAddUsers1_Load_1(object sender, EventArgs e)
        {

        }

        private void adminAddUsers1_Load_2(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to Sign out?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                //open another form
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Hide();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
       
        private void button3_Click(object sender, EventArgs e)
        {
            AdminAddProducts productControl = new AdminAddProducts();//add controls
            LoadUserControl(productControl);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminAddUsers productControl = new AdminAddUsers();//add controls
            LoadUserControl(productControl);
        }

        private void adminAddUsers1_Load_3(object sender, EventArgs e)
        {

        }
    }
    }


