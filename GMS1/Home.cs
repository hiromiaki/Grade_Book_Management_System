using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMS1
{
    public partial class Home : Form
    {
        private MySqlConnection connection2;
        public Home()
        {
            InitializeComponent();

            string connectionString2 = "Server=localhost;Database=gradebook;Uid=root;Pwd=;";
            connection2 = new MySqlConnection(connectionString2);
            students1.Hide();
            label1.Hide();
            subjects1.Hide();
            label2.Hide();
            teachers1.Hide();
            label3.Hide();
            button8.Hide();
           
            label4.Hide();
            adminGrades1.Hide() ;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            students1.Visible = true;
            label1.Visible = true;
            subjects1.Visible = false;
            label2.Visible = false;
            teachers1.Visible = false;
            label3.Visible = false;
           
            label4.Visible = false;
            adminGrades1.Visible = false;
            Color newColor = Color.FromArgb(163, 202, 225);
            Color newColor1 = Color.FromArgb(233, 241, 245);
            button1.BackColor = newColor;
            button3.BackColor = newColor1;
            button4.BackColor = newColor1;
            button5.BackColor = newColor1;
            button7.BackColor = newColor1;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            subjects1.Visible = true;
            label2.Visible = true;
            students1.Visible = false;
            label1.Visible = false;
            teachers1.Visible = false;
            label3.Visible = false;
           
            label4.Visible = false;
            adminGrades1.Visible=false;
            Color newColor = Color.FromArgb(163, 202, 225);
            Color newColor1 = Color.FromArgb(233, 241, 245);
            button3.BackColor = newColor;
            button1.BackColor = newColor1;
            button4.BackColor = newColor1;
            button5.BackColor = newColor1;
            button7.BackColor = newColor1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            teachers1.Visible = true;
            label3.Visible = true;
            students1.Visible = false;
            label1.Visible = false;
            subjects1.Visible = false;
            label2.Visible = false;
          
            label4.Visible= false;
            adminGrades1.Visible = false;
            Color newColor = Color.FromArgb(163, 202, 225);
            Color newColor1 = Color.FromArgb(233, 241, 245);
            button5.BackColor = newColor;
            button3.BackColor = newColor1;
            button4.BackColor = newColor1;
            button1.BackColor = newColor1;
            button7.BackColor = newColor1;


        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            label4.Visible = true;
            students1.Visible = false;
            label1.Visible=false;
            teachers1.Visible=false;
            label2.Visible=false;
            subjects1.Visible=false;
            label3.Visible=false;
            adminGrades1.Visible = false;
            Color newColor = Color.FromArgb(163, 202, 225);
            Color newColor1 = Color.FromArgb(233, 241, 245);
            button4.BackColor = newColor;
            button3.BackColor = newColor1;
            button1.BackColor = newColor1;
            button5.BackColor = newColor1;
            button7.BackColor = newColor1;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            adminGrades1.Visible = true;
            label4.Visible = true;
            students1.Visible = false;
            label1.Visible = false;
            teachers1.Visible = false;
            label2.Visible = false;
            subjects1.Visible = false;
            label3.Visible = false;
            Color newColor = Color.FromArgb(163, 202, 225);
            Color newColor1 = Color.FromArgb(233, 241, 245);
            button7.BackColor = newColor;
            button3.BackColor = newColor1;
            button1.BackColor = newColor1;
            button5.BackColor = newColor1;
        }

        private void adminGrades1_Load(object sender, EventArgs e)
        {

        }

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            Form1 frm = new Form1();
            frm.ShowDialog();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.ShowDialog();
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
