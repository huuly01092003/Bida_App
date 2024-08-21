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

namespace N12_QLBIDA.Views
{
    public partial class frmTaiKhoan : Form
    {
        SqlConnection conn;
        SqlDataAdapter da_user;
        DataSet ds_user;
        public frmTaiKhoan()
        {
            InitializeComponent();
            conn = new SqlConnection("server =DESKTOP-LSFEJMC\\SQLEXPRESS; user id= sa; password= 123; database = BiDa");
            ds_user = new DataSet();
            string sql = "select*from Userlogin";
            da_user = new SqlDataAdapter(sql, conn);
            da_user.Fill(ds_user, "User");
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            getListUser();
            databiding();
        }
        public void getListUser()
        {
            dataGridView1.DataSource = ds_user.Tables["User"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow newrow = ds_user.Tables[0].NewRow();
            newrow["username"] = textBox1.Text;

            // Make sure to add "password" column to the DataTable
            if (!ds_user.Tables[0].Columns.Contains("password"))
            {
                ds_user.Tables[0].Columns.Add("password");
            }

            // Use the result of encodePassword and set it to the "password" column
            newrow["password"] = Controllers.TaiKhoanCtrl.encodePassword(textBox2.Text);
            newrow["screenname"] = textBox3.Text;
            newrow["status"] = textBox4.Text;

            ds_user.Tables[0].Rows.Add(newrow);
            SqlCommandBuilder cmb = new SqlCommandBuilder(da_user);
            da_user.Update(ds_user, "User");
            frmUser_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_user.Tables["User"].Columns["Username"];
            ds_user.Tables["User"].PrimaryKey = key;
            DataRow dr = ds_user.Tables["User"].Rows.Find(textBox1.Text);
            // Xoa dong du lieu vua tim duoc
            if (dr != null)
            {
                dr.Delete();
            }
            //Cap nhat trong CSDL
            SqlCommandBuilder cB = new SqlCommandBuilder(da_user);
            // Cap nhat trong dataSet hien thi
            da_user.Update(ds_user, "User");
            frmUser_Load(sender, e);
        }
        void databiding()
        {
            textBox1.DataBindings.Clear();
            textBox2.DataBindings.Clear();
            textBox3.DataBindings.Clear();
            textBox4.DataBindings.Clear();
            textBox1.DataBindings.Add("Text", dataGridView1.DataSource, "username");
            textBox2.DataBindings.Add("Text", dataGridView1.DataSource, "password");
            textBox3.DataBindings.Add("Text", dataGridView1.DataSource, "screenname");
            textBox4.DataBindings.Add("Text", dataGridView1.DataSource, "status");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UserName_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
