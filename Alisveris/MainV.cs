using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alisveris
{
    public partial class MainV : Form
    {
        public MainV()
        {
            InitializeComponent();
        }

        void Temizle()
        {
            txtBirimTutar.Clear(); txtMiktar.Clear(); txtUrunAd.Clear();
            lboSepetDetay.Items.Clear();
            cboKdv.SelectedIndex = -1; cboIskonto.SelectedIndex = 0; cboPersonelIndirim.SelectedIndex = 0;
            lblSepetTutar.Text = "";
        }

        const double ciroHedef = 500; // sabit değer
        private void Form1_Load(object sender, EventArgs e)
        {
            lboSepetDetay.Items.Add("Fiş No\tÜrün Adı\tFiyat");

            txtCiroHedef.Text = ciroHedef.ToString();

            cboIskonto.SelectedIndex = 0;
            cboPersonelIndirim.SelectedIndex = 0;
        }

       
        double sepetToplam = 0;

        private void btnUrunEkle_Click(object sender, EventArgs e)
        {
            if (txtUrunAd.Text == "" || txtMiktar.Text == "" || txtBirimTutar.Text == "" || cboKdv.SelectedIndex == -1)
            {
                MessageBox.Show("Ürün eklemek için ilgili alanları doldurunuz..");
            }
            else
            {
                string urunAd = txtUrunAd.Text;
                double miktar = Convert.ToDouble(txtMiktar.Text);
                double birimTutar = Convert.ToDouble(txtBirimTutar.Text);
                double toplamTutar = miktar * birimTutar;       
                int kdv =  Convert.ToInt32(cboKdv.Text);
                int iskonto =  Convert.ToInt32(cboIskonto.Text);
                int personelIndirim =  Convert.ToInt32(cboPersonelIndirim.Text);
                toplamTutar = toplamTutar + ((toplamTutar * kdv) / 100) - ((toplamTutar * iskonto) / 100) - ((toplamTutar * personelIndirim) / 100);
                sepetToplam += toplamTutar;
                lboSepetDetay.Items.Add(urunNumara + "\t" + urunAd + "\t" + toplamTutar);

                lblSepetTutar.Text = sepetToplam.ToString();
            }
        }

        private void btnUrunSil_Click(object sender, EventArgs e)
        {
            int secilen = lboSepetDetay.SelectedIndex;
            if (secilen != -1)
            {
                lboSepetDetay.Items.RemoveAt(secilen);
                MessageBox.Show("Seçilen ürün başarıyla silindi!");
            }
            else
            {
                MessageBox.Show("Seçim Yapın!");
            }
        }

        int urunNumara = 1;
        private void btnYeniKayit_Click(object sender, EventArgs e)
        {
            lblGunlukCiro.Text = sepetToplam.ToString();
            ++urunNumara;
            Temizle();

            if (sepetToplam >= ciroHedef)
            {
                lblMesaj.Text = "GÜNLÜK CİROYA ULAŞILDI!";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = $"Alışveriş Uygulaması | {DateTime.Now}";
        }
    }
}
