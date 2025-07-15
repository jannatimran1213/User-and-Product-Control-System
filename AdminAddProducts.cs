using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data
    ;
using System.Data.SqlClient;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
namespace CafeShopManagementSystem
{
    public partial class AdminAddProducts : UserControl
    {
        //establish sql connection
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\Documents\cafe.mdf;Integrated Security=True;Connect Timeout=30");
        public AdminAddProducts()
        {
            InitializeComponent();
            displayData();
        }
        private void AdminAddProducts_Load(object sender, EventArgs e)
        {
            //set back color of control

            this.BackColor = Color.White;

            //edit panel back color

            panel1.BackColor = Color.White;
            panel2.BackColor = Color.White;

            adminAddProducts_imageView.BackColor = Color.Silver;//set color of picture box

            //set text of labels

            label1.Text = "Data of Products";
            label2.Text = "Product ID:";
            label4.Text = "Status:";
            label3.Text = "Product Name:";
            label5.Text = "Type:";
            label6.Text = "Stock:";
            label7.Text = "Price:";

            //set font of labels

            label1.Font = new Font("Times New Roman", 14, FontStyle.Bold);
            label2.Font = new Font("Times New Roman", 10, FontStyle.Regular);
            label3.Font = new Font("Times New Roman", 10, FontStyle.Regular);
            label4.Font = new Font("Times New Roman", 10, FontStyle.Regular);
            label5.Font = new Font("Times New Roman", 10, FontStyle.Regular);
            label6.Font = new Font("Times New Roman", 10, FontStyle.Regular);
            label7.Font = new Font("Times New Roman", 10, FontStyle.Regular);
            //set buttons text

            adminAddProducts_addBtn.Text = "ADD";
            adminAddProducts_clearBtn.Text = "CLEAR";
            adminAddProducts_deleteBtn.Text = "DELETE";
            adminAddProducts_updateBtn.Text = "UPDATE";
            adminAddProducts_importBtn.Text = "Import";

            //set fonts for buttons 

            adminAddProducts_addBtn.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            adminAddProducts_clearBtn.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            adminAddProducts_deleteBtn.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            adminAddProducts_updateBtn.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            adminAddProducts_importBtn.Font = new Font("Times New Roman", 12, FontStyle.Bold);

            //set back color for buttons

            adminAddProducts_addBtn.BackColor = Color.DodgerBlue;
            adminAddProducts_clearBtn.BackColor = Color.DodgerBlue;
            adminAddProducts_deleteBtn.BackColor = Color.DodgerBlue;
            adminAddProducts_updateBtn.BackColor = Color.DodgerBlue;
            adminAddProducts_importBtn.BackColor = Color.DodgerBlue;

            //set fore color for buttons

            adminAddProducts_addBtn.ForeColor = Color.White;
            adminAddProducts_clearBtn.ForeColor = Color.White;
            adminAddProducts_deleteBtn.ForeColor = Color.White;
            adminAddProducts_updateBtn.ForeColor = Color.White;
            adminAddProducts_importBtn.ForeColor = Color.White;

            //adding items in combobox
            adminAddProducts_type.Items.Clear();
            adminAddProducts_type.DropDownStyle = ComboBoxStyle.DropDown;
            adminAddProducts_type.Items.Add("Skin care");
            adminAddProducts_type.Items.Add("Drink");
            adminAddProducts_type.Items.Add("Grocery");
            adminAddProducts_type.Items.Add("Stationery");
            adminAddProducts_status.Items.Clear();
            adminAddProducts_status.DropDownStyle = ComboBoxStyle.DropDown;
            adminAddProducts_status.Items.Add("Available");
            adminAddProducts_status.Items.Add("Unvailable");
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        public void displayData()
        {
            AdminAddProductsData prodData = new AdminAddProductsData();
            List<AdminAddProductsData> listData = prodData.productsListData();
            dataGridView1.DataSource = listData;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public bool emptyFields()
        {
            if (adminAddProducts_id.Text == "" || adminAddProducts_name.Text == ""
            || adminAddProducts_type.SelectedIndex == -1 || adminAddProducts_stock.Text == ""
            || adminAddProducts_price.Text == "" || adminAddProducts_status.SelectedIndex == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void adminAddProducts_importBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png";

                string imagePath = "";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    adminAddProducts_imageView.ImageLocation = imagePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void clearFields()
        {
            adminAddProducts_id.Text = "";
            adminAddProducts_name.Text = "";
            adminAddProducts_type.SelectedIndex = -1;
            adminAddProducts_stock.Text = "";
            adminAddProducts_price.Text = "";
            adminAddProducts_status.SelectedIndex = -1;
            adminAddProducts_imageView.Image = null;
        }

        private void adminAddProducts_addBtn_Click(object sender, EventArgs e)
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
                        // CHECKING IF THE PRODUCT ID IS EXISTING ALREADY
                        string selectProdID = "SELECT * FROM products WHERE prod_id= @prodID";
                        using (SqlCommand selectPID = new SqlCommand(selectProdID, connect))
                        {
                            selectPID.Parameters.AddWithValue("@prodID", adminAddProducts_id.Text.Trim());
                            SqlDataAdapter adapter = new SqlDataAdapter(selectPID);
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            if (table.Rows.Count >= 1)
                            {
                                MessageBox.Show("Product ID: " + adminAddProducts_id.Text.Trim() + " is taken already", "Error Message"
                                , MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string insertData = "INSERT INTO products (prod_id, prod_name, prod_type, " +
                                "prod_stock, prod_price, prod_status, prod_image, date_insert) VALUES (@prodID, @prodName" +
                                ", @prodType, @prodStock, @prodPrice, @prodStatus, @prodImage, @dateInsert)";
                                DateTime today = DateTime.Today;
                                string path = Path.Combine(@"C:\Users\HP\Desktop\jannat\jannat(lab manual)\CafeShopManagementSystem\CafeShopManagementSystem\Product_Directory\" + adminAddProducts_id.Text.Trim() + ".jpg");
                                string directoryPath = Path.GetDirectoryName(path);
                                if (!Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }
                                File.Copy(adminAddProducts_imageView.ImageLocation, path, true);
                                using (SqlCommand cmd = new SqlCommand(insertData, connect))
                                {
                                    cmd.Parameters.AddWithValue("@prodID", adminAddProducts_id.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodName", adminAddProducts_name.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodType", adminAddProducts_type.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodStock", adminAddProducts_stock.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodPrice", adminAddProducts_price.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodStatus", adminAddProducts_status.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodImage", path);
                                    cmd.Parameters.AddWithValue("@dateInsert", today);
                                    cmd.ExecuteNonQuery();
                                    clearFields();
                                    MessageBox.Show("Added successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    displayData();
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

        private void adminAddProducts_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void adminAddProducts_updateBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("All fields are required to be filled", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult check = MessageBox.Show("Are you sure you want to Update Product ID: " + adminAddProducts_id.Text.Trim() + "?",
                    "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                DateTime today = DateTime.Today;
                if (check == DialogResult.Yes)
                {
                    if (connect.State != ConnectionState.Open)
                    {
                        try
                        {
                            connect.Open();
                            string updateData = "UPDATE products SET prod_name = @prodName" +
                                               ", prod_type = @prodType, prod_stock = @prodStock, prod_price = @prodPrice, prod_status = @prodStatus" +
                                               ", date_update = @dateUpdate WHERE prod_id = @prodID";
                            using (SqlCommand updateD = new SqlCommand(updateData, connect))
                            {
                                updateD.Parameters.AddWithValue("@prodName", adminAddProducts_name.Text.Trim());
                                updateD.Parameters.AddWithValue("@prodType", adminAddProducts_type.Text.Trim());
                                updateD.Parameters.AddWithValue("@prodStock", adminAddProducts_stock.Text.Trim());
                                updateD.Parameters.AddWithValue("@prodPrice", adminAddProducts_price.Text.Trim());
                                updateD.Parameters.AddWithValue("@prodStatus", adminAddProducts_status.Text.Trim());
                                updateD.Parameters.AddWithValue("@dateUpdate", today);
                                updateD.Parameters.AddWithValue("@prodID", adminAddProducts_id.Text.Trim());
                                updateD.ExecuteNonQuery();
                                clearFields();
                                MessageBox.Show("Updated successfullyl", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                displayData();
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
        private void adminAddProducts_addBtn_Click_1(object sender, EventArgs e)
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
                        // CHECKING IF THE PRODUCT ID IS EXISTING ALREADY
                        string selectProdID = "SELECT * FROM products WHERE prod_id= @prodID";
                        using (SqlCommand selectPID = new SqlCommand(selectProdID, connect))
                        {
                            selectPID.Parameters.AddWithValue("@prodID", adminAddProducts_id.Text.Trim());
                            SqlDataAdapter adapter = new SqlDataAdapter(selectPID);
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            if (table.Rows.Count >= 1)
                            {
                                MessageBox.Show("Product ID: " + adminAddProducts_id.Text.Trim() + " is taken already", "Error Message"
                                , MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string insertData = "INSERT INTO products (prod_id, prod_name, prod_type, " +
                                "prod_stock, prod_price, prod_status, prod_image, date_insert) VALUES (@prodID, @prodName" +
                                ", @prodType, @prodStock, @prodPrice, @prodStatus, @prodImage, @dateInsert)";
                                DateTime today = DateTime.Today;
                                string path = Path.Combine(@"C:\Users\HP\Desktop\jannat\jannat(lab manual)\CafeShopManagementSystem\CafeShopManagementSystem\Product_Directory\" + adminAddProducts_id.Text.Trim() + ".jpg");
                                string directoryPath = Path.GetDirectoryName(path);
                                if (!Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }
                                File.Copy(adminAddProducts_imageView.ImageLocation, path, true);
                                using (SqlCommand cmd = new SqlCommand(insertData, connect))
                                {
                                    cmd.Parameters.AddWithValue("@prodID", adminAddProducts_id.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodName", adminAddProducts_name.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodType", adminAddProducts_type.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodStock", adminAddProducts_stock.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodPrice", adminAddProducts_price.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodStatus", adminAddProducts_status.Text.Trim());
                                    cmd.Parameters.AddWithValue("@prodImage", path);
                                    cmd.Parameters.AddWithValue("@dateInsert", today);
                                    cmd.ExecuteNonQuery();
                                    clearFields();
                                    MessageBox.Show("Added successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    displayData();
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

        private void adminAddProducts_importBtn_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                adminAddProducts_id.Text = row.Cells[1].Value.ToString();
                adminAddProducts_name.Text = row.Cells[2].Value.ToString();
                adminAddProducts_type.Text = row.Cells[3].Value.ToString();
                adminAddProducts_stock.Text = row.Cells[4].Value.ToString();
                adminAddProducts_price.Text = row.Cells[5].Value.ToString();
                adminAddProducts_status.Text = row.Cells[6].Value.ToString();
                string imagepath = row.Cells[7].Value.ToString();


            }
        }

    

        private void adminAddProducts_deleteBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("All fields are required to be filled", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult check = MessageBox.Show("Are you sure you want to Delete Product ID: " + adminAddProducts_id.Text.Trim() + "?",
                "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (check == DialogResult.Yes)
                {

                    if (connect.State != ConnectionState.Open)
                    {
                        try
                        {
                            connect.Open();
                            string deleteData = "Delete products  WHERE prod_id = @prodID";
                            DateTime today = DateTime.Today;
                            using (SqlCommand updated = new SqlCommand(deleteData, connect))
                            {
                                updated.Parameters.AddWithValue("@dateDelete", today);
                                updated.Parameters.AddWithValue("@prodID", adminAddProducts_id.Text.Trim());
                                updated.ExecuteNonQuery();
                                clearFields();
                                MessageBox.Show("Deleted successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                displayData();
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
}