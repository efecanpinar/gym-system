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
using Npgsql;

namespace SporSalonu_G181210092
{
    public partial class Form1 : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; Database=vtys_g181210092; user ID=postgres; password=2259092");
        public Form1()
        {
            InitializeComponent();
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            string Tipquery = "select * from \"KisiTipi\" ";
            NpgsqlCommand komut = new NpgsqlCommand(Tipquery,baglanti);
            NpgsqlDataReader datar = komut.ExecuteReader();
            DataTable datatable = new DataTable();
            datatable.Columns.Add("Tip");
            datatable.Load(datar);
            comboBox1.ValueMember = "tipID";
            comboBox1.DisplayMember="Tip";
            comboBox1.DataSource = datatable;
            baglanti.Close();

            baglanti.Open();
            string adresquery = "select * from \"Adres\" ";
            NpgsqlCommand komut1 = new NpgsqlCommand(adresquery, baglanti);
            NpgsqlDataReader datar2 = komut1.ExecuteReader();
            DataTable datatable1 = new DataTable();
            datatable1.Columns.Add("il");
            datatable1.Load(datar2);
            comboBox2.ValueMember = "adresID";
            comboBox2.DisplayMember = "il";
            comboBox2.DataSource = datatable1;
            baglanti.Close();
        }

   
        private void KayıtlarıListele()
        {
            listView1.Items.Clear();
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select * from \"Kisi\"", baglanti);
            NpgsqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["kisiID"].ToString();
                ekle.SubItems.Add(oku["adi"].ToString());
                ekle.SubItems.Add(oku["soyadi"].ToString());
                ekle.SubItems.Add(oku["boy"].ToString());
                ekle.SubItems.Add(oku["kilo"].ToString());
                ekle.SubItems.Add(oku["adresID"].ToString());
                ekle.SubItems.Add(oku["tipID"].ToString());
                listView1.Items.Add(ekle);
            }
            baglanti.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            KayıtlarıListele();          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand($"Insert into \"Kisi\" (\"adi\",\"soyadi\",\"boy\",\"kilo\",\"adresID\",\"tipID\")" +
                $"values (@p2,@p3,@p4,@p5,@p6,@p7); ", baglanti);

            komut.Parameters.AddWithValue("p2", textBox1.Text.ToString());
            komut.Parameters.AddWithValue("p3", textBox2.Text.ToString());
            komut.Parameters.AddWithValue("p4", textBox3.Text.ToString());
            komut.Parameters.AddWithValue("p5", textBox4.Text.ToString());
            komut.Parameters.AddWithValue("p6", comboBox2.SelectedValue);
            komut.Parameters.AddWithValue("p7", comboBox1.SelectedValue);
            komut.ExecuteNonQuery();
            baglanti.Close();
            KayıtlarıListele();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 showForm = new Form2();
            showForm.ShowDialog();
        }
        int id = 0;
        private void button4_Click(object sender, EventArgs e)
        {
            int tipıd = int.Parse(comboBox2.Text = listView1.SelectedItems[0].SubItems[6].Text);
            if (tipıd==2)
            {
                baglanti.Open();
                NpgsqlCommand komutt = new NpgsqlCommand("Delete from \"Kayit\" where \"UyeID\"=(" + id + ")", baglanti);
                komutt.ExecuteNonQuery();
                NpgsqlCommand komut = new NpgsqlCommand("Delete from \"Uye\" where \"kisiID\"=(" + id + ")", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                KayıtlarıListele();
            }
            
            if (tipıd == 1)
            {
                NpgsqlCommand komut = new NpgsqlCommand("[egitmenhata]", baglanti);
                komut.CommandType = System.Data.CommandType.StoredProcedure;
                komut.Parameters.AddWithValue("id", id);
                komut.Parameters.Add("sonuc", (NpgsqlTypes.NpgsqlDbType)SqlDbType.Int);
                komut.Parameters["sonuc"].Direction = ParameterDirection.Output;
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                int sonuc = Convert.ToInt32(komut.Parameters["sonuc"].Value.ToString());
                if (sonuc == 1)
                {
                    MessageBox.Show("Egitmen şu an kurs vermekte silemezsiniz");
                }
                KayıtlarıListele();
            }
        }

     

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            textBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[2].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[3].Text;
            textBox4.Text = listView1.SelectedItems[0].SubItems[4].Text;
            comboBox1.Text = listView1.SelectedItems[0].SubItems[5].Text;
            comboBox2.Text = listView1.SelectedItems[0].SubItems[6].Text;
        }
    }
}
