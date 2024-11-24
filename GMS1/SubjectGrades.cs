using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMS1
{
    public partial class SubjectGrades : Form
    {
        private MySqlConnection connection1;

        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        MySqlCommand cmd;

      

        public SubjectGrades()
        {
            InitializeComponent();
           
            txtSubject.ReadOnly = true;
            txtLastName.ReadOnly = true;
            txtStudentID.ReadOnly = true;
            txtFirstName.ReadOnly = true;
            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;
            button7.Hide();
            button8.Hide();

            string connectionString1 = "Server=localhost;Database=studentinfo;Uid=root;Pwd=;";
            connection1 = new MySqlConnection(connectionString1);

            panel3.Visible = true;
            LoadDataIntoDataGridView1();
            SetupDataGridView();
        }

        private void CountingRows()
        {
            string ConnectionString = "Server=localhost;Database=loginform;User ID=root;Pwd=;";
            string tableName = txtSubject.Text.ToLower();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Query to get the count of rows from your table
                    string query = "SELECT COUNT(*) FROM " +tableName;

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        // ExecuteScalar returns the first column of the first row
                        int rowCount = Convert.ToInt32(cmd.ExecuteScalar());

                        // Display the count in the label
                        txtRowCount.Text = $"TOTAL: {rowCount}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CountingRow1()
        {
            string ConnectionString = "Server=localhost;Database=studentinfo;User ID=root;Pwd=;";
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

            foreach (DataGridViewColumn column in dataGridView2.Columns)
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


        private void LoadDataIntoDataGridView1()
        {
            try
            {
                connection1.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT StudentID, LastName, FirstName, Gender, YearAndSection FROM students ORDER BY LastName", connection1);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data from Database1: " + ex.Message);
            }
            finally
            {
                connection1.Close();
            }
        }

        private string CalculateGrade(double average)
        {
            if (average >= 96.7 && average <= 100)
                return "1.00";
            else if (average >= 93.4 && average <= 96.6)
                return "1.25";
            else if (average >= 90.1 && average <= 93.3)
                return "1.50";
            else if (average >= 86.7 && average <= 90.0)
                return "1.75";
            else if (average >= 83.4 && average <= 86.6)
                return "2.00";
            else if (average >= 80.1 && average <= 83.3)
                return "2.25";
            else if (average >= 76.7 && average <= 80.0)
                return "2.50";
            else if (average >= 73.4 && average <= 76.6)
                return "2.75";
            else if (average >= 70.0 && average <= 73.3)
                return "3.00";
            else if (average >= 50.0 && average <= 69.9)
                return "4.00";
            else 
                return "5.00";
        }


        private void button5_Click(object sender, EventArgs e)
        {

            string tableName = txtSubject.Text.ToLower();
            if (txtStudentID.Text != "" && txtLastName.Text != "" && txtFirstName.Text != "" && txtMidterm.Text != "" && txtFinal.Text != "" && txtAttendance.Text != "" && txtQuizzes.Text != "" && txtOutputs.Text != "" && txtClassPart.Text != "")
            {
                double[] grades = new double[6];

                if (IsValidGrade(txtMidterm.Text) &&
                    IsValidGrade(txtFinal.Text) &&
                    IsValidGrade(txtAttendance.Text) &&
                    IsValidGrade(txtQuizzes.Text) &&
                    IsValidGrade(txtOutputs.Text) &&
                    IsValidGrade(txtClassPart.Text))
                {
                    grades[0] = Convert.ToDouble(txtMidterm.Text);
                    grades[1] = Convert.ToDouble(txtFinal.Text);
                    grades[2] = Convert.ToDouble(txtAttendance.Text);
                    grades[3] = Convert.ToDouble(txtQuizzes.Text);
                    grades[4] = Convert.ToDouble(txtOutputs.Text);
                    grades[5] = Convert.ToDouble(txtClassPart.Text);

                    double average = grades.Average();

                    

                    string remarks = (average >= 70) ? "Passed" : "Failed";

                    string connectionString = "Server=localhost;Database=loginform;User ID=root;Password=;";
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        string checkQuery = "SELECT COUNT(*) FROM " +tableName+ " WHERE StudentID = @StudentID";
                        using (MySqlCommand checkcommand = new MySqlCommand(checkQuery, connection))
                        {
                            checkcommand.Parameters.AddWithValue("@StudentID", txtStudentID.Text);
                            int count = Convert.ToInt32(checkcommand.ExecuteScalar());

                            if (count > 0)
                            {
                                MessageBox.Show("Student already has a grade.");
                            }
                            else
                            {
                                string query = "INSERT INTO " +tableName+ " (StudentID, LastName, FirstName, SubjectCode, Midterm, Finals, Attendance, Quizzes, Outputs, ClassPart, Grades, Remarks) VALUES (@StudentID, @Last, @First, @Subject, @Midterm, @Finals, @Attendance, @Quizzes, @Outputs, @Classpart, @Grades, @remarks)";
                                using (MySqlCommand command = new MySqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@StudentID", txtStudentID.Text);
                                    command.Parameters.AddWithValue("@Last", txtLastName.Text);
                                    command.Parameters.AddWithValue("@First", txtFirstName.Text);
                                    command.Parameters.AddWithValue("@Subject", txtSubject.Text);
                                    command.Parameters.AddWithValue("@Midterm", txtMidterm.Text);
                                    command.Parameters.AddWithValue("@Finals", txtFinal.Text);
                                    command.Parameters.AddWithValue("@Attendance", txtAttendance.Text);
                                    command.Parameters.AddWithValue("@Quizzes", txtQuizzes.Text);
                                    command.Parameters.AddWithValue("@Outputs", txtOutputs.Text);
                                    command.Parameters.AddWithValue("@Classpart", txtClassPart.Text);
                                    command.Parameters.AddWithValue("@Grades", average);
                                    command.Parameters.AddWithValue("@remarks", remarks);

                                    command.ExecuteNonQuery();

                                    MessageBox.Show("Record Successfully Added", "INSERT", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    loadData();
                                    ClearData();
                                    CountingRows(); 
                                }
                            }
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Invalid grade input. Please enter grades between 0 and 100.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Select a student you want to give a grade and input a grade", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }



        private void loadData()
        {
            string tableName = txtSubject.Text.ToLower();
            // Assuming you have a connection string
            string connectionString = "Server=localhost;Database=loginform;User ID=root;Password=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Assuming you have a SELECT query to retrieve data from the specified table
                string query = "SELECT StudentID, LastName, FirstName, SubjectCode, Grades, Remarks FROM " + tableName + " ORDER BY LastName";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Assuming you have a DataGridView control named dataGridView1
                        dataGridView2.DataSource = dataTable;
                    }
                }
             }
        }

        private void ClearData()
        {
            txtStudentID.Text = "";
            txtLastName.Text = "";
            txtFirstName.Text = "";
            txtMidterm.Text = "";
            txtFinal.Text = "";
            txtAttendance.Text = "";
            txtQuizzes.Text = "";
            txtOutputs.Text = "";
            txtClassPart.Text = "";
        }

        private bool IsValidGrade(string gradeText)
        {
            if (double.TryParse(gradeText, out double grade))
            {
                return grade >= 0 && grade <= 100;
            }
            return false;
        }

        public void SetSubjectLabel(string Subjects)
        {
            label5.Text = Subjects;
        }

        private void SubjectGrades_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMidterm.Text) || string.IsNullOrWhiteSpace(txtFinal.Text) || string.IsNullOrWhiteSpace(txtAttendance.Text) || string.IsNullOrWhiteSpace(txtQuizzes.Text) || string.IsNullOrWhiteSpace(txtOutputs.Text) || string.IsNullOrWhiteSpace(txtClassPart.Text))
            {
                txtMidterm.Text = "Midterm Exam";
                txtMidterm.ForeColor = SystemColors.GrayText;
                txtFinal.Text = "Final Exam";
                txtFinal.ForeColor = SystemColors.GrayText;
                txtAttendance.Text = "Attendance";
                txtAttendance.ForeColor = SystemColors.GrayText;
                txtQuizzes.Text = "Quizzes";
                txtQuizzes.ForeColor = SystemColors.GrayText;
                txtOutputs.Text = "Outputs";
                txtOutputs.ForeColor = SystemColors.GrayText;
                txtClassPart.Text = "Class Partcipation";
                txtClassPart.ForeColor = SystemColors.GrayText;
            }
        }

        private void txtMidterm_Enter(object sender, EventArgs e)
        {
            if (txtMidterm.ForeColor == SystemColors.GrayText)
            {
                txtMidterm.Text = "";
                txtMidterm.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtMidterm_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMidterm.Text))
            {
                txtMidterm.Text = "Midterm Exam";
                txtMidterm.ForeColor = SystemColors.GrayText;
            }
        }

        private void txtFinal_Enter(object sender, EventArgs e)
        {
            if (txtFinal.ForeColor == SystemColors.GrayText)
            {
                txtFinal.Text = "";
                txtFinal.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtFinal_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFinal.Text))
            {
                txtFinal.Text = "Final Exam";
                txtFinal.ForeColor = SystemColors.GrayText;
            }
        }

        private void txtAttendance_Enter(object sender, EventArgs e)
        {
            if (txtAttendance.ForeColor == SystemColors.GrayText)
            {
                txtAttendance.Text = "";
                txtAttendance.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtAttendance_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAttendance.Text))
            {
                txtAttendance.Text = "Attendance";
                txtAttendance.ForeColor = SystemColors.GrayText;
            }
        }

        private void txtQuizzes_Enter(object sender, EventArgs e)
        {
            if (txtQuizzes.ForeColor == SystemColors.GrayText)
            {
                txtQuizzes.Text = "";
                txtQuizzes.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtQuizzes_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtQuizzes.Text))
            {
                txtQuizzes.Text = "Quizzes";
                txtQuizzes.ForeColor = SystemColors.GrayText;
            }
        }

        private void txtOutputs_Enter(object sender, EventArgs e)
        {
            if (txtOutputs.ForeColor == SystemColors.GrayText)
            {
                txtOutputs.Text = "";
                txtOutputs.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtOutputs_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOutputs.Text))
            {
                txtOutputs.Text = "Outputs";
                txtOutputs.ForeColor = SystemColors.GrayText;
            }
        }

        private void txtClassPart_Enter(object sender, EventArgs e)
        {
            if (txtClassPart.ForeColor == SystemColors.GrayText)
            {
                txtClassPart.Text = "";
                txtClassPart.ForeColor = SystemColors.WindowText;

            }
        }

        private void txtClassPart_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtClassPart.Text))
            {
                txtClassPart.Text = "Class Participation";
                txtClassPart.ForeColor = SystemColors.GrayText;
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtStudentID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtLastName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtFirstName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        public void SetSubject(string Subjects)
        {
            txtSubject.Text = Subjects;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            Color newColor = Color.FromArgb(163, 202, 225);
            button4.BackColor = newColor;
            loadData();
            CountingRows();
            CountingRow1();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= -1)
            {
                Color blue = Color.FromArgb(41, 131, 191);
                // Customize the appearance of the column header
                using (Brush brush = new SolidBrush(blue))
                {
                    e.Graphics.FillRectangle(brush, e.CellBounds);
                    e.PaintContent(e.ClipBounds);
                    e.Handled = true;
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string tableName = txtSubject.Text.ToLower();
            if (txtStudentID.Text != "" && txtLastName.Text != "" && txtFirstName.Text != "" && txtSubject.Text != "")
            {
                cmd = new MySqlCommand("delete from loginform. " +tableName+ " where StudentID=@StudentID", connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@StudentID", txtStudentID.Text);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Record Successfully Deleted", "DELETE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Select the record you want to Delete", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtStudentID.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtLastName.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtFirstName.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tableName = txtSubject.Text.ToLower();

            if (txtStudentID.Text != "" && txtLastName.Text != "" && txtFirstName.Text != "" && txtSubject.Text != "" && txtMidterm.Text != "" && txtFinal.Text != "" && txtAttendance.Text != "" && txtQuizzes.Text != "" && txtOutputs.Text != "" && txtClassPart.Text != "")
            {
                double[] grades = new double[6];

                if (IsValidGrade(txtMidterm.Text) &&
                    IsValidGrade(txtFinal.Text) &&
                    IsValidGrade(txtAttendance.Text) &&
                    IsValidGrade(txtQuizzes.Text) &&
                    IsValidGrade(txtOutputs.Text) &&
                    IsValidGrade(txtClassPart.Text))
                {
                    grades[0] = Convert.ToDouble(txtMidterm.Text);
                    grades[1] = Convert.ToDouble(txtFinal.Text);
                    grades[2] = Convert.ToDouble(txtAttendance.Text);
                    grades[3] = Convert.ToDouble(txtQuizzes.Text);
                    grades[4] = Convert.ToDouble(txtOutputs.Text);
                    grades[5] = Convert.ToDouble(txtClassPart.Text);

                    double average = grades.Average();

                    string remarks = (average >= 70) ? "Passed" : "Failed";

                    cmd = new MySqlCommand("update loginform. " + tableName + " set LastName=@lastName, FirstName=@firstName, Midterm=@midterm, Finals=@finals, Attendance=@attendance, Quizzes=@quiz, Outputs=@outputs, ClassPart=@class, Grades=@grades, Remarks=@remarks where StudentID=@studentID", connection);
                    connection.Open();
                    cmd.Parameters.AddWithValue("@studentID", txtStudentID.Text);
                    cmd.Parameters.AddWithValue("@lastName", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@midterm", txtMidterm.Text);
                    cmd.Parameters.AddWithValue("@finals", txtFinal.Text);
                    cmd.Parameters.AddWithValue("@attendance", txtAttendance.Text);
                    cmd.Parameters.AddWithValue("@quiz", txtQuizzes.Text);
                    cmd.Parameters.AddWithValue("@outputs", txtOutputs.Text);
                    cmd.Parameters.AddWithValue("@class", txtClassPart.Text);
                    cmd.Parameters.AddWithValue("@grades", average);
                    cmd.Parameters.AddWithValue("@remarks", remarks);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Successfully Updated", "UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    connection.Close();
                    loadData();
                    ClearData();
                    CountingRows();
                }
            }
            else
            {
                MessageBox.Show("Select the record you want to Update or Fill the Record you want to Update", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtStudentID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtLastName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtFirstName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void dataGridView2_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            // Check the condition (e.g., "Failed" in the "Remarks" column)
            string remarksValue = dataGridView2.Rows[e.RowIndex].Cells["Remarks"].Value?.ToString();

            // Change the color if the condition is met
            if (remarksValue == "Failed")
            {
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White; // Optional: Change text color
            }
        }
    }
}
