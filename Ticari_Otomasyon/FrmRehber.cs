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

namespace Ticari_Otomasyon
{
    public partial class FrmRehber : Form
    {
        public FrmRehber()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmRehber_Load(object sender, EventArgs e)
        {
            //Müşteri Bilgileri

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select AD,SOYAD,TELEFON,TELEFON2,MAIL from  TBL_MUSTERILER ",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

            // Firma Bilgileri

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select AD,YETKILIADSOYAD,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX from TBL_FIRMALAR",bgl.baglanti());
            da2.Fill(dt2);
            gridControl2.DataSource = dt2;

          //  this.reportViewer1.RefreshReport();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e) // ÇİFT TIKLAYINCA MAIL İLE DOLDURSUN
        {

            FrmMail frm = new FrmMail();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr!=null){

                frm.mail = dr["MAIL"].ToString();


            }
            frm.Show();

        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {

            FrmMail frm = new FrmMail();
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);

            if (dr != null)
            {

                frm.mail = dr["MAIL"].ToString();


            }
            frm.Show();

        }
    }
}
