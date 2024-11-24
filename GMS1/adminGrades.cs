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

namespace GMS1
{
    public partial class adminGrades : UserControl
    {

        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        string connectionString = "Server=localhost;Database=studentinfo;User ID=root;Password=;";
        MySqlDataAdapter adapt;
        public adminGrades()
        {
            InitializeComponent();
            DisplayData();
            button2.Hide();
        }

        static void CalculateAverageAndRemarks(string connectionString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Retrieve data from the table
                    string selectQuery = "SELECT StudentID, COSC55, COSC60, DCIT24, MATH1, INSY50, GNED04, FITT3, DCIT50 FROM students";
                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Add GWA and Remarks columns to the DataTable
                            dataTable.Columns.Add("GPA", typeof(double));
                            dataTable.Columns.Add("Remarks", typeof(string));

                            // Calculate average and assign remarks for each row
                            foreach (DataRow row in dataTable.Rows)
                            {
                                double sum = 0;

                                // Calculate the sum of subject grades
                                for (int i = 1; i < dataTable.Columns.Count - 2; i++)
                                {
                                    sum += Convert.ToDouble(row[i]);
                                }

                                // Calculate GWA (average)
                                double average = sum / (dataTable.Columns.Count - 3);

                                // Assign GWA to the DataTable
                                row["GPA"] = average;

                                // Assign Remarks based on the GWA
                                row["Remarks"] = (average >= 70) ? "Passed" : "Failed";
                            }

                            // Update the database with calculated GWA and Remarks
                            UpdateDatabaseWithGWAandRemarks(connectionString, dataTable);

                            Console.WriteLine("Average and Remarks calculated and updated successfully.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        static void UpdateDatabaseWithGWAandRemarks(string connectionString, DataTable dataTable)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Update GWA and Remarks in the database
                    string updateQuery = "UPDATE students SET GPA = @GWA, Remarks = @Remarks WHERE StudentID = @StudentID";
                    using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@GWA", row["GPA"]);
                            command.Parameters.AddWithValue("@Remarks", row["Remarks"]);
                            command.Parameters.AddWithValue("@StudentID", row["StudentID"]);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while updating GWA and Remarks: {ex.Message}");
                }
            }
        }

        private void adminGrades_Load(object sender, EventArgs e)
        {

        }

        private void DisplayData()
        {

            DataTable dt = new DataTable();
            adapt = new MySqlDataAdapter("select StudentID, LastName, FirstName, YearAndSection, COSC55, COSC60, DCIT24, MATH1, INSY50, GNED04, FITT3, DCIT50, GPA, Remarks from studentinfo.students ORDER BY GPA DESC", connection);
            adapt.Fill(dt);
            dataGridView2.DataSource = dt;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DisplayData();
            CalculateAverageAndRemarks(connectionString);
            ExecuteUpdateSubject();

        }

        private void ExecuteUpdateSubject()
        {
            try
            {
                // SQL statement to update SubjectTeacher in the subject table
                string updateSubjectsSQL = "UPDATE studentinfo.students SET COSC55 = (SELECT Grades FROM loginform.cosc55 WHERE studentinfo.students.StudentID = loginform.cosc55.StudentID)";

                using (MySqlCommand cmd = new MySqlCommand(updateSubjectsSQL, connection))
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating subjects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                // SQL statement to update SubjectTeacher in the subject table
                string updateSubjectsSQL = "UPDATE studentinfo.students SET COSC60 = (SELECT Grades FROM loginform.cosc60 WHERE studentinfo.students.StudentID = loginform.cosc60.StudentID)";

                using (MySqlCommand cmd = new MySqlCommand(updateSubjectsSQL, connection))
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating subjects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                // SQL statement to update SubjectTeacher in the subject table
                string updateSubjectsSQL = "UPDATE studentinfo.students SET DCIT24 = (SELECT Grades FROM loginform.dcit24 WHERE studentinfo.students.StudentID = loginform.dcit24.StudentID)";

                using (MySqlCommand cmd = new MySqlCommand(updateSubjectsSQL, connection))
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating subjects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                // SQL statement to update SubjectTeacher in the subject table
                string updateSubjectsSQL = "UPDATE studentinfo.students SET DCIT50 = (SELECT Grades FROM loginform.dcit50 WHERE studentinfo.students.StudentID = loginform.dcit50.StudentID)";

                using (MySqlCommand cmd = new MySqlCommand(updateSubjectsSQL, connection))
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating subjects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                // SQL statement to update SubjectTeacher in the subject table
                string updateSubjectsSQL = "UPDATE studentinfo.students SET FITT3 = (SELECT Grades FROM loginform.fitt3 WHERE studentinfo.students.StudentID = loginform.fitt3.StudentID)";

                using (MySqlCommand cmd = new MySqlCommand(updateSubjectsSQL, connection))
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating subjects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                // SQL statement to update SubjectTeacher in the subject table
                string updateSubjectsSQL = "UPDATE studentinfo.students SET GNED04 = (SELECT Grades FROM loginform.gned04 WHERE studentinfo.students.StudentID = loginform.gned04.StudentID)";

                using (MySqlCommand cmd = new MySqlCommand(updateSubjectsSQL, connection))
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating subjects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                // SQL statement to update SubjectTeacher in the subject table
                string updateSubjectsSQL = "UPDATE studentinfo.students SET INSY50 = (SELECT Grades FROM loginform.insy50 WHERE studentinfo.students.StudentID = loginform.insy50.StudentID)";

                using (MySqlCommand cmd = new MySqlCommand(updateSubjectsSQL, connection))
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating subjects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                // SQL statement to update SubjectTeacher in the subject table
                string updateSubjectsSQL = "UPDATE studentinfo.students SET MATH1 = (SELECT Grades FROM loginform.math1 WHERE studentinfo.students.StudentID = loginform.math1.StudentID)";

                using (MySqlCommand cmd = new MySqlCommand(updateSubjectsSQL, connection))
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating subjects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_RowPrePaint_1(object sender, DataGridViewRowPrePaintEventArgs e)
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
