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


namespace PASTANE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-TP2LRU1\SQLEXPRESS;Initial Catalog=PastahaneDb;Integrated Security=True");
        void MalzemeListe()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBLMALZEMELER", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
         
        void UrunListesi()
        {
            SqlDataAdapter da2 = new SqlDataAdapter("Select * From TBLURUNLER", baglanti);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
        }

        void Kasa()
        {
            SqlDataAdapter da3 = new SqlDataAdapter("Select * From TBLKASA", baglanti);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            dataGridView1.DataSource = dt3;
        }
        void Urunler()
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBLURUNLER",baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbUrun.ValueMember = "URUNID";
            cmbUrun.DisplayMember = "AD";
            cmbUrun.DataSource = dt;
            baglanti.Close();   
        }
        void malzemeler()
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBLMALZEMELER", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbmalzeme.ValueMember = "MALZEMEID";
            cmbmalzeme.DisplayMember = "AD";
            cmbmalzeme.DataSource = dt;
            baglanti.Close();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MalzemeListe();
        }

        private void btnMalzemeList_Click(object sender, EventArgs e)
        {
            MalzemeListe();
        }

        private void btnUrunList_Click(object sender, EventArgs e)
        {
            UrunListesi();
        }

        private void btnKasa_Click(object sender, EventArgs e)
        {
            Kasa();
        }

        private void btnMalzemeEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLMALZEMELER (AD,STOK,FIYAT,NOTLAR) values (@p1,@p2,@p3,@p4)",baglanti);
            komut.Parameters.AddWithValue("@P1", txtMalzemeAd.Text);
            komut.Parameters.AddWithValue("@P2", decimal.Parse(txtMalzemeStok.Text));
            komut.Parameters.AddWithValue("@P3", decimal.Parse(txtMalzemeFiyat.Text));
            komut.Parameters.AddWithValue("@P4", txtNotlar.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Malzeme Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MalzemeListe();
        }

        private void btnÜrünekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLURUNLER (AD) values (@P1)", baglanti);
            komut.Parameters.AddWithValue("@P1", txtUrunAd.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün Sisteme Eklendi ","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLFIRIN (URUNID,MALZEMEID,MIKTAR,MALIYET) values (@p1,@p2,@p3,@p4)", baglanti);
            komut.Parameters.AddWithValue("@P1", cmbUrun.SelectedValue);
            komut.Parameters.AddWithValue("@P2",cmbmalzeme.SelectedValue);
            komut.Parameters.AddWithValue("@P3", decimal.Parse(txtMiktar.Text));
            komut.Parameters.AddWithValue("@P4", decimal.Parse(txtMaliyet.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Malzeme Eklendi","Bilgi",MessageBoxButtons.OK
                ,MessageBoxIcon.Information);
        }

        private void txtMiktar_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
