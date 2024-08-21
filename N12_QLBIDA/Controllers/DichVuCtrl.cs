using N12_QLBIDA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N12_QLBIDA.Controllers
{
    class DichVuCtrl
    {
        public static long generalid()
        {
            DichVuMod menu = new DichVuMod();
            return menu.generalid();
        }
        // Method Add
        public static int insert(long id, string name, string unit, float price, string description)
        {
            DichVuMod menu = new DichVuMod(id, name, unit, price, description);
            return menu.insert();
        }
        // Method Update
        public static int update(long id, string name, string unit, float price, string description)
        {
            DichVuMod menu = new DichVuMod(id, name, unit, price, description);
            return menu.update();
        }
        // Method Delete
        public static int delete(long id)
        {
            DichVuMod menu = new DichVuMod(id);
            return menu.delete();
        }
        public static DataSet findView()
        {
            DichVuMod menu = new DichVuMod();
            return menu.findView();
        }
    }
}
