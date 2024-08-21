using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace N12_QLBIDA.Models
{
    class TaiKhoanMod
    {
        protected string username { get; set; } //100
        protected string password { get; set; } //250
        protected string screenname { get; set; }   //250
        protected int status { get; set; }  //int

        public TaiKhoanMod()
        {
        }
        public TaiKhoanMod(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        public string encodeSHA256(string password)
        {
            SHA256Managed crypt = new SHA256Managed();
            string hash = "";
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            foreach (byte bit in crypto)
            {
                hash += bit.ToString("x2");
            }
            return hash;
            //return password;
        }
        public string checkLogin()
        {
            string[] paras = new string[2] { "@username", "@password" };
            object[] values = new object[2] { username, password };
            return connection.ExcuteScalar(constant.check_Userlogin, CommandType.StoredProcedure, paras, values);
        }
        public int updateLogin()
        {
            string[] paras = new string[2] { "@username", "@password" };
            object[] values = new object[2] { username, password };
            return connection.Excute_Sql(constant.update_Userlogin, CommandType.StoredProcedure, paras, values);
        }

    }
}
