using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace SporSalonu_G181210092
{
    public partial class Personel : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; Database=vtys_g181210092; user ID=postgres; password=2259092");
        public Personel()
        {
            InitializeComponent();
        }

        private void Personel_Load(object sender, EventArgs e)
        {

        }

        private void KayıtlarıListele()
        {
            listView1.Items.Clear();
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select * from \"Personel\"", baglanti);
            NpgsqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["personelID"].ToString();
                ekle.SubItems.Add(oku["ad"].ToString());
                ekle.SubItems.Add(oku["soyad"].ToString());
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
            NpgsqlCommand komut = new NpgsqlCommand($"Insert into \"Personel\" (\"ad\",\"soyad\")" +
                $"values (@p2,@p3); ", baglanti);

            komut.Parameters.AddWithValue("p2", adtxtbox.Text.ToString());
            komut.Parameters.AddWithValue("p3", soyadtxtbox.Text.ToString());
            komut.ExecuteNonQuery();
            baglanti.Close();
            KayıtlarıListele();
            adtxtbox.Clear();
            soyadtxtbox.Clear();
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
            NpgsqlCommand komut = new NpgsqlCommand("Delete from \"Personel\" where \"personelID\"= (" + id + ")", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            KayıtlarıListele();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            adtxtbox.Text = listView1.SelectedItems[0].SubItems[1].Text;
            soyadtxtbox.Text = listView1.SelectedItems[0].SubItems[2].Text;
        }
    }
}
