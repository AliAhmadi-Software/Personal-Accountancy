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
    public partial class frmListBank : Office2007Form
    {
        public frmListBank()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("data source=(local);initial catalog=PersonalAccountancy;integrated security=true;");
        SqlCommand cmd = new SqlCommand();

        void display()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from TBLBank";
            adp.Fill(ds,"TBLBank");
            dgvBank.DataSource = ds;
            dgvBank.DataMember = "TBLBank";
            //*****************
            dgvBank.Columns[0].HeaderText = "کد";
            dgvBank.Columns[1].HeaderText = "نام حساب";
            dgvBank.Columns[2].HeaderText = "شماره حساب";
            dgvBank.Columns[3].HeaderText = "موجودی اولیه";
            dgvBank.Columns[4].HeaderText = "نام بانک";
            dgvBank.Columns[5].HeaderText = "توضیحات";
            dgvBank.Columns[5].Width = 200;
        }
        private void frmListBank_Load(object sender, EventArgs e)
        {
            display();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from TBLBank where NameHesab like '%' + @S + '%'";
            adp.SelectCommand.Parameters.AddWithValue("@S",txtName.Text + "%");
            adp.Fill(ds, "TBLBank");
            dgvBank.DataSource = ds;
            dgvBank.DataMember = "TBLBank";
        }

        private void txtNameB_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from TBLBank where NameBank like '%' + @S + '%'";
            adp.SelectCommand.Parameters.AddWithValue("@S", txtNameB.Text + "%");
            adp.Fill(ds, "TBLBank");
            dgvBank.DataSource = ds;
            dgvBank.DataMember = "TBLBank";
        }
    }
}
