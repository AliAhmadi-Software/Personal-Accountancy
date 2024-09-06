using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Data.SqlClient;

namespace PersonalAccountancy
{
    public partial class frmEntegal : Office2007Form
    {
        public frmEntegal()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("data source=(local);initial catalog=PersonalAccountancy;integrated security=true;");
        SqlCommand cmd = new SqlCommand();

        private void frmEntegal_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh.Text = p.GetYear(DateTime.Now).ToString() +  p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string MHMB;
            int MHE;
            con.Open();
            SqlCommand sqlcmd = new SqlCommand("select Mablagh from TBLBank where SHH='"+txtShMb.Text+"'",con);
            MHMB = Convert.ToString((int)sqlcmd.ExecuteScalar());//موجودی حساب مبدا
            MHE = Convert.ToInt32(txtMablagh.Text);
            if (MHE > Convert.ToInt32(MHMB))
            {
                MessageBox.Show("موجودی حساب مبدا کافی نمی باشد","موجودی کافی نیست");
                return;
            }
            else
            {
                int NewMHMB = Int32.Parse(MHMB) - MHE;

                string UpdateMHMB = "Update TBLBank set Mablagh='"+NewMHMB+ "' where SHH='" + txtShMb.Text + "'";
                SqlCommand com = new SqlCommand(UpdateMHMB,con);
                com.ExecuteNonQuery();

                string MHMG;
                SqlCommand sqlcmdMG = new SqlCommand("select Mablagh from TBLBank where SHH='" + txtShMG.Text + "'", con);
                MHMG = Convert.ToString((int)sqlcmdMG.ExecuteScalar());//موجودی حساب مقصد

                int NewMHMG = Int32.Parse(MHMG) + MHE;
                string UpdateMHMG = "Update TBLBank set Mablagh='" + NewMHMG + "' where SHH='" + txtShMG.Text + "'";
                SqlCommand comMG = new SqlCommand(UpdateMHMG, con);
                comMG.ExecuteNonQuery();

                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "insert into Entegal (ShHMB,NameHMB,Mablagh,ShHMG,NameHMG,Tarikh,Tozih)values(@a,@b,@c,@d,@e,@f,@g)";
                cmd.Parameters.AddWithValue("@a",txtShMb.Text);
                cmd.Parameters.AddWithValue("@b", txtNameMB.Text);
                cmd.Parameters.AddWithValue("@c", txtMablagh.Text);
                cmd.Parameters.AddWithValue("@d", txtShMG.Text);
                cmd.Parameters.AddWithValue("@e", txtNameMG.Text);
                cmd.Parameters.AddWithValue("@f", mskTarikh.Text);
                cmd.Parameters.AddWithValue("@g", txtTozih.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("انتقال وجه انجام شد");
                //***************************************
                txtShMb.Text = "";
                txtNameMB.Text = "";
                txtMablagh.Text = "";
                txtShMG.Text = "";
                txtNameMG.Text = "";
                mskTarikh.Text = "";
                txtTozih.Text = "";
            }

        }

        private void btnSMB_Click(object sender, EventArgs e)
        {
            SqlDataReader dr;
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from TBLBank where ShH=@s";
            cmd.Parameters.AddWithValue("@s", txtShMb.Text);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
               // txtCode.Text = dr["id"].ToString();
                txtNameMB.Text = dr["NameHesab"].ToString();
                txtShMb.Text = dr["ShH"].ToString();
                txtMablagh.Text = dr["Mablagh"].ToString();
            }
            else
            {
                MessageBox.Show("حسابی با این کد پیدا نشد");
                txtCode.Text = "";
                txtCode.Focus();
            }
            con.Close();
        }

        private void btnSMG_Click(object sender, EventArgs e)
        {
            SqlDataReader dr;
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from TBLBank where ShH=@s";
            cmd.Parameters.AddWithValue("@s", txtShMG.Text);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                //txtCode.Text = dr["id"].ToString();
                txtNameMG.Text = dr["NameHesab"].ToString();
                txtShMG.Text = dr["ShH"].ToString();
            }
            else
            {
                MessageBox.Show("حسابی با این کد پیدا نشد");
                txtCode.Text = "";
                txtCode.Focus();
            }
            con.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "Delete from Entegal where id="+txtCode.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("اطلاعات حذف شد");
        }

        private void btnSE_Click(object sender, EventArgs e)
        {
            SqlDataReader dr;
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from Entegal where id=@s";
            cmd.Parameters.AddWithValue("@s", txtCode.Text);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtCode.Text = dr["id"].ToString();
                txtShMb.Text = dr["ShHMB"].ToString();
                txtNameMB.Text = dr["NameHMB"].ToString();
                txtMablagh.Text = dr["Mablagh"].ToString();
                txtShMG.Text = dr["ShHMG"].ToString();
                txtNameMG.Text = dr["NameHMG"].ToString();
                mskTarikh.Text = dr["Tarikh"].ToString();
                txtTozih.Text = dr["Tozih"].ToString();
            }
            else
            {
                MessageBox.Show("اطلاعاتی با این کد پیدا نشد");
                txtCode.Text = "";
                txtCode.Focus();
            }
            con.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void mskTarikh_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
