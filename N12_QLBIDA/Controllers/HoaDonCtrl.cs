using N12_QLBIDA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N12_QLBIDA.Controllers
{
    class HoaDonCtrl
    {
        public static long generalid()
        {
            HoaDonMod receiption = new HoaDonMod();
            return receiption.generalid();

        }
        // Method Add
        public static int insert(long id, long deskid, string deskname, float deskfee, string timebegin, int status)
        {
            HoaDonMod receiption = new HoaDonMod();
            return receiption.insert(id, deskid, deskname, deskfee, timebegin, status);

        }
        // Method Update time
        public static int updateTimebegin(long id, string timebegin)
        {
            HoaDonMod receiption = new HoaDonMod();
            return receiption.updateTimebegin(id, timebegin);

        }
        // Method Update finish
        public static int updatefinish(long id, string timefinish, int totalminute, float fee, float total, int status)
        {
            HoaDonMod receiption = new HoaDonMod();
            return receiption.updatefinish(id, timefinish, totalminute, fee, total, status);

        }

        // Method find by findBydeskid
        public static DataSet findBydeskid(long deskid)
        {
            HoaDonMod receiption = new HoaDonMod();
            return receiption.findBydeskid(deskid);
        }
        public static int updateTransfer(long id, long deskid, string deskname, float deskfee, string timebegin)
        {
            HoaDonMod receiption = new HoaDonMod();
            return receiption.updateTransfer(id, deskid, deskname, deskfee, timebegin);

        }
    }
}
