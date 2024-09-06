using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PersonalAccountancy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblUser.Text = clc_variable.stru;
            if (lblUser.Text == "کاربر")
            {
                btnUser.Enabled = false;
            }
            System.Globalization.PersianCalendar p =new System.Globalization.PersianCalendar();
            lblDate.Text = p.GetYear(DateTime.Now).ToString() + "/" + p.GetMonth(DateTime.Now).ToString("0#") +"/"+ p.GetDayOfMonth(DateTime.Now).ToString("0#");
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            new frmUser().ShowDialog();
        }

        private void btnBank_Click(object sender, EventArgs e)
        {
            new frmBank().ShowDialog();
        }

        private void btnListBank_Click(object sender, EventArgs e)
        {
            new frmListBank().ShowDialog();
        }

        private void btnEntegal_Click(object sender, EventArgs e)
        {
            new frmEntegal().ShowDialog();
        }

        private void btnListEntegal_Click(object sender, EventArgs e)
        {
            new frmListEntegal().ShowDialog();
        }

        private void btnCheckP_Click(object sender, EventArgs e)
        {
            new frmCheckP().ShowDialog();
        }

        private void btnListCheckP_Click(object sender, EventArgs e)
        {
            new frmListCheckP().ShowDialog();
        }

        private void btnCheckD_Click(object sender, EventArgs e)
        {
            new frmChekD().ShowDialog();
        }

        private void btnListCheckD_Click(object sender, EventArgs e)
        {
            new frmListCheckD().ShowDialog();
        }
    }
}
