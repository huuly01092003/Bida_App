using N12_QLBIDA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N12_QLBIDA.Controllers
{
    class BanCtrl
    {
        public static long generalid()
        {
            BanMod Desk = new BanMod();
            return Desk.generalid();
        }
        // Method Add
        public static int insert(long id, string name, int style, string description, int status, float fee)
        {
            BanMod Desk = new BanMod(id, name, style, description, status, fee);
            return Desk.insert();
        }
        // Method Update
        public static int update(long id, string name, int style, string description, int status, float fee)
        {
            BanMod Desk = new BanMod(id, name, style, description, status, fee);
            return Desk.update();
        }
        // Method Delete
        public static int delete(long id)
        {
            BanMod Desk = new BanMod(id);
            return Desk.delete();
        }

        // Method find view
        public static DataSet findView()
        {
            BanMod Desk = new BanMod();
            return Desk.findView();
        }
        public static DataSet findAll()
        {
            BanMod Desk = new BanMod();
            return Desk.findAll();
        }
        public static DataSet findByStatus(int status)
        {
            BanMod Desk = new BanMod();
            return Desk.findByStatus(status);
        }
        public static int updateStatus(long id, int status)
        {
            BanMod Desk = new BanMod();
            return Desk.updateStatus(id, status);
        }
        public static DataSet findById(long id)
        {
            BanMod Desk = new BanMod();
            return Desk.findById(id);
        }
    }
}
