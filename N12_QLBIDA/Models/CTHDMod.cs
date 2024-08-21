using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N12_QLBIDA.Models
{
    class CTHDMod
    {
        protected long id { get; set; }
        protected long receiptionid { get; set; }
        protected string name { get; set; }
        protected string unit { get; set; }
        protected float price { get; set; }
        protected int quantum { get; set; }
        protected float total { get; set; }

        public CTHDMod(long _id)
        {
            this.id = _id;
        }
        public CTHDMod()
        {
        }
        public CTHDMod(long id, long receiptionid, string name, string unit, float price, int quantum, float total)
        {
            this.id = id;
            this.receiptionid = receiptionid;
            this.name = name;
            this.unit = unit;
            this.price = price;
            this.quantum = quantum;
            this.total = total;
        }
        public long generalid()
        {
            return long.Parse(connection.ExcuteScalar(String.Format(constant.createid_Receiptiondetail)));
        }
        public int insert()
        {
            string[] paras = new string[7] { "@id", "@receiptionid", "@name", "@unit", "@price", "@quantum", "@total" };
            object[] values = new object[7] { id, receiptionid, name, unit, price, quantum, total };
            return connection.Excute_Sql(constant.insert_Receiptiondetail, CommandType.StoredProcedure, paras, values);

        }
        public int update(long id, string name, string unit, float price, int quantum, float total)
        {
            string[] paras = new string[6] { "@id", "@name", "@unit", "@price", "@quantum", "@total" };
            object[] values = new object[6] { id, name, unit, price, quantum, total };
            return connection.Excute_Sql(constant.update_Receiptiondetail, CommandType.StoredProcedure, paras, values);

        }
        public int delete()
        {
            string[] paras = new string[1] { "@id" };
            object[] values = new object[1] { id };
            return connection.Excute_Sql(constant.delete_Receiptiondetail, CommandType.StoredProcedure, paras, values);

        }

        public DataSet findByreceiptionid(long receiptionid)
        {
            DataSet ds = new DataSet();
            string[] paras = new string[1] { "@receiptionid" };
            object[] values = new object[1] { receiptionid };
            ds = connection.FillDataSet(constant.findByReceiptionid_Receiptiondetail, CommandType.StoredProcedure, paras, values);
            return ds;
        }
    }
}
