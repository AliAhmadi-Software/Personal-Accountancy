using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevComponents.DotNetBar;

namespace PersonalAccountancy
{
    public partial class frmBank : Office2007Form
    {
        public frmBank()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("data source=(local);initial catalog=PersonalAccountancy;integrated security=true;");
        SqlCommand cmd = new SqlCommand();
        private void frmBank_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "Insert into TBLBank(NameHesab,ShH,Mablagh,NameBank,Tozih)values(@a,@b,@c,@d,@e)";
            cmd.Parameters.AddWithValue("@a", txtName.Text);
            cmd.Parameters.AddWithValue("@b", txtSh.Text);
            cmd.Parameters.AddWithValue("@c", txtMablagh.Text);
            cmd.Parameters.AddWithValue("@d", txtNameB.Text);
            cmd.Parameters.AddWithValue("@e", txtTozih.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("بانک با موفقیت ثبت شد");
            //******************************************
            txtCode.Text = "";
            txtName.Text = "";
            txtSh.Text = "";
            txtMablagh.Text = "";
            txtNameB.Text = "";
            txtTozih.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "Delete from TBLBank where id="+txtCode.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("بانک با موفقیت حذف شد");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "Update TBLBank Set NameHesab='"+txtName.Text+ "',ShH='" + txtSh.Text + "',Mablagh='" + txtMablagh.Text + "',NameBank='" + txtNameB.Text + "',Tozih='" + txtTozih.Text + "' where id="+txtCode.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("بانک با موفقیت ویرایش شد");
            //******************************************
            txtCode.Text = "";
            txtName.Text = "";
            txtSh.Text = "";
            txtMablagh.Text = "";
            txtNameB.Text = "";
            txtTozih.Text = "";
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            SqlDataReader dr;
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from TBLBank where id=@s";
            cmd.Parameters.AddWithValue("@s",txtCode.Text);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtCode.Text = dr["id"].ToString();
                txtName.Text = dr["NameHesab"].ToString();
                txtSh.Text = dr["ShH"].ToString();
                txtMablagh.Text = dr["Mablagh"].ToString();
                txtNameB.Text = dr["NameBank"].ToString();
                txtTozih.Text = dr["Tozih"].ToString();
            }
            else
            {
                MessageBox.Show("حسابی با این کد پیدا نشد");
                txtCode.Text = "";
                txtCode.Focus();
            }
            con.Close();
        }
    }
}
