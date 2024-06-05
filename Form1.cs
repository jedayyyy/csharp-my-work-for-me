using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIsual_BD_2._5
{
    public partial class Form1 : Form
    {
        Form2 f2 = new Form2();
        string login, pass;

        public Form1(string login, string pass, bool privellage)
        {
            InitializeComponent();

            if (privellage == false)
            {
                this.добавитьToolStripMenuItem.Enabled = false;
                this.удалитьToolStripMenuItem.Enabled = false;
                this.удалитьToolStripMenuItem1.Enabled = false;
                this.добавить1ToolStripMenuItem.Enabled = false;
            }

            this.login = login;
            this.pass = pass;

            label1.Hide(); label2.Hide(); label3.Hide(); label4.Hide();
            maskedTextBox1.Hide(); maskedTextBox2.Hide(); comboBox1.Hide(); comboBox2.Hide(); button1.Hide();
            dataGridView1.Hide(); button2.Hide();
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f2.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void добавить1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Show(); label2.Show(); label3.Show(); label4.Show();
            maskedTextBox1.Show(); maskedTextBox2.Show(); comboBox1.Show(); comboBox2.Show(); button1.Show();
        }

        private void ahah(SqlConnection connection)
        {
            //Для данных Combobox

            string Фильмы = "SELECT * FROM Фильмы";
            SqlCommand фильмы = new SqlCommand(Фильмы, connection);
            SqlDataReader reader = фильмы.ExecuteReader();

            List<string> data = new List<string>();

            while (reader.Read())
            {
                data.Add(reader.GetString(1));
            }

            reader.Close();

            foreach(string s in data)
            {
                comboBox1.Items.Add(s);
            }

            string Залы = "SELECT * FROM Кинозалы";
            SqlCommand залы = new SqlCommand(Залы, connection);
            SqlDataReader reader2 = залы.ExecuteReader();

            List<string> data2 = new List<string>();

            while (reader2.Read())
            {
                data2.Add(reader2.GetString(1));
            }

            reader2.Close();

            foreach (string s in data2)
            {
                comboBox2.Items.Add(s);
            }

            //Конец
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connString = @"Data Source=desktop-dblrosm\sqlexpress;Initial Catalog=2.5_db;Persist Security Info=True;User ID=" + login + ";Password=" + pass;
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();

            string dobavka = ("INSERT INTO Сеансы VALUES(" + "'" +maskedTextBox1.Text+"'" + "," + "'"+maskedTextBox2.Text+"'" + "," + (comboBox1.SelectedIndex+1) + "," + (comboBox2.SelectedIndex+34) + ")");
            SqlCommand sqlCommand = new SqlCommand(dobavka, connection);
            sqlCommand.ExecuteScalar();
            MessageBox.Show("Успешно");

            Update(connection);
            
            connection.Close();
        }

        private void Update(SqlConnection connection)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns[1].Name = "Дата";
            dataGridView1.Columns[2].Name = "Время";
            dataGridView1.Columns[3].Name = "Фильм";
            dataGridView1.Columns[4].Name = "Зал";

            string query = "SELECT * FROM Сеансы";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[5]);

                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
            }

            reader.Close();

            ahah(connection);

            connection.Close();

            foreach (string[] s in data)
            {
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Rows.Add(s);
            }
        }

        private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string connString = @"Data Source=desktop-dblrosm\sqlexpress;Initial Catalog=2.5_db;Persist Security Info=True;User ID=" + login + ";Password=" + pass;
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();

            string dobavka = ("DELETE FROM Сеансы WHERE [Айди Сеанса]=" +(dataGridView1.CurrentCell.RowIndex+13));
            SqlCommand sqlCommand = new SqlCommand(dobavka, connection);
            sqlCommand.ExecuteScalar();
            MessageBox.Show("Успешно");

            Update(connection);

            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(login, pass);
            form3.Show();
        }

        private void субботаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Show();
            button2.Show();
            try
            {
                string connString = @"Data Source=desktop-dblrosm\sqlexpress;Initial Catalog=2.5_db;Persist Security Info=True;User ID=" + login + ";Password=" + pass;
                SqlConnection connection = new SqlConnection(connString);
                connection.Open();
                MessageBox.Show("Успешно");

                string CountColumns = "SELECT count(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE table_catalog ='2.5_db' AND table_name = 'Сеансы'";
                SqlCommand sqlCommand = new SqlCommand(CountColumns, connection);
                int CountCol = (Int32)sqlCommand.ExecuteScalar();
                dataGridView1.ColumnCount = CountCol;

                //string CountRows = "SELECT count(*) from Сеансы";
                //SqlCommand sqlCommand1 = new SqlCommand(CountRows, connection);
                //int CountRow = (Int32)sqlCommand1.ExecuteScalar();
                //dataGridView1.RowCount = CountRow;

                Update(connection);
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }

        }
    }
}
