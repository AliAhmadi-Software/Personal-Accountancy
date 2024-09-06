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
    public partial class frmLogin : Office2007Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("data source=(local);initial catalog=PersonalAccountancy;integrated security=true;");
        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();
        private void btnIn_Click(object sender, EventArgs e)
        {
            string struser, search;
            if (cmbNoo.SelectedItem=="مدیر")
            {
                struser = "admin";
                clc_variable.stru = "مدیر";
            }
            else
            {
                struser = "user";
                clc_variable.stru = "کاربر";
            }
            search = "select id  from TBLUser where Noo='"+struser+"' AND UName='"+txtUName.Text+ "' AND Pass='" + txtPass.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(search,con);
            da.Fill(ds,"TBLUser");
            if (ds.Tables["TBLUser"].Rows.Count > 0 )
            {
                new Form1().ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("کاربری با این مشخصات وجود ندارد");
            }
            con.Close();
        }
    }
}
