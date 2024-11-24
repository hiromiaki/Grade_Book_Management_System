using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace GMS1
{
    public partial class LoginTeacher : Form
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;user id=root;password=;database=loginform");
        string connectionString = "Server=localhost;Database=loginform;User ID=root;Password=;";
        public LoginTeacher()
        {
            InitializeComponent();
            button8.Hide();
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please input Email and Password", "Error");
            }
            else
            {
                    connection.Open();
                    string query = "SELECT SubjectCode FROM loginform.userinfo WHERE Email = @Email AND Password = @Password";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            string subjects = result.ToString();
                            MessageBox.Show("Login Successful!");

                            SubjectGrades home = new SubjectGrades();
                            home.SetSubjectLabel(subjects);
                            home.SetSubject(subjects);
                            home.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid email or password");
                        }
                    }
                connection.Close();
                }
               
            }
        


        public string GetSubjectTableName(string subjects)
        {
            string tableName = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT TableName FROM loginform.userinfo WHERE SubjectCode = @Subject";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Subject", subjects);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        tableName = result.ToString();
                    }
                }
            }

            return tableName;
        }
        private void LoginTeacher_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.ShowDialog();
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
