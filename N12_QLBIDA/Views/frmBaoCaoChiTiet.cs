using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace N12_QLBIDA
{
    public partial class frmBaoCaoChiTiet : Form
    {
        public frmBaoCaoChiTiet()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            rptrptCTHD hd= new rptrptCTHD();
            hd.SetDatabaseLogon("sa", "123", "DESKTOP-LSFEJMC\\SQLEXPRESS", "BiDa");
            crystalReportViewer1.ReportSource= hd;
            crystalReportViewer1.Refresh();
        }
    }
}
