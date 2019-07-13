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
using System.Xml;

namespace Ticari_Otomasyon
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void stoklar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT URUNAD,SUM(ADET) AS 'ADET' FROM TBL_URUNLER GROUP BY URUNAD HAVING SUM(ADET)<=20 ORDER BY SUM(ADET)", bgl.baglanti());
            da.Fill(dt);
           GridControlStoklar.DataSource = dt;
        }

        void ajanda()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 10 TARIH,SAAT,BASLIK FROM TBL_NOTLAR ORDER BY ID DESC",bgl.baglanti());
            da.Fill(dt);
            GridControlAjanda.DataSource = dt;


        }
        void FirmaHareketleri()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("exec FirmaHareket2", bgl.baglanti());
            da.Fill(dt);
            GridControlFirmaHareket.DataSource = dt;


        }

        void fihrist()
        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT AD,TELEFON1 FROM TBL_FIRMALAR",bgl.baglanti());
            da.Fill(dt);
            GridControlFihrist.DataSource = dt;

        }
        void haberler()
        {

            XmlTextReader xmloku = new XmlTextReader("http://www.hurriyet.com.tr/rss/anasayfa");
            while (xmloku.Read())
            {
                if (xmloku.Name == "title")
                {
                    listBox1.Items.Add(xmloku.ReadString());

                }

            }



        }
        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            stoklar();
            ajanda();
            fihrist();
            FirmaHareketleri();
            webBrowser1.Navigate("http://www.tcmb.gov.tr/kurlar/today.xml");
            haberler();

        }
    }
}
