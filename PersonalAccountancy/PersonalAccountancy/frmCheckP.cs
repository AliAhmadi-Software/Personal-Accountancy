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
    public partial class frmCheckP : Office2007Form
    {
        public frmCheckP()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("data source=(local);initial catalog=PersonalAccountancy;integrated security=true;");
        SqlCommand cmd = new SqlCommand();
        private void frmCheckP_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
            mskSarResid.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
        }

        private void btnSMB_Click(object sender, EventArgs e)
        {
            SqlDataReader dr;
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from TBLBank where ShH=@s";
            cmd.Parameters.AddWithValue("@s", txtSh.Text);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtName.Text = dr["NameHesab"].ToString();
                txtSh.Text = dr["ShH"].ToString();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "insert into CheckP(ShH,NameHesab,Mablagh,NameSh,Tarikh,SarResid)values(@a,@b,@c,@d,@e,@f)";
            cmd.Parameters.AddWithValue("@a", txtSh.Text);
            cmd.Parameters.AddWithValue("@b", txtName.Text);
            cmd.Parameters.AddWithValue("@c", txtMablagh.Text);
            cmd.Parameters.AddWithValue("@d", txtNameSh.Text);
            cmd.Parameters.AddWithValue("@e", mskTarikh.Text);
            cmd.Parameters.AddWithValue("@f", mskSarResid.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("ثبت چک پرداختی انجام شد");
            //**************************************
            txtSh.Text = "";
            txtName.Text = "";
            txtMablagh.Text = "";
            txtNameSh.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "Delete from CheckP where id="+txtCode.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("حذف چک پرداختی انجام شد");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "Update CheckP set ShH='"+txtSh.Text+ "',NameHesab='" + txtName.Text + "',Mablagh='" + txtMablagh.Text + "',NameSh='" + txtNameSh.Text + "',Tarikh='" + mskTarikh.Text + "',SarResid='" + mskSarResid.Text + "' where id="+txtCode.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("ویرایش چک پرداختی انجام شد");
            //**************************************
            txtSh.Text = "";
            txtName.Text = "";
            txtMablagh.Text = "";
            txtNameSh.Text = "";
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            SqlDataReader dr;
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from CheckP where id=@s";
            cmd.Parameters.AddWithValue("@s", txtCode.Text);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtCode.Text = dr["id"].ToString();
                txtSh.Text = dr["ShH"].ToString();
                txtName.Text = dr["NameHesab"].ToString();
                txtMablagh.Text = dr["Mablagh"].ToString();
                txtNameSh.Text = dr["NameSh"].ToString();
                mskTarikh.Text = dr["Tarikh"].ToString();
                mskSarResid.Text = dr["SarResid"].ToString();
            }
            else
            {
                MessageBox.Show("چکی با این کد پیدا نشد");
                txtCode.Text = "";
                txtCode.Focus();
            }
            con.Close();
        }
    }
}
