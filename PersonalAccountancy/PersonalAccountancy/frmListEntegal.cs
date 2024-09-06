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
    public partial class frmListEntegal : Office2007Form
    {
        public frmListEntegal()
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
            adp.SelectCommand.CommandText = "select * from Entegal";
            adp.Fill(ds, "Entegal");
            dgvEntegal.DataSource = ds;
            dgvEntegal.DataMember = "Entegal";
            //*****************
            dgvEntegal.Columns[0].HeaderText = "کد";
            dgvEntegal.Columns[1].HeaderText = "شماره حساب مبدا";
            dgvEntegal.Columns[2].HeaderText = "نام حساب مبدا";
            dgvEntegal.Columns[3].HeaderText = "مبلغ انتقالی";
            dgvEntegal.Columns[4].HeaderText = "شماره حساب مقصد";
            dgvEntegal.Columns[5].HeaderText = "نام حساب مقصد";
            dgvEntegal.Columns[6].HeaderText = "تاریخ انتقال";
            dgvEntegal.Columns[7].HeaderText = "توضیحات";
            dgvEntegal.Columns[7].Width = 200;
        }

        void displayTarikh()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Entegal where Tarikh between '"+mskTarikh1.Text+ "' AND '" + mskTarikh2.Text + "'";
            adp.Fill(ds, "Entegal");
            dgvEntegal.DataSource = ds;
            dgvEntegal.DataMember = "Entegal";
            //*****************
            dgvEntegal.Columns[0].HeaderText = "کد";
            dgvEntegal.Columns[1].HeaderText = "شماره حساب مبدا";
            dgvEntegal.Columns[2].HeaderText = "نام حساب مبدا";
            dgvEntegal.Columns[3].HeaderText = "مبلغ انتقالی";
            dgvEntegal.Columns[4].HeaderText = "شماره حساب مقصد";
            dgvEntegal.Columns[5].HeaderText = "نام حساب مقصد";
            dgvEntegal.Columns[6].HeaderText = "تاریخ انتقال";
            dgvEntegal.Columns[7].HeaderText = "توضیحات";
            dgvEntegal.Columns[7].Width = 200;
        }

        private void frmListEntegal_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh1.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
            mskTarikh2.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
            display();
        }

        private void txtNameHMB_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Entegal where NameHMB like '%' + @S + '%'";
            adp.SelectCommand.Parameters.AddWithValue("@S", txtNameHMB.Text + "%");
            adp.Fill(ds, "Entegal");
            dgvEntegal.DataSource = ds;
            dgvEntegal.DataMember = "Entegal";
        }

        private void txtNameHMG_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from Entegal where NameHMG like '%' + @S + '%'";
            adp.SelectCommand.Parameters.AddWithValue("@S", txtNameHMG.Text + "%");
            adp.Fill(ds, "Entegal");
            dgvEntegal.DataSource = ds;
            dgvEntegal.DataMember = "Entegal";
        }

        private void mskTarikh1_TextChanged(object sender, EventArgs e)
        {
            displayTarikh();
        }

        private void mskTarikh2_TextChanged(object sender, EventArgs e)
        {
            displayTarikh();
        }
    }
}
