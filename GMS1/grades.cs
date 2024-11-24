using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace GMS1
{
    public partial class grades : UserControl
    {
        private MySqlConnection connection1;
        private MySqlConnection connection2;
      
        public grades()
        {
            InitializeComponent();
            //Database Students
            string connectionString1 = "Server=localhost;Database=studentinfo;Uid=root;Pwd=;";
            connection1 = new MySqlConnection(connectionString1);

            string connectionString2 = "Server=localhost;Database=gradeinfo;Uid=root;Pwd=;";
            connection2 = new MySqlConnection(connectionString2);

            LoadDataIntoDataGridView1();
            LoadDataGrades();
        }

        private void LoadDataGrades()
        {
            try
            {
                connection2.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT StudentID, LastName, FirstName, SubjectCode, Grades, Remarks FROM grades", connection2);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                adapter.Fill(dt);

                dataGridView2.DataSource = dt;
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

        private void LoadDataIntoDataGridView1()
        {
            try
            {
                connection1.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT StudentID, LastName, FirstName, Gender, YearAndSection FROM students", connection1);
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

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void grades_Load(object sender, EventArgs e)
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

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (txtMidterm.ForeColor == SystemColors.GrayText)
            {
                txtMidterm.Text = "";
                txtMidterm.ForeColor = SystemColors.WindowText;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMidterm.Text))
            {
                txtMidterm.Text = "Midterm Exam";
                txtMidterm.ForeColor = SystemColors.GrayText;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (txtFinal.ForeColor == SystemColors.GrayText)
            {
                txtFinal.Text = "";
                txtFinal.ForeColor = SystemColors.WindowText;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (txtAttendance.ForeColor == SystemColors.GrayText)
            {
                txtAttendance.Text = "";
                txtAttendance.ForeColor = SystemColors.WindowText;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (txtQuizzes.ForeColor == SystemColors.GrayText)
            {
                txtQuizzes.Text = "";
                txtQuizzes.ForeColor = SystemColors.WindowText;
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (txtOutputs.ForeColor == SystemColors.GrayText)
            {
                txtOutputs.Text = "";
                txtOutputs.ForeColor = SystemColors.WindowText;
            }
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (txtClassPart.ForeColor == SystemColors.GrayText)
            {
                txtClassPart.Text = "";
                txtClassPart.ForeColor = SystemColors.WindowText;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFinal.Text))
            {
                txtFinal.Text = "Final Exam";
                txtFinal.ForeColor = SystemColors.GrayText;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAttendance.Text))
            {
                txtAttendance.Text = "Attendance";
                txtAttendance.ForeColor = SystemColors.GrayText;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtQuizzes.Text))
            {
                txtQuizzes.Text = "Quizzes";
                txtQuizzes.ForeColor = SystemColors.GrayText;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOutputs.Text))
            {
                txtOutputs.Text = "Outputs";
                txtOutputs.ForeColor = SystemColors.GrayText;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtClassPart.Text))
            {
                txtClassPart.Text = "Class Participation";
                txtClassPart.ForeColor = SystemColors.GrayText;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {
            

            if (txtStudentID.Text != "" && txtLastName.Text != "" && txtFirstName.Text != "" && txtMidterm.Text != "" && txtFinal.Text != "" && txtAttendance.Text != "" && txtQuizzes.Text != "" && txtOutputs.Text != "" && txtClassPart.Text != "")
            {
                double[] grades = new double[6]; ;

                grades[0] = Convert.ToDouble(txtMidterm.Text);
                grades[1] = Convert.ToDouble(txtFinal.Text);
                grades[2] = Convert.ToDouble(txtAttendance.Text);
                grades[3] = Convert.ToDouble(txtQuizzes.Text);
                grades[4] = Convert.ToDouble(txtOutputs.Text);
                grades[5] = Convert.ToDouble(txtClassPart.Text);

                double average = grades.Average();

                string connectionString = "Server=localhost;Database=gradeinfo;User ID=root;Password=;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO grades (StudentID, LastName, FirstName, SubjectCode, Midterm, Finals, Attendance, Quizzes, Outputs, ClassPart, Grades) VALUES (@StudentID, @Last, @First, @Subject, @Midterm, @Finals, @Attendance, @Quizzes, @Outputs, @Classpart, @Grades)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentID", txtStudentID.Text);
                        command.Parameters.AddWithValue("@Last", txtLastName.Text);
                        command.Parameters.AddWithValue("@First", txtFirstName.Text);
                        command.Parameters.AddWithValue("@Subject",txtSubject.Text);
                        command.Parameters.AddWithValue("@Midterm", txtMidterm.Text);
                        command.Parameters.AddWithValue("@Finals", txtFinal.Text);
                        command.Parameters.AddWithValue("@Attendance", txtAttendance.Text);
                        command.Parameters.AddWithValue("@Quizzes", txtQuizzes.Text);
                        command.Parameters.AddWithValue("@Outputs", txtOutputs.Text);
                        command.Parameters.AddWithValue("@Classpart", txtClassPart.Text);
                        command.Parameters.AddWithValue("@Grades", average);

                        command.ExecuteNonQuery();
                    }
                }

                DisplayAverageInDataGridView();
            }
            else
            {
                MessageBox.Show("Select a student you want to give a grade and input a grade", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayAverageInDataGridView()
        {
            string connectionString = "Server=localhost;Database=gradeinfo;User ID=root;Password=;";
            string query = "SELECT StudentID, LastName, FirstName, SubjectCode, Midterm, Finals, Attendance, Quizzes, Outputs, ClassPart, Grades Grades FROM grades";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Display the result in the DataGridView
                    dataGridView2.DataSource = dataTable;
                }
            }
        }
    }
}
