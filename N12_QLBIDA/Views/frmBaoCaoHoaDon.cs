using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace N12_QLBIDA.Views
{
    public partial class frmBaoCaoHoaDon : Form
    {
        public frmBaoCaoHoaDon()
        {
            InitializeComponent();
        }

        private void frmBaoCaoHoaDon_Load(object sender, EventArgs e)
        {
            rptHoaDon hd = new rptHoaDon();
            //hd.SetDatabaseLogon("sa", "123", "DESKTOP-LSFEJMC\\SQLEXPRESS", "BiDa");
            crystalReportViewer1.ReportSource = hd;
            crystalReportViewer1.Refresh();
        }
    }
}
