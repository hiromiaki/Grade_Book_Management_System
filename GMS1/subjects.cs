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

namespace GMS1
{
    public partial class subjects : UserControl
    {
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        
        MySqlDataAdapter adapt;
        public subjects()
        {
            InitializeComponent();
            DisplayData();
            SetupDataGridView();
        }

        private void DisplayData()
        {
            connection.Open();
            DataTable dt = new DataTable();
            adapt = new MySqlDataAdapter("select SubjectName, SubjectCode, SubjectTeacher, Time from loginform.subject", connection);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
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
        private void subjects_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
