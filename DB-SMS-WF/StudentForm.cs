using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DB_SMS_WF
{
    public partial class StudentForm : Form
    {
        static string connectionString = @"Data Source = DESKTOP-FJV5Q13; Initial Catalog = SMS; Integrated Security = true";
        static SqlConnection connection;
        static SqlDataAdapter adapter = new SqlDataAdapter();
        static SqlCommand command;

        private void StudentListLoad()
        {
            string queryString = "SELECT * FROM Student";
            StudentListView.DataSource = SelectRows(queryString);
        }

        private static DataTable SelectRows(string queryString)
        {
            DataTable table = new DataTable();

            command = connection.CreateCommand();
            command.CommandText = queryString;
            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }
        public StudentForm()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "UPDATE Student SET name = '" + name.Text + "', birthday = '" + birthday.Text + "', program = '" + program.Text + "' WHERE sid = '" + sid.Text + "'";
            command.ExecuteNonQuery();

            string queryString = "SELECT * FROM Student";
            StudentListView.DataSource = SelectRows(queryString);
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            StudentListLoad();
        }

        private void StudentListView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = StudentListView.CurrentRow.Index;

            sid.Text = StudentListView.Rows[i].Cells[0].Value.ToString();
            sid.ReadOnly = true;
            name.Text = StudentListView.Rows[i].Cells[1].Value.ToString();
            birthday.Text = StudentListView.Rows[i].Cells[2].Value.ToString();
            program.Text = StudentListView.Rows[i].Cells[3].Value.ToString();
        }

        private void ADD_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Student VALUES ('" + sid.Text + "', '" + name.Text + "', '" + birthday.Text + "', '" + program.Text + "')";
            command.ExecuteNonQuery();

            StudentListLoad();
        }

        private void DELETE_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Student WHERE sid = '" + sid.Text + "'";
            command.ExecuteNonQuery();

            StudentListLoad();
        }

        private void sid_TextChanged(object sender, EventArgs e)
        {

        }

        private void NEW_Click(object sender, EventArgs e)
        {
            sid.Text = "";
            sid.ReadOnly = false;
            name.Text = "";
            birthday.Text = "";
            program.Text = "";
        }
    }
}
