using N12_QLBIDA.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace N12_QLBIDA
{
    public partial class frmDangNhap : Form
    {
        //SqlConnection conn = new SqlConnection("server =DESKTOP-LSFEJMC\\SQLEXPRESS; user id= sa; password= 123; database = BiDa");
        public frmDangNhap()
        {
            InitializeComponent();
        }
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk_Click(this, new EventArgs());
            }
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private int userStatus;
        //private void btnOk_Click(object sender, EventArgs e)
        //{
        //    conn.Open();
        //    if (txtUsername.Text == "")
        //    {
        //        MessageBox.Show("Hãy nhập vào tên đăng nhập");
        //    }
        //    else if (txtPassword.Text == "")
        //    {
        //        MessageBox.Show("Hãy nhập vào mật khẩu");
        //    }
        //    else
        //    {
        //        string user = txtUsername.Text;
                
        //        string pass = txtPassword.Text;
                
        //        if (CheckLogin(user, pass))
        //        {
        //            userStatus = GetUserStatus(user);
        //            MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //            frmMain frm = new frmMain(userStatus);
        //            this.Hide();
        //            frm.ShowDialog();

        //            this.Close();
        //        }
        //        else
        //        {
        //            // Đăng nhập thất bại
        //            MessageBox.Show("Đăng nhập không thành công. Vui lòng kiểm tra tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //        conn.Close();
        //    }
        //}
        private void btnOk_Click(object sender, EventArgs e)
        {
            
            if (txtUsername.Text == "")
            {
                MessageBox.Show("Hãy nhập vào tên đăng nhập");
            }
            else if (txtPassword.Text == "")
            {
                MessageBox.Show("Hãy nhập vào mật khẩu");
            }
            else
            {
                string user = txtUsername.Text;
                string pass = Controllers.TaiKhoanCtrl.encodePassword(txtPassword.Text);
                //string pass = txtPassword.Text;
                string check = Controllers.TaiKhoanCtrl.checkUserlogin(user, pass);

                if (check == "")
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng");
                }
                else
                {
                    userStatus = GetUserStatus(user);

                    frmMain frm = new frmMain(userStatus);
                    this.Hide();
                    frm.ShowDialog();
                    this.Close();
                }
           
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
            {
                Application.Exit();
            }

            private int GetUserStatus(string username)
            {
                
                //using (SqlConnection conn = new SqlConnection())
                using (SqlConnection conn = Models.connection.Getconnection())
                {
                    
                    if (conn != null)
                    {
                        conn.Open();

                        string query = "SELECT status FROM Userlogin WHERE username = @username";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@username", username);

                            object result = cmd.ExecuteScalar();
                            if (result != null && int.TryParse(result.ToString(), out int status))
                            {
                                return status;
                            }
                        }
                        conn.Close();
                    }
                }
                return 0;
            }




            private bool CheckLogin(string username, string password)
            {
                
                string conn = "server =DESKTOP-LSFEJMC\\SQLEXPRESS; user id= sa; password= 123; database = BiDa";
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                   
                    string query = "select count(*) from userlogin where username = @Username and password = @Password";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        int count = (int)cmd.ExecuteScalar();

                        
                        if (count > 0)
                        {
                            
                            return true;
                        }
                        else
                        {
                            
                            return false;
                        }
                    }
                }
            }
        }
    }

