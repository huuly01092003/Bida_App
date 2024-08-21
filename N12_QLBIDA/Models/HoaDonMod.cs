using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N12_QLBIDA.Models
{
    class HoaDonMod
    {
        protected long id { get; set; }
        protected long deskid { get; set; }
        protected string deskname { get; set; }
        protected float deskfee { get; set; }
        protected string timebegin { get; set; }
        protected string timefinish { get; set; }
        protected int totalminute { get; set; }
        protected float fee { get; set; }
        protected float total { get; set; }
        protected int status { get; set; }

        public HoaDonMod(long _id)
        {
            this.id = _id;
        }
        public HoaDonMod()
        {
        }
        public HoaDonMod(long id, long deskid, string deskname, float deskfee, string timebegin, string timefinish, int totalminute, float fee, float total, int status)
        {
            this.id = id;
            this.deskid = deskid;
            this.deskname = deskname;
            this.deskfee = deskfee;
            this.timebegin = timebegin;
            this.timefinish = timefinish;
            this.totalminute = totalminute;
            this.fee = fee;
            this.total = total;
            this.status = status;
        }
        public long generalid()
        {
            return long.Parse(connection.ExcuteScalar(String.Format(constant.createid_Receiption)));
        }
        public int insert(long id, long deskid, string deskname, float deskfee, string timebegin, int status)
        {
            string[] paras = new string[6] { "@id", "@deskid", "@deskname", "@deskfee", "@timebegin", "@status" };
            object[] values = new object[6] { id, deskid, deskname, deskfee, timebegin, status };
            return connection.Excute_Sql(constant.insert_Receiption, CommandType.StoredProcedure, paras, values);
        }
        public int updateTimebegin(long id, string timebegin)
        {
            string[] paras = new string[2] { "@id", "@timebegin" };
            object[] values = new object[2] { id, timebegin };
            return connection.Excute_Sql(constant.updateTimebegin_Receiption, CommandType.StoredProcedure, paras, values);
        }
        public int updatefinish(long id, string timefinish, int totalminute, float fee, float total, int status)
        {
            string[] paras = new string[6] { "@id", "@timefinish", "@totalminute", "@fee", "@total", "@status" };
            object[] values = new object[6] { id, timefinish, totalminute, fee, total, status };
            return connection.Excute_Sql(constant.updatefinish_Receiption, CommandType.StoredProcedure, paras, values);
        }
        public DataSet findBydeskid(long deskid)
        {
            DataSet ds = new DataSet();
            string[] paras = new string[1] { "@deskid" };
            object[] values = new object[1] { deskid };
            ds = connection.FillDataSet(constant.findByDeskid_Receiption, CommandType.StoredProcedure, paras, values);
            return ds;
        }
        public int updateTransfer(long id, long deskid, string deskname, float deskfee, string timebegin)
        {
            string[] paras = new string[5] { "@id", "@deskid", "@deskname", "@deskfee", "@timebegin" };
            object[] values = new object[5] { id, deskid, deskname, deskfee, timebegin };
            return connection.Excute_Sql(constant.transfer_Receiption, CommandType.StoredProcedure, paras, values);
        }
    }
}
