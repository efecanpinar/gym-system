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
    public partial class Fatura : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; Database=vtys_g181210092; user ID=postgres; password=2259092");

        public Fatura()
        {
            InitializeComponent();
        }
        int id = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 showForm = new Form2();
            showForm.ShowDialog();
        }
        private void KayıtlarıListele()
        {
            listView1.Items.Clear();
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select * from \"Fatura\"", baglanti);
            NpgsqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["faturaID"].ToString();
                ekle.SubItems.Add(oku["elektrik"].ToString());
                ekle.SubItems.Add(oku["su"].ToString());
                ekle.SubItems.Add(oku["dogalgaz"].ToString());
                listView1.Items.Add(ekle);
            }
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand($"Insert into \"Fatura\" (\"elektrik\",\"su\",\"dogalgaz\")" +
                $"values (@p2,@p3,@p4); ", baglanti);

            komut.Parameters.AddWithValue("p2", elektriktxtbox.Text.ToString());
            komut.Parameters.AddWithValue("p3", sutxtbox.Text.ToString());
            komut.Parameters.AddWithValue("p4", dogalgaztxtbox.Text.ToString());
            komut.ExecuteNonQuery();
            baglanti.Close();
            KayıtlarıListele();
            elektriktxtbox.Clear();
            sutxtbox.Clear();
            dogalgaztxtbox.Clear();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            elektriktxtbox.Text = listView1.SelectedItems[0].SubItems[1].Text;
            sutxtbox.Text = listView1.SelectedItems[0].SubItems[2].Text;
            dogalgaztxtbox.Text = listView1.SelectedItems[0].SubItems[2].Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("Delete from \"Fatura\" where \"faturaID\"= (" + id + ")", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            KayıtlarıListele();
        }
    }
}
