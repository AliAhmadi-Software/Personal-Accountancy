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
    public partial class frmListCheckD : Office2007Form
    {
        public frmListCheckD()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("data source=(local);initial catalog=PersonalAccountancy;integrated security=true;");
        SqlCommand cmd = new SqlCommand();

        void displayTarikh()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * From CheckD where SarResid between '" + mskTarikh1.Text + "' AND '" + mskTarikh2.Text + "'";
            adp.Fill(ds, "CheckD");
            dgvCheckD.DataSource = ds;
            dgvCheckD.DataMember = "CheckD";
            //*****************
            dgvCheckD.Columns[0].HeaderText = "کد";
            dgvCheckD.Columns[1].HeaderText = "شماره حساب";
            dgvCheckD.Columns[2].HeaderText = "نام حساب";
            dgvCheckD.Columns[3].HeaderText = "مبلغ چک";
            dgvCheckD.Columns[4].HeaderText = "در وجه";
            dgvCheckD.Columns[5].HeaderText = "صدور";
            dgvCheckD.Columns[6].HeaderText = "سررسید";
            dgvCheckD.Columns[2].Width = 150;
        }

        private void frmListCheckD_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh1.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
            mskTarikh2.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
            displayTarikh();
        }

        private void mskTarikh1_TextChanged(object sender, EventArgs e)
        {
            displayTarikh();
        }

        private void mskTarikh2_TextChanged(object sender, EventArgs e)
        {
            displayTarikh();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string MH;
            int MCheckD;
            con.Open();
            SqlCommand sqlcmd = new SqlCommand("select Mablagh from TBLBank where SHH='" + Convert.ToInt32(dgvCheckD.SelectedCells[1].Value) + "'", con);
            MH = Convert.ToString((int)sqlcmd.ExecuteScalar());//موجودی حساب 
            MCheckD = Convert.ToInt32(dgvCheckD.SelectedCells[3].Value);
  
                int NewMH = Int32.Parse(MH) + MCheckD;

                string UpdateMH = "Update TBLBank set Mablagh='" + NewMH + "' where SHH='" + Convert.ToInt32(dgvCheckD.SelectedCells[1].Value) + "'";
                SqlCommand com = new SqlCommand(UpdateMH, con);
                com.ExecuteNonQuery();
                MessageBox.Show("وصول چک دریافتی انجام شد و مبلغ چک به حساب مورد نظر افزوده شد", "وصول چک");
            con.Close();
        }
    }
}
