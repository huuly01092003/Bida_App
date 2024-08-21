using N12_QLBIDA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N12_QLBIDA.Controllers
{
    class CTHDlCtrl
    {
        public static long generalid()
        {
            CTHDMod Receiptiondetail = new CTHDMod();
            return Receiptiondetail.generalid();

        }

        // Method Add
        public static int insert(long id, long receiptionid, string name, string unit, float price, int quantum, float total)
        {
            CTHDMod Receiptiondetail = new CTHDMod(id, receiptionid, name, unit, price, quantum, total);
            return Receiptiondetail.insert();

        }
        // Method Update
        public static int update(long id, string name, string unit, float price, int quantum, float total)
        {
            CTHDMod Receiptiondetail = new CTHDMod();
            return Receiptiondetail.update(id, name, unit, price, quantum, total);

        }
        // Method Delete
        public static int delete(long id)
        {
            CTHDMod Receiptiondetail = new CTHDMod(id);
            return Receiptiondetail.delete();

        }

        // Method find by styleid
        public static DataSet findByreceiptionid(long receiptionid)
        {
            CTHDMod Receiptiondetail = new CTHDMod();
            return Receiptiondetail.findByreceiptionid(receiptionid);

        }

    }
}
