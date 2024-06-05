using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIsual_BD_2._5
{
    public partial class Form2 : Form
    {
        string login, pass;
        bool privellage;

        List<string> usersTable = new List<string>();
        List<string> passTable = new List<string>();

        public Form2()
        {
            InitializeComponent();

            usersTable.Add("administrator");
            usersTable.Add("user");

            passTable.Add("admin1337");
            passTable.Add("user");

            textBox1.Clear();
            textBox2.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login = textBox1.Text;
            pass = textBox2.Text;

            if (usersTable.Contains(login) && passTable.Contains(pass))
            {
                if (usersTable.IndexOf(login) == passTable.IndexOf(pass))
                {
                    if (login == "administrator")
                    {
                        privellage = true;
                        Form1 form1 = new Form1(login, pass, privellage);
                        form1.Show();
                        this.Hide();
                    }
                    else
                    {
                        privellage = false;
                        Form1 form1 = new Form1(login, pass, privellage);
                        form1.Show();
                        this.Hide();
                    }
                }
            }
        }
    }
}
