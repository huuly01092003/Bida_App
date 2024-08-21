using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace N12_QLBIDA.Views
{
    public partial class frmMain : Form
    {
        private int userStatus;
        public frmMain(int userStatus)
        {
            InitializeComponent();
            this.userStatus = userStatus;
            UpdateMenuStatus();
        }
        private void UpdateMenuStatus()
        {
            // Tùy thuộc vào giá trị trạng thái, bạn có thể thực hiện các tác vụ tương ứng
            if (userStatus == 0)
            {
                quảnLýHệThốngToolStripMenuItem.Enabled = false; // Disable ToolStripMenuItem
            }
            else
            {
                quảnLýHệThốngToolStripMenuItem.Enabled = true; // Enable ToolStripMenuItem
            }
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Application.Restart();
                //Application.Exit();
            }
            else
                return;
        }

        private void billardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBan frmb = new frmBan();
            frmb.ShowDialog();
        }

        private void thựcĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDichVu frmdv = new frmDichVu();
            frmdv.ShowDialog();
        }

        private void đổiMậtKhẩuToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDoiMatKhau frm = new frmDoiMatKhau();
            frm.ShowDialog();
        }

        private void bILLIARDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void tàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTaiKhoan user = new frmTaiKhoan();
            user.ShowDialog();
        }

        private void nhậtKýHoạtĐộngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBaoCaoHoaDon frmbchd = new frmBaoCaoHoaDon();
            frmbchd.ShowDialog();
        }

        private void baosCáoCTHDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBaoCaoChiTiet frmbcct = new frmBaoCaoChiTiet();
            frmbcct.ShowDialog();
        }

       
        //lấy ds thực đơn
        public void ShowListMenu()
        {
            dgvMenus.DataSource = Controllers.DichVuCtrl.findView().Tables[0];
            dgvMenus.Dock = DockStyle.Fill;
            dgvMenus.RowHeadersVisible = false;
            dgvMenus.BorderStyle = BorderStyle.Fixed3D;
            bindingMenu();
        }
        //biding data thực đơn
        public void bindingMenu()
        {
            txtDetailid.DataBindings.Clear();
            txtMenuid.DataBindings.Clear();
            txtMenuid.DataBindings.Add("Text", dgvMenus.DataSource, "MaDichVu");
            txtDetailName.DataBindings.Clear();
            txtDetailName.DataBindings.Add("Text", dgvMenus.DataSource, "TenDichVu");
            txtDetailUnit.DataBindings.Clear();
            txtDetailUnit.DataBindings.Add("Text", dgvMenus.DataSource, "DonViTinh");
            txtDetailPrice.DataBindings.Clear();
            txtDetailPrice.DataBindings.Add("Text", dgvMenus.DataSource, "price");
            txtDetailQuantum.Text = "1";
            txtDetailTotal.DataBindings.Clear();
            txtDetailTotal.DataBindings.Add("Text", dgvMenus.DataSource, "price");
            if (txtReceiptionid.Text != "")
            {
                btnAddDetail.Enabled = true;
            }
            else
            {
                btnAddDetail.Enabled = false;
            }
            btnEditDetail.Enabled = false;
            btnDelDetail.Enabled = false;
        }

        //load form
        private void frmMain_Load(object sender, EventArgs e)
        {
            ShowListMenu();
            ShowListDesk();
            refreshForm();
        }
        //hàm tìm bàn theo trạng thái truyền vào
        public DataTable getBillardByStatus(int status)
        {
            DataTable dt = new DataTable();
            if (status >= 0)
                dt = Controllers.BanCtrl.findByStatus(status).Tables[0];
            else
                dt = Controllers.BanCtrl.findAll().Tables[0];   //nếu status = -1 là tìm tất cả bàn
            return dt;
        }

        //làm mới giao diện hóa đơn
        public void cleanReceiption()
        {
            txtDeskid.Text = "";
            txtReceiptionid.Text = "";
            txtDetailid.Text = "";
            txtDeskname.Text = "";
            lbDeskfee.Text = "";
            lbTimebegin.Text = "";
            lbTotalminute.Text = "";
            lbFee.Text = "";
            lblTotal.Text = "";
            lbSumdetail.Text = "";
            cleanReceiptiondetail();
            dgvDetails.Refresh();
        }
        public void cleanReceiptiondetail()
        {
            txtDetailid.DataBindings.Clear();
            txtDetailName.DataBindings.Clear();
            txtDetailUnit.DataBindings.Clear();
            txtDetailPrice.DataBindings.Clear();
            txtDetailQuantum.DataBindings.Clear();
            txtDetailTotal.DataBindings.Clear();
        }

        //lấy ds các bàn bida lên
        public void ShowListDesk()
        {
            lvDesk.Items.Clear();
            DataTable dtable = new DataTable();

            //tìm tất cả bàn nếu trạng thái là -1
            dtable = getBillardByStatus(-1);
            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                //if (dtable.Rows[i]["maban"].ToString() != "1")
                {
                    ListViewItem item = new ListViewItem(dtable.Rows[i]["tenban"].ToString());
                    ListViewItem.ListViewSubItem subId = new ListViewItem.ListViewSubItem(item, dtable.Rows[i]["maban"].ToString());
                    ListViewItem.ListViewSubItem subName = new ListViewItem.ListViewSubItem(item, dtable.Rows[i]["tenban"].ToString());
                    ListViewItem.ListViewSubItem subStyle = new ListViewItem.ListViewSubItem(item, dtable.Rows[i]["trangthai"].ToString());
                    ListViewItem.ListViewSubItem subFee = new ListViewItem.ListViewSubItem(item, string.Format("{0:0,0}", decimal.Parse(dtable.Rows[i]["giaban"].ToString())));
                    item.SubItems.Add(subId);//sub index 1
                    item.SubItems.Add(subName);//sub index 2
                    item.SubItems.Add(subStyle);//sub index 3
                    item.SubItems.Add(subFee);//sub index 4
                    int status = int.Parse(dtable.Rows[i]["trangthai"].ToString());
                    int style = int.Parse(dtable.Rows[i]["loaiban"].ToString());
                    if (status == Models.constant.status_ready)
                    {
                        if (style == Models.constant.style_ball)
                            item.ImageIndex = 0;
                        else
                            item.ImageIndex = 2;
                    }
                    else if (status == Models.constant.status_busy)
                    {
                        if (style == Models.constant.style_ball)
                            item.ImageIndex = 1;
                        else
                            item.ImageIndex = 3;
                    }
                    else if (status == Models.constant.status_upgrade)
                        item.ImageIndex = 4;
                    lvDesk.Items.Add(item);
                }

               
            }

        }
        
        //lấy thông tin hóa đơn bàn đang chơi
        public void ShowBill()
        {
            if (txtDeskid.Text != "")
            {
                DataTable dtable = new DataTable();
                dtable = Controllers.HoaDonCtrl.findBydeskid(long.Parse(txtDeskid.Text)).Tables[0];
                //nếu bạn được chọn đang chơi
                if (dtable.Rows.Count > 0)
                {
                    txtReceiptionid.Text = dtable.Rows[0]["mahd"].ToString();
                    txtDeskid.Text = dtable.Rows[0]["maban"].ToString();
                    txtDeskname.Text = dtable.Rows[0]["tenban"].ToString();
                    lbDeskfee.Text = dtable.Rows[0]["deskfee"].ToString() + " đ";
                    lbTimebegin.Text = dtable.Rows[0]["timebegin"].ToString();
                }
            }
            if (txtReceiptionid.Text != "")
            {
                dgvDetails.DataSource = Controllers.CTHDlCtrl.findByreceiptionid(long.Parse(txtReceiptionid.Text)).Tables[0];
                dgvDetails.Dock = DockStyle.Fill;
                dgvDetails.RowHeadersVisible = false;
                dgvDetails.BorderStyle = BorderStyle.Fixed3D;
                bindingDetail();
                getPaid();
            }
        }
        public void bindingDetail()
        {
            cleanReceiptiondetail();
            if (dgvDetails.Rows.Count > 0)
            {
                txtDetailid.DataBindings.Add("Text", dgvDetails.DataSource, "macthd");
                txtDetailName.DataBindings.Add("Text", dgvDetails.DataSource, "tendichvu");
                txtDetailUnit.DataBindings.Add("Text", dgvDetails.DataSource, "donvitinh");
                txtDetailPrice.DataBindings.Add("Text", dgvDetails.DataSource, "Price");
                txtDetailQuantum.DataBindings.Add("Text", dgvDetails.DataSource, "soluongdichvu");
                txtDetailTotal.DataBindings.Add("Text", dgvDetails.DataSource, "total");
            }
            if (txtDetailid.Text != "")
            {
                btnAddDetail.Enabled = false;
                btnEditDetail.Enabled = true;
                btnDelDetail.Enabled = true;
            }
            else
            {
                btnAddDetail.Enabled = true;
                btnEditDetail.Enabled = false;
                btnDelDetail.Enabled = false;
            }
        }
        public void refreshForm()
        {
            cleanReceiption();
            btnStart.Enabled = false;
            btnFinish.Enabled = false;
            btnPrint.Enabled = false;
            btnDestroy.Enabled = false;
            btnTransfer.Enabled = false;
        }
        public void updateData(int status)
        {
            if (txtReceiptionid.Text != "" && txtDeskid.Text != "")
            {
                getPaid();
                int totalminutes = getTotalminute();
                float billfee = getBillfee();
                float detailfee = getDetailfee();
                //float totalpaid = billfee + detailfee;
                string timefinish = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                Controllers.HoaDonCtrl.updatefinish(long.Parse(txtReceiptionid.Text), timefinish, totalminutes, billfee, billfee + detailfee, status);
                Controllers.BanCtrl.updateStatus(long.Parse(txtDeskid.Text), Models.constant.status_ready);
            }
        }
        public int getTotalminute()
        {
            if (lbTimebegin.Text != "")
            {
                IFormatProvider culture = new CultureInfo("en-US", true);
                DateTime timebegin = DateTime.ParseExact(lbTimebegin.Text, "HH:mm dd/MM/yyyy", culture);
                DateTime timecurrent = DateTime.Now;
                TimeSpan span = timecurrent.Subtract(timebegin);
                decimal total = (decimal)span.TotalMinutes;
                return int.Parse(Math.Ceiling(total).ToString());
            }
            else
                return 0;
        }
        public float getBillfee()
        {
            int totalminutes = getTotalminute();
            float fee = 0;
            if (lbDeskfee.Text.Replace(" ", "").Replace("đ", "") != "")
                fee = float.Parse(lbDeskfee.Text.Replace(" ", "").Replace("đ", ""));
            float feemin = (fee / 60);
            float feetemp = float.Parse(totalminutes.ToString()) * feemin;
            //lam tron len 5,000d
            float totalfee = float.Parse((Math.Ceiling(decimal.Parse((feetemp / 5000).ToString())) * 5000).ToString());
            return totalfee;
        }
        public float getDetailfee()
        {
            float detailfee = 0;
            for (int i = 0; i < dgvDetails.Rows.Count; i++)
            {
                detailfee += float.Parse(dgvDetails.Rows[i].Cells["dtotal"].Value.ToString());
            }
            return detailfee;
        }
        public void getPaid()
        {
            int totalminutes = getTotalminute();
            float billfee = getBillfee();
            float detailfee = getDetailfee();
            float totalpaid = billfee + detailfee;

            lbTotalminute.Text = totalminutes.ToString() + " phút";
            lbFee.Text = billfee.ToString("#,###") + " đ";
            lblTotal.Text = totalpaid.ToString("#,###") + " đ";
            lbSumdetail.Text = detailfee.ToString("#,###") + " đ";

        }
        public void getReceipt()
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(createReceipt);
            printDocument.PrintController = new StandardPrintController();
            printDocument.Print();
        }
        private void createReceipt(object sender, PrintPageEventArgs e)
        {
            Graphics graphic = e.Graphics;
            float x = 5;
            float y = 5;
            float width = 280.0F; // max width I found through trial and error
            float height = 0F;

            Font drawFontArial16Bold = new Font("Arial", 16, FontStyle.Bold);
            Font drawFontArial10Regular = new Font("Arial", 10, FontStyle.Regular);
            Font drawFontArial10Bold = new Font("Arial", 10, FontStyle.Bold);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Set format of string.
            StringFormat drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            StringFormat drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            StringFormat drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;

            string _header = "Đô Án QLBIDA N12";
            string _line = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -";
            //tiêu đề
            graphic.DrawString(_header, drawFontArial16Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
            y += e.Graphics.MeasureString(_header, drawFontArial16Bold).Height;
            graphic.DrawString("* * *", drawFontArial16Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(_header, drawFontArial16Bold).Height;
            //bàn thanh toán
            graphic.DrawString("Tên bàn:", drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
            graphic.DrawString(txtDeskname.Text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            y += e.Graphics.MeasureString(_header, drawFontArial10Bold).Height;
            graphic.DrawString("Giờ bắt đầu:", drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
            graphic.DrawString(lbTimebegin.Text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            y += e.Graphics.MeasureString(_header, drawFontArial10Regular).Height;
            graphic.DrawString("Thời gian chơi:", drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
            graphic.DrawString(lbTotalminute.Text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            y += e.Graphics.MeasureString(_header, drawFontArial10Regular).Height;
            graphic.DrawString("Thành tiền:", drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
            graphic.DrawString(lbFee.Text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            y += e.Graphics.MeasureString(lbFee.Text, drawFontArial10Bold).Height;
            graphic.DrawString(_line, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
            y += e.Graphics.MeasureString(_line, drawFontArial10Regular).Height;

            //Phụ thu
            if (dgvDetails.Rows.Count > 0)
            {
                graphic.DrawString("Phụ thu", drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphic.DrawString("SL".PadRight(15) + "Tổng", drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += e.Graphics.MeasureString("detail", drawFontArial10Regular).Height;
                for (int i = 0; i < dgvDetails.Rows.Count; i++)
                {
                    string name = dgvDetails.Rows[i].Cells["dname"].Value.ToString();
                    string quantum = dgvDetails.Rows[i].Cells["dquantum"].Value.ToString();
                    string total = dgvDetails.Rows[i].Cells["dtotal"].Value.ToString() + " đ";
                    graphic.DrawString(name, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphic.DrawString(quantum.PadRight(10) + total, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += e.Graphics.MeasureString("detail", drawFontArial10Regular).Height;
                }
                graphic.DrawString("Thành tiền:", drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphic.DrawString(lbSumdetail.Text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += e.Graphics.MeasureString(lbSumdetail.Text, drawFontArial10Bold).Height;
                graphic.DrawString(_line, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                y += e.Graphics.MeasureString(_line, drawFontArial10Regular).Height;
            }
            //tổng kết
            graphic.DrawString("Tổng tiền thanh toán: ", drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
            graphic.DrawString(lblTotal.Text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            y += e.Graphics.MeasureString(lblTotal.Text, drawFontArial10Bold).Height;
            graphic.DrawString("Mã hóa đơn số: " + txtReceiptionid.Text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(txtReceiptionid.Text, drawFontArial10Regular).Height;
            graphic.DrawString("Ngày xuất hóa đơn: " + DateTime.Now.ToString("HH:mm dd/MM/yyyy"), drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += e.Graphics.MeasureString(txtReceiptionid.Text, drawFontArial10Regular).Height;
            //kết thúc
            graphic.DrawString("Chào tạm biệt và hẹn gặp lại quý khách!", drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
        }
      
        private void lvBillard_Click(object sender, EventArgs e)
        {
            cleanReceiption();
            txtDeskid.Text = lvDesk.SelectedItems[0].SubItems[1].Text;
            txtDeskname.Text = lvDesk.SelectedItems[0].SubItems[2].Text;
            int status = int.Parse(lvDesk.SelectedItems[0].SubItems[3].Text);
            if (status == Models.constant.status_ready)
            {
                btnStart.Enabled = true;
                btnFinish.Enabled = false;
                btnPrint.Enabled = false;
                btnDestroy.Enabled = false;
                btnTransfer.Enabled = false;
                txtDeskname.ForeColor = Color.Black;
            }
            else if (status == Models.constant.status_busy)
            {
                btnStart.Enabled = false;
                btnFinish.Enabled = true;
                btnPrint.Enabled = true;
                btnDestroy.Enabled = true;
                btnTransfer.Enabled = true;
                txtDeskname.ForeColor = Color.Green;
            }
            else if (status == Models.constant.status_upgrade)
            {
                btnStart.Enabled = false;
                btnFinish.Enabled = false;
                btnPrint.Enabled = false;
                btnDestroy.Enabled = false;
                btnTransfer.Enabled = false;
                txtDeskname.ForeColor = Color.RosyBrown;
            }
            lbDeskfee.Text = lvDesk.SelectedItems[0].SubItems[4].Text + " đ";
            ShowBill();
        }
        private void lvBillard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvDesk.SelectedItems.Count == 0)
            {
                return;
            }
            else
            {

            }
        }
        //receiption action
        private void btnStart_Click(object sender, EventArgs e)
        {
            DialogResult ok = new DialogResult();
            ok = MessageBox.Show("Bạn có muốn bắt đầu chơi bàn " + txtDeskname.Text + " Không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ok == DialogResult.Yes)
            {
                long billid = Controllers.HoaDonCtrl.generalid();
                if (billid > 0)
                {
                    string billardname = txtDeskname.Text;
                    float billardfee = float.Parse(lbDeskfee.Text.Replace(",", "").Replace("đ", "").Replace(" ", ""));
                    string timebegin = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    int result = Controllers.HoaDonCtrl.insert(billid, long.Parse(txtDeskid.Text), billardname, billardfee, timebegin, Models.constant.receiption_new);
                    if (result > 0)
                        Controllers.BanCtrl.updateStatus(long.Parse(txtDeskid.Text), Models.constant.status_busy);

                    MessageBox.Show("Bắt đầu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("Chưa có bàn nào được chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                frmMain_Load(sender, e);
            }
            else
            {
                return;
            }
        }
        private void btnFinish_Click(object sender, EventArgs e)
        {
            DialogResult confirmPayment = MessageBox.Show("Bạn có muốn tính tiền " + txtDeskname.Text + " Không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmPayment == DialogResult.Yes)
            {
                updateData(Models.constant.receiption_finished);
                DialogResult confirmPrint = MessageBox.Show("Bạn có muốn in hóa đơn " + txtDeskname.Text + " Không ?", "Số tiền phải thanh toán là: " + lblTotal.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmPrint == DialogResult.Yes)
                {
                    getReceipt();
                }
                else
                {
                    MessageBox.Show("Số tiền phải thanh toán là: " + lblTotal.Text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                return;
            }
            frmMain_Load(sender, e);
        }
        private void btnIn_Click(object sender, EventArgs e)
        {
            getPaid();
            getReceipt();
            DialogResult confirmFinish = MessageBox.Show("Bạn có muốn tính tiền " + txtDeskname.Text + " Không ?", "Số tiền phải thanh toán là: " + lblTotal.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmFinish == DialogResult.Yes)
            {
                updateData(Models.constant.receiption_finished);
                MessageBox.Show("Số tiền phải thanh toán là: " + lblTotal.Text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmMain_Load(sender, e);
            }
        }
        private void btnDestroy_Click(object sender, EventArgs e)
        {
            DialogResult confirmDestroy = MessageBox.Show("Bạn có ngừng " + txtDeskname.Text + " ngay không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmDestroy == DialogResult.Yes)
            {
                updateData(Models.constant.receiption_destroyed);
            }
            else
            {
                return;
            }
            frmMain_Load(sender, e);
        }
        private void btnTransfer_Click(object sender, EventArgs e)
        {
            getPaid();
            string deskname = txtDeskname.Text;
            string deskid = txtDeskid.Text;
            string deskfee = lbDeskfee.Text;
            string timebegin = lbTimebegin.Text;
            string totalminute = lbTotalminute.Text;
            string fee = lbFee.Text;
            string receiptionid = txtReceiptionid.Text;
            frmChuyenBan frmTransfer = new frmChuyenBan(deskname, deskid, deskfee, timebegin, totalminute, fee, receiptionid);
            frmTransfer.ShowDialog();
            if (frmTransfer.dr == DialogResult.OK)
            {
                ShowListDesk();
                refreshForm();
            }
            if (frmTransfer.dr == DialogResult.No)
            {
                getPaid();
            }
        }

        //detail action
        private void txtDetailQuantum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtDetailQuantum_TextChanged(object sender, EventArgs e)
        {
            int quantum = 0;
            int price = 0;
            if (txtDetailQuantum.Text.Replace(",", "") != "")
            {
                quantum = int.Parse(txtDetailQuantum.Text.Replace(",", ""));
            }
            if (txtDetailPrice.Text.Replace(",", "") != "")
            {
                price = int.Parse(txtDetailPrice.Text.Replace(",", ""));
            }
            float total = quantum * price;
            txtDetailTotal.Text = string.Format("{0:0,0}", decimal.Parse((quantum * price).ToString()));
            txtDetailTotal.SelectionStart = txtDetailTotal.Text.Length;
        }
        private void dgvMenus_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            bindingMenu();
        }
        private void dgvDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            bindingDetail();
        }
        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            if (txtReceiptionid.Text != "")
            {
                long detailid = Controllers.CTHDlCtrl.generalid();
                if (detailid > 0)
                {
                    //long menuid=long.Parse(txtDetailid.Text);
                    string name = txtDetailName.Text;
                    string unit = txtDetailUnit.Text;
                    float price = float.Parse(txtDetailPrice.Text);
                    int quantum = int.Parse(txtDetailQuantum.Text);
                    float total = float.Parse(txtDetailTotal.Text);
                    Controllers.CTHDlCtrl.insert(detailid, long.Parse(txtReceiptionid.Text), name, unit, price, quantum, total);
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lvBillard_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Thêm không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Bàn được chọn chưa có người chơi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnEditDetail_Click(object sender, EventArgs e)
        {
            if (txtDetailid.Text != "")
            {
                long detailid = long.Parse(txtDetailid.Text);
                long menuid = long.Parse((string)txtMenuid.Text);
                string name = txtDetailName.Text;
                string unit = txtDetailUnit.Text;
                float price = float.Parse(txtDetailPrice.Text);
                int quantum = int.Parse(txtDetailQuantum.Text);
                float total = float.Parse(txtDetailTotal.Text);
                Controllers.CTHDlCtrl.update(detailid, name, unit, price, quantum, total);
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lvBillard_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnDelDetail_Click(object sender, EventArgs e)
        {
            if (txtDetailid.Text != "")
            {
                DialogResult confirmDelete = MessageBox.Show("Bạn có muốn xóa Không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmDelete == DialogResult.Yes)
                {
                    Controllers.CTHDlCtrl.delete(long.Parse(txtDetailid.Text));
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lvBillard_Click(sender, e);
                }
            }
        }
        private void txtDetailPrice_TextChanged(object sender, EventArgs e)
        {
            string txt = txtDetailPrice.Text.Replace(",", "");
            if (txt != "")
            {
                txtDetailPrice.Text = string.Format("{0:0,0}", decimal.Parse(txt));
                txtDetailPrice.SelectionStart = txtDetailPrice.Text.Length;
                if (txt == "00")
                {
                    txtDetailPrice.Text = "";
                }
            }
            txtDetailQuantum_TextChanged(sender, e);
        }
        private void txtDetailPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

    }
}
