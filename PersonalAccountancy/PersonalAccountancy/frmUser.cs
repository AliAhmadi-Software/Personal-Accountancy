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
    public partial class frmUser : Office2007Form
    {
        public frmUser()
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
            adp.SelectCommand.CommandText = "select * from TBLUser";
            adp.Fill(ds,"TBLUser");
            dgvUser.DataSource = ds;
            dgvUser.DataMember = "TBLUser";
            //*****************************
            dgvUser.Columns[0].HeaderText = "کد";
            dgvUser.Columns[1].HeaderText = "نام کاربری";
            dgvUser.Columns[2].HeaderText = "کلمه عبور";
            dgvUser.Columns[3].HeaderText = "سطح دسترسی";
        }
        private void frmUser_Load(object sender, EventArgs e)
        {
            display();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "insert into TBLUser(UName,Pass,Noo)values(@UName,@Pass,@Noo)";
            cmd.Parameters.AddWithValue("@UName", txtUName.Text);
            cmd.Parameters.AddWithValue("@Pass", txtPass.Text);
            cmd.Parameters.AddWithValue("@Noo",  cmbNoo.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            display();
            MessageBox.Show("کاربر با موفقیت ثبت شد");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(dgvUser.SelectedCells[0].Value);
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "Delete from TBLUser where id=@N";
            cmd.Parameters.AddWithValue("@N",x);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            display();
            MessageBox.Show("کاربر با موفقیت حذف شد");
        }
    }
}
