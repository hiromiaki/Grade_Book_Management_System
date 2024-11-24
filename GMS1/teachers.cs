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
    public partial class teachers : UserControl
    {
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        MySqlCommand cmd;
        MySqlDataAdapter adapt;
        public teachers()
        {
            InitializeComponent();
            DisplayData();
            Fillcombo();
            SetupDataGridView();
            comboSubjects.DropDownStyle = ComboBoxStyle.DropDownList;
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

        void Fillcombo()
        {
            string constring = "server=localhost;user id=root;password=;database=loginform";
            string Query = "select * from loginform.subject";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand(Query, conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();

                while (myReader.Read())
                {
                    string sName = myReader.GetString("SubjectCode");
                    comboSubjects.Items.Add(sName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void teachers_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (!this.txtEmail.Text.Contains('@') || !this.txtEmail.Text.Contains('.'))
            {
                MessageBox.Show("Please Enter A Valid Email", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MySqlCommand cmdCheck = new MySqlCommand("SELECT COUNT(*) FROM loginform.userinfo WHERE SubjectCode = @subjects", connection);
            cmdCheck.Parameters.AddWithValue("@subjects", comboSubjects.Text);

            connection.Open();

            int userCount = Convert.ToInt32(cmdCheck.ExecuteScalar());

            connection.Close();

            if (userCount > 0)
            {
                MessageBox.Show("User with the subject already exists!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Adds a User in the Database
                if (txtName.Text != "" && txtEmail.Text != "" && comboSubjects.Text != "" && txtPassword.Text != "")
                {
                    cmd = new MySqlCommand("INSERT INTO loginform.userinfo(Name, Email, SubjectCode, Password) VALUES (@name, @email, @subjects, @pass)", connection);
                    connection.Open();
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@subjects", comboSubjects.Text);
                    cmd.Parameters.AddWithValue("@pass", txtPassword.Text);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Record Successfully Added", "INSERT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisplayData();
                    ClearData();
                    ExecuteUpdateSubjectsSQL();
                }
                else
                {
                    MessageBox.Show("Fill out all the information needed", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExecuteUpdateSubjectsSQL()
        {
            try
            {
                // SQL statement to update SubjectTeacher in the subject table
                string updateSubjectsSQL = "UPDATE loginform.subject SET SubjectTeacher = (SELECT Name FROM loginform.userinfo WHERE loginform.subject.SubjectCode = loginform.userinfo.SubjectCode)";

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

        private void DisplayData()
        {
            connection.Open();
            DataTable dt = new DataTable();
            adapt = new MySqlDataAdapter("select Name, Email, SubjectCode, Password, DateCreated from loginform.userinfo", connection);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }
        private void ClearData()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            comboSubjects.Text = "";
            txtPassword.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtEmail.Text != "" && comboSubjects.Text != "" && txtPassword.Text != "")
            {
                cmd = new MySqlCommand("delete from loginform.userinfo where Name=@name", connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Record Successfully Deleted", "DELETE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayData();
                ClearData();
                ExecuteUpdateSubjectsSQL();
            }
            else
            {
                MessageBox.Show("Select the record you want to Delete", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtEmail.Text != "" && comboSubjects.Text != "" && txtPassword.Text != "")
            {
                cmd = new MySqlCommand("update loginform.userinfo set Email=@email, SubjectCode=@subjects, Password=@password where Name=@name", connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@subjects", comboSubjects.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Successfully Updated", "UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.Close();
                DisplayData();
                ClearData();
                ExecuteUpdateSubjectsSQL();
            }
            else
            {
                MessageBox.Show("Select the record you want to Update", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            comboSubjects.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPassword.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void comboSubjects_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            comboSubjects.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPassword.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClearData();
        }
    }
}
