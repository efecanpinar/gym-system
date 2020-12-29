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
    public partial class Malzeme : Form
    {
        public Malzeme()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection(@"server=localhost; port=5432; Database=vtys_g181210092; user ID=postgres; password=2259092"); private void MalzemeListele()
        {
            listView1.Items.Clear();
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("SELECT * FROM \"Malzeme\" ", baglanti);
            NpgsqlDataReader malzemeoku = komut.ExecuteReader();
            while (malzemeoku.Read())
            {
                ListViewItem Malzemeekle = new ListViewItem();
                Malzemeekle.Text = malzemeoku["malzemeID"].ToString();
                Malzemeekle.SubItems.Add(malzemeoku["adi"].ToString());
                Malzemeekle.SubItems.Add(malzemeoku["adet"].ToString());
              
                listView1.Items.Add(Malzemeekle);


            }
            baglanti.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MalzemeListele();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand($"INSERT INTO \"Malzeme\" (\"adi\", \"adet\" ) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "')", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MalzemeListele();
            textBox1.Clear();
            textBox2.Clear();
            
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

            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("DELETE FROM \"Malzeme\" WHERE \"malzemeID\"=(" + id + ")", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MalzemeListele();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            textBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[2].Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("UPDATE \"Malzeme\" SET \"adi\" ='" + textBox1.Text.ToString() + "', \"adet\"='" + textBox2.Text.ToString() + "'" + " WHERE \"malzemeID\"=" + id,baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MalzemeListele();
        }
    }
}
