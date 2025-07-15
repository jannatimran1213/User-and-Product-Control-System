using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CafeShopManagementSystem
{
    public partial class AdminAddUsers : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");
        public AdminAddUsers()
        {
            InitializeComponent();
            displayAddUsersData();
        }
        private void AdminAddUsers_Load(object sender, EventArgs e)
        {
            //set back color of control

            this.BackColor = Color.White;
            //edit panel back color

            panel1.BackColor = Color.White;
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.Silver;

            adminAddUsers_imageView.BackColor = Color.Silver;//set color of picture box

            //set text of labels

            label1.Text = "Data of Users";
            label2.Text = "Username:";
            label4.Text = "Status:";
            label3.Text = "Password:";
            label5.Text = "Role:";

            //set font of labels

            label1.Font = new Font("Times New Roman", 14, FontStyle.Bold);
            label2.Font = new Font("Times New Roman", 10, FontStyle.Regular);
            label3.Font = new Font("Times New Roman", 10, FontStyle.Regular);
            label4.Font = new Font("Times New Roman", 10, FontStyle.Regular);
            label5.Font = new Font("Times New Roman", 10, FontStyle.Regular);

            //set buttons text

            adminAddUsers_addBtn.Text = "ADD";
            adminAddUsers_clearBtn.Text = "CLEAR";
            adminAddUsers_deleteBtn.Text = "DELETE";
            adminAddUsers_updateBtn.Text = "UPDATE";
            adminAddUsers_importBtn.Text = "Import Picture";

            //set fonts for buttons 

            adminAddUsers_addBtn.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            adminAddUsers_clearBtn.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            adminAddUsers_deleteBtn.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            adminAddUsers_updateBtn.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            adminAddUsers_importBtn.Font = new Font("Times New Roman", 12, FontStyle.Bold);

            //set back color for buttons

            adminAddUsers_addBtn.BackColor = Color.DodgerBlue;
            adminAddUsers_clearBtn.BackColor = Color.DodgerBlue;
            adminAddUsers_deleteBtn.BackColor = Color.DodgerBlue;
            adminAddUsers_updateBtn.BackColor = Color.DodgerBlue;
            adminAddUsers_importBtn.BackColor = Color.DodgerBlue;

            //set fore color for buttons

            adminAddUsers_addBtn.ForeColor = Color.White;
            adminAddUsers_clearBtn.ForeColor = Color.White;
            adminAddUsers_deleteBtn.ForeColor = Color.White;
            adminAddUsers_updateBtn.ForeColor = Color.White;
            adminAddUsers_importBtn.ForeColor = Color.White;

            //adding items in combobox
            adminAddUsers_role.Items.Clear();
            adminAddUsers_role.DropDownStyle = ComboBoxStyle.DropDown;
            adminAddUsers_role.Items.Add("Staff");
            adminAddUsers_role.Items.Add("Admin");
            adminAddUsers_role.DropDownStyle = ComboBoxStyle.DropDown;
            adminAddUsers_status.Items.Clear();
            adminAddUsers_status.Items.Add("Active");
            adminAddUsers_status.Items.Add("inactive");
            adminAddUsers_status.Items.Add("Approval");
            
        }
        public void displayAddUsersData()
        {
            AdminAddUsersData usersData = new AdminAddUsersData();
            List<AdminAddUsersData> listData = usersData.usersListData();
            dataGridView1.DataSource = listData;
        }
       

      
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public bool emptyFields()
        {
            if (adminAddUsers_username.Text == "" || adminAddUsers_password.Text == ""
            || adminAddUsers_role.Text == "" || adminAddUsers_status.Text == "" || adminAddUsers_imageView.Image == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool emptyFields2()
        {
            if (adminAddUsers_username.Text == "" || adminAddUsers_password.Text == ""
            || adminAddUsers_role.Text == "" || adminAddUsers_status.Text == "" )
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void adminAddUsers_addBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("All fields are required to be filled", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State == ConnectionState.Closed)
                {
                    try
                    {
                        connect.Open();
                        string selectUsern = "SELECT * FROM users WHERE username = @usern";
                        using (SqlCommand checkUsern = new SqlCommand(selectUsern, connect))
                        {
                            checkUsern.Parameters.AddWithValue("@usern", adminAddUsers_username.Text.Trim());
                            SqlDataAdapter adapter = new SqlDataAdapter(checkUsern);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            if (table.Rows.Count >= 1)
                            {
                                string usern = adminAddUsers_username.Text.Substring(0, 1).ToUpper() + adminAddUsers_username.Text.Substring(1);
                                MessageBox.Show(usern + " is already taken", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string insertData = "INSERT INTO users (username, password, profile_image, role, status, date_reg) " +
                                                    "VALUES (@usern, @pass, @image, @role, @status, @date)";
                                DateTime today = DateTime.Today;
                                string path = Path.Combine(@"C:\Users\HP\Desktop\jannat\jannat(lab manual)\CafeShopManagementSystem\CafeShopManagementSystem\Directory\" + adminAddUsers_username.Text.Trim() + ".jpg");
                                string directoryPath = Path.GetDirectoryName(path);
                                if (!Directory.Exists(directoryPath))
                                {
                                }
                                Directory.CreateDirectory(directoryPath);
                                File.Copy(adminAddUsers_imageView.ImageLocation, path, true);
                                using (SqlCommand cmd = new SqlCommand(insertData, connect))
                                {
                                    cmd.Parameters.AddWithValue("@usern", adminAddUsers_username.Text.Trim());
                                    cmd.Parameters.AddWithValue("@pass", adminAddUsers_password.Text.Trim());
                                    cmd.Parameters.AddWithValue("@image", path);
                                    cmd.Parameters.AddWithValue("@role", adminAddUsers_role.Text.Trim());
                                    cmd.Parameters.AddWithValue("@status", adminAddUsers_status.Text.Trim());
                                    cmd.Parameters.AddWithValue("@date", today);
                                    cmd.ExecuteNonQuery();
                                    clearFields();
                                    MessageBox.Show("Added Successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    displayAddUsersData();
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

        private void adminAddUsers_importBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png";

                string imagePath = "";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    adminAddUsers_imageView.ImageLocation = imagePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private int id = 0;
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void adminAddUsers_updateBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields2())
            {
                MessageBox.Show("All fields are required to be filled.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                
                DialogResult result = MessageBox.Show("Are you sure you want to Update Username: " + adminAddUsers_username.Text.Trim() +
                    "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (connect.State == ConnectionState.Closed)
                    {
                        try
                        {
                            connect.Open();
                            string updateData = "UPDATE users SET username = @usern, password = @pass, role = @role, status = @status WHERE id = @id";
                            using (SqlCommand cmd = new SqlCommand(updateData, connect))
                            {
                                cmd.Parameters.AddWithValue("@usern", adminAddUsers_username.Text.Trim());
                                cmd.Parameters.AddWithValue("@pass", adminAddUsers_password.Text.Trim());
                                cmd.Parameters.AddWithValue("@role", adminAddUsers_role.Text.Trim());
                                cmd.Parameters.AddWithValue("@status", adminAddUsers_status.Text.Trim());
                                
                                cmd.Parameters.AddWithValue("@id", id);

                                cmd.ExecuteNonQuery();
                               
                                MessageBox.Show("Updated successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               
                                displayAddUsersData();
                                clearFields();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Connection failed: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            connect.Close();
                        }
                    }
                }
            }
        }
        public void clearFields()
        {
          
    adminAddUsers_username.Text = "";
    adminAddUsers_password.Text = "";
    adminAddUsers_role.SelectedIndex = -1;
    adminAddUsers_status.SelectedIndex = -1;
    adminAddUsers_imageView.Image = null;
        }

        private void adminAddUsers_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void adminAddUsers_deleteBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields2())
            {
                MessageBox.Show("All fields are required to be filled.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to Delete Username: " + adminAddUsers_username.Text.Trim()
                    + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (connect.State != ConnectionState.Open)
                    {
                        try
                        {
                            connect.Open();
                            string deleteData = "DELETE FROM users WHERE id = @id";
                            using (SqlCommand cmd = new SqlCommand(deleteData, connect))
                            {
                                cmd.Parameters.AddWithValue("@id", id);
                                cmd.ExecuteNonQuery();
                                clearFields();
                                MessageBox.Show("Deleted successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                clearFields();
                                displayAddUsersData();
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            id = (int)row.Cells[1].Value;

            adminAddUsers_username.Text = row.Cells[0].Value.ToString();
            adminAddUsers_password.Text = row.Cells[3].Value.ToString();
            adminAddUsers_role.Text = row.Cells[4].Value.ToString();
            adminAddUsers_status.Text = row.Cells[2].Value.ToString();
          


        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}