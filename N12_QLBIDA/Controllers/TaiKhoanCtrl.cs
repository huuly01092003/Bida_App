using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N12_QLBIDA.Controllers
{
    class TaiKhoanCtrl
    {
        public static string encodePassword(string _password)
        {
            Models.TaiKhoanMod login = new Models.TaiKhoanMod();
            return login.encodeSHA256(_password);

            //return _password;
        }
        public static string checkUserlogin(string _user, string _password)
        {
            Models.TaiKhoanMod login = new Models.TaiKhoanMod(_user, _password);
            return login.checkLogin();
        }
        public static int updateUserlogin(string _user, string _password)
        {
            Models.TaiKhoanMod login = new Models.TaiKhoanMod(_user, _password);
            return login.updateLogin();
        }

    }
}
