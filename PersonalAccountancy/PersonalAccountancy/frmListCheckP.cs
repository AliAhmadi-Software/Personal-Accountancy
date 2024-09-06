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
    public partial class frmListCheckP : Office2007Form
    {
        public frmListCheckP()
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
            adp.SelectCommand.CommandText = "select * from CheckP where SarResid between '" + mskTarikh1.Text + "' AND '" + mskTarikh2.Text + "'";
            adp.Fill(ds, "CheckP");
            dgvCheckP.DataSource = ds;
            dgvCheckP.DataMember = "CheckP";
            //*****************
            dgvCheckP.Columns[0].HeaderText = "کد";
            dgvCheckP.Columns[1].HeaderText = "شماره حساب";
            dgvCheckP.Columns[2].HeaderText = "نام حساب";
            dgvCheckP.Columns[3].HeaderText = "مبلغ چک";
            dgvCheckP.Columns[4].HeaderText = "در وجه";
            dgvCheckP.Columns[5].HeaderText = "صدور";
            dgvCheckP.Columns[6].HeaderText = "سررسید";
            dgvCheckP.Columns[2].Width = 150;
        }
        private void frmListCheckP_Load(object sender, EventArgs e)
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
            int MCheckP;
            con.Open();
            SqlCommand sqlcmd = new SqlCommand("select Mablagh from TBLBank where SHH='" + Convert.ToInt32(dgvCheckP.SelectedCells[1].Value) + "'", con);
            MH = Convert.ToString((int)sqlcmd.ExecuteScalar());//موجودی حساب 
            MCheckP = Convert.ToInt32(dgvCheckP.SelectedCells[3].Value);
            if (MCheckP > Convert.ToInt32(MH))
            {
                MessageBox.Show("موجودی حساب برای وصول این چک کافی نمی باشد", "موجودی کافی نیست");
                return;
            }
            else
            {
                int NewMH = Int32.Parse(MH) - MCheckP;

                string UpdateMH = "Update TBLBank set Mablagh='" + NewMH + "' where SHH='" + Convert.ToInt32(dgvCheckP.SelectedCells[1].Value) + "'";
                SqlCommand com = new SqlCommand(UpdateMH, con);
                com.ExecuteNonQuery();
                MessageBox.Show("وصول چک پرداختی انجام شد و مبلغ چک از حساب مورد نظر کسر شد", "وصول چک");
                con.Close();
            }
        }
    }
}
