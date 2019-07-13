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
using DevExpress.Charts;
namespace Ticari_Otomasyon
{
    public partial class FrmKasalar : Form
    {
        public FrmKasalar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void musterihareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("EXECUTE MusteriHareketler",bgl.baglanti());

            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void firmahareket()
        {

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("EXECUTE FirmaHareketler",bgl.baglanti());
            da2.Fill(dt2);
            gridControl3.DataSource = dt2;


        }
        void giderler()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM TBL_GIDERLER",bgl.baglanti());
            da2.Fill(dt);
            gridControl2.DataSource = dt;

        }
        public string ad;
        private void FrmKasalar_Load(object sender, EventArgs e)
        {
            LblAktifKullanici.Text = ad;
            musterihareket();
            firmahareket();
            giderler();

            //Toplam Tutarı Hesaplama
            SqlCommand komut1 = new SqlCommand("Select Sum(Tutar) from TBL_FATURADETAY",bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                LblKasaToplam.Text = dr1[0].ToString()  +   " TL";

            }
            bgl.baglanti().Close();
            // SON AYIN FATURALARI
            SqlCommand komut2 = new SqlCommand("SELECT (ELEKTRIK+SU+DOGALGAZ+INTERNET+EKSTRA) FROM TBL_GIDERLER ORDER BY ID ASC",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {

                LblOdemeler.Text = dr2[0].ToString() + " TL";

            }
            bgl.baglanti().Close();

            //SON AYIN PERSONEL MAAŞLARI
            SqlCommand komut3 = new SqlCommand("SELECT MAASLAR FROM TBL_GIDERLER  ORDER BY ID ASC",bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                LblPersonelMaaslari.Text = dr3[0].ToString() + " TL";
            }
            bgl.baglanti().Close();

            //TOPLAM MÜŞTERİ SAYISI
            SqlCommand komut4 = new SqlCommand("SELECT COUNT(*) FROM TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                LblMusteriSayisi.Text = dr4[0].ToString();
            }
            bgl.baglanti().Close();

            //TOPLAM FİRMA SAYISI
            SqlCommand komut5 = new SqlCommand("SELECT COUNT(*) FROM TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                LblFirmaSayisi.Text = dr5[0].ToString();
            }
            bgl.baglanti().Close();

            //TOPLAM FİRMA ŞEHİR SAYISI
            SqlCommand komut6 = new SqlCommand("SELECT COUNT(DISTINCT(IL)) FROM TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                LblSehirSayisi.Text = dr6[0].ToString();
            }
            bgl.baglanti().Close();

            //TOPLAM MÜŞTERİ ŞEHİR SAYISI
            SqlCommand komut7 = new SqlCommand("SELECT COUNT(DISTINCT(IL)) FROM TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                LblSehirSayisi2.Text = dr7[0].ToString();
            }
            bgl.baglanti().Close();

            //TOPLAM PERSONEL SAYISI
            SqlCommand komut8 = new SqlCommand("SELECT COUNT(*) FROM TBL_PERSONELLER", bgl.baglanti());
            SqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
                LblPersonelSayisi.Text = dr8[0].ToString();
            }
            bgl.baglanti().Close();

            //TOPLAM ÜRÜN SAYISI
            SqlCommand komut9 = new SqlCommand("SELECT SUM(ADET) FROM TBL_URUNLER", bgl.baglanti());
            SqlDataReader dr9 = komut9.ExecuteReader();
            while (dr9.Read())
            {
                LblStokSayisi.Text = dr9[0].ToString();
            }
            bgl.baglanti().Close();

            

        }
        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            //ELEKTRİK
            if (sayac>0 && sayac<=5)
            {
             
                groupControl10.Text = "ELEKTRİK";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("SELECT TOP 4 AY,ELEKTRIK FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {

                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));

                }
                bgl.baglanti().Close();

                
            }
            //SU
            if (sayac>5 && sayac<=10)
            {
                groupControl10.Text = "SU";
                chartControl1.Series["Aylar"].Points.Clear();
               
                SqlCommand komut11 = new SqlCommand("SELECT TOP 4 AY,SU FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {

                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));

                }
                bgl.baglanti().Close();

            }
            //DOĞALGAZ

            if (sayac > 10 && sayac <= 15)
            {
                groupControl10.Text = "DOĞALGAZ";
                chartControl1.Series["Aylar"].Points.Clear();
                //CHART CONTROLE SU FATURASI SON 4 AY LISTELE
                SqlCommand komut11 = new SqlCommand("SELECT TOP 4 AY,DOGALGAZ FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {

                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));

                }
                bgl.baglanti().Close();

            }
            //İNTERNET

            if (sayac > 15 && sayac <= 20)
            {
                groupControl10.Text = "İNTERNET";
                chartControl1.Series["Aylar"].Points.Clear();
                //CHART CONTROLE SU FATURASI SON 4 AY LISTELE
                SqlCommand komut11 = new SqlCommand("SELECT TOP 4 AY,INTERNET FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {

                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));

                }
                bgl.baglanti().Close();

            }
            //EKSTRA

            if (sayac > 20 && sayac <= 25)
            {
                groupControl10.Text = "EKSTRA HARCAMALAR";
                chartControl1.Series["Aylar"].Points.Clear();
                //CHART CONTROLE SU FATURASI SON 4 AY LISTELE
                SqlCommand komut11 = new SqlCommand("SELECT TOP 4 AY,EKSTRA FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {

                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));

                }
                bgl.baglanti().Close();

            }
            if (sayac == 26)
            {

                sayac = 0;
            }
        }
        int sayac2;
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++;
            //ELEKTRİK
            if (sayac2 > 0 && sayac2 <= 5)
            {

                groupControl11.Text = "ELEKTRİK";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("SELECT TOP 4 AY,ELEKTRIK FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {

                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));

                }
                bgl.baglanti().Close();


            }
            //SU
            if (sayac2 > 5 && sayac2 <= 10)
            {
                groupControl11.Text = "SU";
                chartControl2.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("SELECT TOP 4 AY,SU FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {

                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));

                }
                bgl.baglanti().Close();

            }
            //DOĞALGAZ

            if (sayac2 > 10 && sayac2 <= 15)
            {
                groupControl11.Text = "DOĞALGAZ";
                chartControl2.Series["Aylar"].Points.Clear();
                //CHART CONTROLE SU FATURASI SON 4 AY LISTELE
                SqlCommand komut11 = new SqlCommand("SELECT TOP 4 AY,DOGALGAZ FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {

                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));

                }
                bgl.baglanti().Close();

            }
            //İNTERNET

            if (sayac2 > 15 && sayac2 <= 20)
            {
                groupControl11.Text = "İNTERNET";
                chartControl2.Series["Aylar"].Points.Clear();
                //CHART CONTROLE SU FATURASI SON 4 AY LISTELE
                SqlCommand komut11 = new SqlCommand("SELECT TOP 4 AY,INTERNET FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {

                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));

                }
                bgl.baglanti().Close();

            }
            //EKSTRA

            if (sayac2 > 20 && sayac2 <= 25)
            {
                groupControl11.Text = "EKSTRA HARCAMALAR";
                chartControl2.Series["Aylar"].Points.Clear();
                //CHART CONTROLE SU FATURASI SON 4 AY LISTELE
                SqlCommand komut11 = new SqlCommand("SELECT TOP 4 AY,EKSTRA FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {

                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));

                }
                bgl.baglanti().Close();

            }
            if (sayac2 == 26)
            {

                sayac2 = 0;
            }
        }
    }
}
