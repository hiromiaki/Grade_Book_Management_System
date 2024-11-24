using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GMS1
{
    public partial class students : UserControl
    {
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        MySqlCommand cmd;
        MySqlDataAdapter adapt;
        public students()
        {
            InitializeComponent();
            DisplayData();
            comboGender.DropDownStyle = ComboBoxStyle.DropDownList;
            SetupDataGridView();
            CountingRow1();
        }

        private void SetupDataGridView()
        {
            // Assuming you have columns in the DataGridView, loop through each column
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                // Set the ReadOnly property to true for each column
                column.ReadOnly = true;
                column.HeaderCell.Style.SelectionBackColor = Color.Empty;
                column.DefaultCellStyle.Font = new Font("Lucida Sans", 9); 

                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.HeaderCell.Style.WrapMode = DataGridViewTriState.True;
                column.HeaderCell.Style.Padding = new Padding(0, 5, 0, 5);
            }
        }

        private void ColumnHeaderCell_MouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Prevent any action on column header click
            dataGridView1.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM gradebook.students WHERE StudentID = @ID", connection);
            cmd1.Parameters.AddWithValue("@ID", txtStudentNum.Text);
            connection.Open();
            bool userExists = false;
            using (var dr1 = cmd1.ExecuteReader())
                if (userExists = dr1.HasRows)
                    MessageBox.Show("StudentID not available!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            connection.Close();
            if (!(userExists))
            {
                // Adds a User in the Database
                if (txtStudentNum.Text != "" && txtLastName.Text != "" && txtFirstName.Text != "" && comboGender.Text != "" && txtYear.Text != "")
                {
                    cmd = new MySqlCommand("insert into gradebook.students(StudentID,LastName,FirstName, Age, Gender,YearAndSection) values(@ID,@last,@first, @age, @gender,@year)", connection);
                    connection.Open();
                    cmd.Parameters.AddWithValue("@ID", txtStudentNum.Text);
                    cmd.Parameters.AddWithValue("@last", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@first", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@age", txtAge.Text);
                    cmd.Parameters.AddWithValue("@gender", comboGender.Text);
                    cmd.Parameters.AddWithValue("@year", txtYear.Text);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Record Successfully Added", "INSERT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisplayData();
                    ClearData();
                    CountingRow1();
                }
                else
                {
                    MessageBox.Show("Fill out all the information needed", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void CountingRow1()
        {
            string ConnectionString = "Server=localhost;Database=gradebook;User ID=root;Pwd=;";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Query to get the count of rows from your table
                    string query = "SELECT COUNT(*) FROM students";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        // ExecuteScalar returns the first column of the first row
                        int rowCount = Convert.ToInt32(cmd.ExecuteScalar());

                        // Display the count in the label
                        txtStudentList.Text = $"TOTAL: {rowCount}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayData()
        {

            DataTable dt = new DataTable();
            adapt = new MySqlDataAdapter("select StudentID, LastName, FirstName, Age, Gender, YearAndSection from gradebook.students ORDER BY LastName", connection);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        private void ClearData()
        {
            txtStudentNum.Text = "";
            txtLastName.Text = "";
            txtFirstName.Text = "";
            comboGender.Text = "";
            txtYear.Text = "";
            txtAge.Text = "";
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtStudentNum.Text != "" && txtLastName.Text != "" && txtFirstName.Text != "" && txtAge.Text != "" && comboGender.Text != "" && txtYear.Text != "")
            {
                cmd = new MySqlCommand("delete from gradebook.students where StudentID=@StudentID", connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@StudentID", txtStudentNum.Text);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Record Successfully Deleted", "DELETE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayData();
                ClearData();
                CountingRow1();
            }
            else
            {
                MessageBox.Show("Select the record you want to Delete", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string rowHeaderText = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtStudentNum.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtLastName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtFirstName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtAge.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            comboGender.Text = rowHeaderText;
            txtYear.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtStudentNum.Text != "" && txtLastName.Text != "" && txtFirstName.Text != "" && txtAge.Text != "" && comboGender.Text != "" && txtYear.Text != "")
            {
                cmd = new MySqlCommand("update gradebook.students set LastName=@lastName, FirstName=@firstName, Age=@age, Gender=@gender, YearAndSection=@year where StudentID=@studentID", connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@studentID", txtStudentNum.Text);
                cmd.Parameters.AddWithValue("@lastName", txtLastName.Text);
                cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@age", txtAge.Text);
                cmd.Parameters.AddWithValue("@gender", comboGender.Text);
                cmd.Parameters.AddWithValue("@year", txtYear.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Successfully Updated", "UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.Close();
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Select the record you want to Update", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void students_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtStudentNum_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string rowHeaderText = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtStudentNum.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtLastName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtFirstName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtAge.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            comboGender.Text = rowHeaderText;
            txtYear.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClearData();
        }
    }
}
