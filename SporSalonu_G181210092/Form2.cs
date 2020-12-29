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
using Npgsql;

namespace SporSalonu_G181210092
{
    public partial class Form2 : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; Database=vtys_g181210092; user ID=postgres; password=2259092");
        public Form2()
        {
            InitializeComponent();
            Console.ForegroundColor = ConsoleColor.Magenta;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 showForm = new Form1();
            showForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Malzeme showForm = new Malzeme();
            showForm.ShowDialog();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Personel showForm = new Personel();
            showForm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Fatura showForm = new Fatura();
            showForm.ShowDialog();
        }
    }
}
