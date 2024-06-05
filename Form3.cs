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
    public partial class Form3 : Form
    {
        string login; string pass;
        SqlConnection connection;

        public Form3(string login, string pass)
        {
            InitializeComponent();

            this.login = login;
            this.pass = pass;

            string connString = @"Data Source=desktop-dblrosm\sqlexpress;Initial Catalog=2.5_db;Persist Security Info=True;User ID=" + this.login + ";Password=" + this.pass;
            connection = new SqlConnection(connString);
            connection.Open();

            Фильмы(connection);

            dataGridView1.RowCount = 5;
            dataGridView1.ColumnCount = 18;

            button1.Enabled = false;
            button2.Enabled = false;

            connection.Close();
        }

        public void Tickets()
        {
            string Tickets = "SELECT * FROM ";
            SqlCommand sqlCommand = new SqlCommand(Tickets, connection);
        }

        public void Фильмы(SqlConnection connection)
        {
            string Фильмы = "SELECT * FROM Фильмы";
            SqlCommand фильмы = new SqlCommand(Фильмы, connection);
            SqlDataReader reader = фильмы.ExecuteReader();

            List<string> data = new List<string>();

            while (reader.Read())
            {
                data.Add(reader.GetString(1));
            }

            reader.Close();

            foreach (string s in data)
            {
                comboBox1.Items.Add(s);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            connection.Open();
            comboBox2.Items.Clear();
            string Залы = "SELECT * FROM Сеансы WHERE Фильм=" +(comboBox1.SelectedIndex+1);
            SqlCommand залы = new SqlCommand(Залы, connection);
            SqlDataReader reader2 = залы.ExecuteReader();

            List<string> data2 = new List<string>();

            while (reader2.Read())
            {
                data2.Add(reader2.GetInt32(0).ToString());
            }

            reader2.Close();

            foreach (string s in data2)
            {
                comboBox2.Items.Add(s);
            }
            connection.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            connection.Open();
            string Операции = "SELECT * FROM Операции WHERE [Айди Сеанса]=" + (comboBox2.SelectedIndex+13);
            SqlCommand операции = new SqlCommand(Операции, connection);
            SqlDataReader операциии = операции.ExecuteReader();

            List<int> data = new List<int>();
            List<int> data1 = new List<int>();

            while (операциии.Read())
            {
                data.Add(операциии.GetInt32(6));
                data1.Add(операциии.GetInt32(7));
            }

            операциии.Close();

            for (int i=0; i < 5; i++)
            {
               for(int j=0; j < 18; j++)
               {
                    dataGridView1.Rows[i].Cells[j].Value = (j+1).ToString();
                    if((data1.Contains(i+1) && data.Contains(j+1)) && (data1.IndexOf(i+1) == data.IndexOf(j+1)))
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Red;
                    }
               }
            }

            connection.Close();

            connection.Open();

            string Бронь = "SELECT * FROM Бронь WHERE [Айди Сеанса]=" + (comboBox2.SelectedIndex + 13);
            SqlCommand бронь = new SqlCommand(Бронь, connection);
            SqlDataReader броньь = бронь.ExecuteReader();

            List<int> data2 = new List<int>();
            List<int> data3 = new List<int>();

            while (броньь.Read())
            {
                data2.Add(броньь.GetInt32(2));
                data3.Add(броньь.GetInt32(3));
            }

            броньь.Close();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 18; j++)
                {
                    if ((data3.Contains(i + 1) && data2.Contains(j + 1)) && (data3.IndexOf(i + 1) == data2.IndexOf(j + 1)))
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Gray;
                    }

                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[dataGridView1.CurrentCell.ColumnIndex].Style.BackColor != Color.Red)
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }
        }


        int z=1;

        private void button1_Click(object sender, EventArgs e)
        {
            string connString = @"Data Source=desktop-dblrosm\sqlexpress;Initial Catalog=2.5_db;Persist Security Info=True;User ID=" + this.login + ";Password=" + this.pass;
            connection = new SqlConnection(connString);
            connection.Open();
            //string lastval = "SELECT [Айди Билета] FROM Операции ORDER BY [Айди Билета] DESC";
            //SqlCommand SqlCommand = new SqlCommand(lastval, connection);
            //int val = (Int32)SqlCommand.ExecuteScalar();
            //z = val;
            //z++;
            string Покупка = ("INSERT INTO Операции VALUES(1," + z + ",1,1," + (comboBox2.SelectedIndex + 13) + "," + (dataGridView1.CurrentCell.ColumnIndex + 1) + "," + (dataGridView1.CurrentCell.RowIndex + 1) +")");
            SqlCommand sqlCommand = new SqlCommand(Покупка, connection);
            sqlCommand.ExecuteScalar();
            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[dataGridView1.CurrentCell.ColumnIndex].Style.BackColor = Color.Red;
            MessageBox.Show("Билет куплен!");
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connString = @"Data Source=desktop-dblrosm\sqlexpress;Initial Catalog=2.5_db;Persist Security Info=True;User ID=" + this.login + ";Password=" + this.pass;
            connection = new SqlConnection(connString);
            connection.Open();
            string Покупка = ("INSERT INTO Бронь VALUES("+(comboBox2.SelectedIndex + 13) + "," + (dataGridView1.CurrentCell.ColumnIndex + 1) + "," + (dataGridView1.CurrentCell.RowIndex + 1) + ")");
            SqlCommand sqlCommand = new SqlCommand(Покупка, connection);
            sqlCommand.ExecuteScalar();
            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[dataGridView1.CurrentCell.ColumnIndex].Style.BackColor = Color.Gray;
            MessageBox.Show("Билет забронирован!");
            connection.Close();
        }
    }
}
