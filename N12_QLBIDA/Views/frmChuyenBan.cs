using N12_QLBIDA.Models;
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
    public partial class frmChuyenBan : Form
    {
        public DialogResult dr { get; set; }
        string deskname = "";
        string deskid = "";
        string deskfee = "";
        string timebegin = "";
        string totalminute = "";
        string fee = "";
        string receiptionid = "";


        public frmChuyenBan(string deskname, string deskid, string deskfee, string timebegin, string totalminute, string fee, string receiptionid)
        {
            this.deskname = deskname;
            this.deskid = deskid;
            this.deskfee = deskfee;
            this.timebegin = timebegin;
            this.totalminute = totalminute;
            this.fee = fee;
            this.receiptionid = receiptionid;
            InitializeComponent();
        }

        private void frmTransferDesk_Load(object sender, EventArgs e)
        {
            lbDeskname.Text = deskname;
            lbDeskfee.Text = deskfee;
            lbTimebegin.Text = timebegin;
            lbTotalminute.Text = totalminute;
            lbFee.Text = fee;
            loadDeskReady();
        }
        private void loadDeskReady()
        {
            DataTable dtable = Controllers.BanCtrl.findByStatus(Models.constant.status_ready).Tables[0];
            if (dtable.Rows.Count == 0)
            {
                cmbDesk.Text = "Không có bàn!";
                btnOk.Enabled = false;
            }
            else
            {
                List<Models.cmbSelection> lstDesk = new List<Models.cmbSelection>();
                for (int i = 0; i < dtable.Rows.Count; i++)//chỗ này
                {
                    lstDesk.Add(new Models.cmbSelection(long.Parse(dtable.Rows[i]["maban"].ToString()), dtable.Rows[i]["tenban"].ToString()));
                }

                //for (int i = 0; i < dtable.Rows.Count; i++)
                //{
                //    long maBan = long.Parse(dtable.Rows[i]["maban"].ToString());
                //    string tenBan = dtable.Rows[i]["tenban"].ToString();

                //    // Kiểm tra nếu bàn không phải là bàn số 5
                //    if (maBan != null)
                //    {
                //        lstDesk.Add(new Models.cmbSelection(maBan, tenBan));
                //    }
                //}



                cmbDesk.DataSource = lstDesk;
                cmbDesk.DisplayMember = "name";
                cmbDesk.ValueMember = "lid";
            }
        }
        private int transferDesk()
        {
            long idfrom = long.Parse(deskid);
            Models.cmbSelection to = cmbDesk.SelectedItem as Models.cmbSelection;
            long idto = to.lid;
            DataTable dtable = Controllers.BanCtrl.findById(idto).Tables[0];

            if (dtable.Rows.Count > 0) 
            {
                Controllers.BanCtrl.updateStatus(idfrom, Models.constant.status_ready);
                Controllers.BanCtrl.updateStatus(idto, Models.constant.status_busy);

                string timebegin = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                int result = Controllers.HoaDonCtrl.updateTransfer(long.Parse(receiptionid), idto, dtable.Rows[0]["tenban"].ToString(), float.Parse(dtable.Rows[0]["giaban"].ToString()), timebegin);
                if (result > 0)
                {
                    long detailid = Controllers.CTHDlCtrl.generalid();
                    if (detailid > 0)
                    {
                        int quantum = int.Parse(totalminute.Replace("phút", "").Replace(" ", ""));
                        float total = float.Parse(fee.Replace(",", "").Replace("đ", "").Replace(" ", ""));
                        Controllers.CTHDlCtrl.insert(detailid, long.Parse(receiptionid), deskname + " chuyển qua", "Phút", 0, quantum, total);
                    }
                }
                MessageBox.Show("Chuyển bàn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return 1;
            }
            else
            {
                MessageBox.Show("Chuyển bàn không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return 0;
            }

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.dr = DialogResult.No;
            this.Close();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult ok = new DialogResult();
            ok = MessageBox.Show("Bạn có chắc muốn chuyển bàn Không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ok == DialogResult.Yes && receiptionid != "")
            {
                int trs = transferDesk();
                if (trs > 0)
                {
                    this.dr = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                return;
            }
        }
        //private void loadDeskReady()
        //{
        //    DataTable dtable = findByStatus(Models.constant.status_ready).Tables[0];
        //    if (dtable.Rows.Count == 0)
        //    {
        //        cmbDesk.Text = "Không có bàn!";
        //        btnOk.Enabled = false;
        //    }
        //    else
        //    {
        //        List<Models.cmbSelection> lstDesk = new List<Models.cmbSelection>();
        //        for (int i = 0; i < dtable.Rows.Count; i++)
        //            lstDesk.Add(new Models.cmbSelection(long.Parse(dtable.Rows[i]["maban"].ToString()), dtable.Rows[i]["tenban"].ToString()));

        //        cmbDesk.DataSource = lstDesk;
        //        cmbDesk.DisplayMember = "name";
        //        cmbDesk.ValueMember = "lid";
        //    }
        //}
        //private int transferDesk()
        //{
        //    long idfrom = long.Parse(deskid);
        //    Models.cmbSelection to = cmbDesk.SelectedItem as Models.cmbSelection;
        //    long idto = to.lid;
        //    DataTable dtable = Controllers.BanCtrl.findById(idto).Tables[0];
        //    if (dtable.Rows.Count > 0)
        //    {
        //        Controllers.BanCtrl.updateStatus(idfrom, Models.constant.status_ready);
        //        Controllers.BanCtrl.updateStatus(idto, Models.constant.status_busy);
        //        string timebegin = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        //        int result = updateTransfer(long.Parse(receiptionid), idto, dtable.Rows[0]["tenban"].ToString(), float.Parse(dtable.Rows[0]["giaban"].ToString()), timebegin);
        //        if (result > 0)
        //        {
        //            long detailid = generalid();
        //            if (detailid > 0)
        //            {
        //                int quantum = int.Parse(totalminute.Replace("phút", "").Replace(" ", ""));
        //                float total = float.Parse(fee.Replace(",", "").Replace("đ", "").Replace(" ", ""));
        //                Controllers.CTHDlCtrl.insert(detailid, long.Parse(receiptionid), deskname + " chuyển qua", "Phút", 0, quantum, total);
        //            }
        //        }
        //        MessageBox.Show("Chuyển bàn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        //        return 1;
        //    }
        //    else
        //    {
        //        MessageBox.Show("Chuyển bàn không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        //        return 0;
        //    }


        //}

        //public int updateTransfer(long id, long deskid, string deskname, float deskfee, string timebegin)
        //{
        //    string[] paras = new string[5] { "@id", "@deskid", "@deskname", "@deskfee", "@timebegin" };
        //    object[] values = new object[5] { id, deskid, deskname, deskfee, timebegin };
        //    return connection.Excute_Sql(constant.transfer_Receiption, CommandType.StoredProcedure, paras, values);
        //}
        //public DataSet findByStatus(int _status)
        //{
        //    DataSet ds = new DataSet();
        //    string[] paras = new string[1] { "@status" };
        //    object[] values = new object[1] { _status };
        //    ds = connection.FillDataSet(constant.findByStatus_Desk, CommandType.StoredProcedure, paras, values);
        //    return ds;
        //}
        //public int updateStatus(long id, int status)
        //{
        //    int i = 0;
        //    string[] paras = new string[2] { "@id", "@status" };
        //    object[] values = new object[2] { id, status };
        //    i = connection.Excute_Sql(constant.updateStatus_Desk, CommandType.StoredProcedure, paras, values);
        //    return i;
        //}
        //protected long id { get; set; }
        
        //protected string name { get; set; }
        //protected string unit { get; set; }
        //protected float price { get; set; }
        //protected int quantum { get; set; }
        //protected float total { get; set; }

        //public long generalid()
        //{
        //    return long.Parse(connection.ExcuteScalar(String.Format(constant.createid_Receiptiondetail)));
        //}
        //public int insert()
        //{
        //    string[] paras = new string[7] { "@id", "@receiptionid", "@name", "@unit", "@price", "@quantum", "@total" };
        //    object[] values = new object[7] { id, receiptionid, name, unit, price, quantum, total };
        //    return connection.Excute_Sql(constant.insert_Receiptiondetail, CommandType.StoredProcedure, paras, values);

        //}
    }
}
