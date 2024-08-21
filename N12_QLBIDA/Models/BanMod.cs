using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N12_QLBIDA.Models
{
    class BanMod
    {
        protected long id { get; set; }
        protected string name { get; set; }
        protected int style { get; set; }
        protected string description { get; set; }
        protected int status { get; set; }
        protected float fee { get; set; }

        public BanMod()
        {
        }
        public BanMod(long id)
        {
            this.id = id;
        }
        public BanMod(long id, string name, int style, string description, int status, float fee)
        {
            this.id = id;
            this.name = name;
            this.style = style;
            this.description = description;
            this.fee = fee;
            this.status = status;
        }
        public long generalid()
        {
            return long.Parse(connection.ExcuteScalar(string.Format(constant.createid_Desk)));
        }
        public int insert()
        {
            int i = 0;
            string[] paras = new string[6] { "@id", "@name", "@style", "@description", "@status", "@fee" };
            object[] values = new object[6] { id, name, style, description, status, fee };
            i = connection.Excute_Sql(constant.insert_Desk, CommandType.StoredProcedure, paras, values);
            return i;
        }
        public int update()
        {
            int i = 0;
            string[] paras = new string[6] { "@id", "@name", "@style", "@description", "@status", "@fee" };
            object[] values = new object[6] { id, name, style, description, status, fee };
            i = connection.Excute_Sql(constant.update_Desk, CommandType.StoredProcedure, paras, values);
            return i;
        }
        public int delete()
        {
            int i = 0;
            string[] paras = new string[1] { "@id" };
            object[] values = new object[1] { id };
            i = connection.Excute_Sql(constant.delete_Desk, CommandType.StoredProcedure, paras, values);
            return i;
        }
        public DataSet findView()
        {
            return connection.FillDataSet(constant.find_Desk, CommandType.StoredProcedure);
        }
        public DataSet findAll()
        {
            return connection.FillDataSet(constant.findAll_Desk, CommandType.StoredProcedure);
        }
        public DataSet findByStatus(int _status)
        {
            DataSet ds = new DataSet();
            string[] paras = new string[1] { "@status" };
            object[] values = new object[1] { _status };
            ds = connection.FillDataSet(constant.findByStatus_Desk, CommandType.StoredProcedure, paras, values);
            return ds;
        }

        public int updateStatus(long id, int status)
        {
            int i = 0;
            string[] paras = new string[2] { "@id", "@status" };
            object[] values = new object[2] { id, status };
            i = connection.Excute_Sql(constant.updateStatus_Desk, CommandType.StoredProcedure, paras, values);
            return i;
        }
        public DataSet findById(long id)
        {
            DataSet ds = new DataSet();
            string[] paras = new string[1] { "@id" };
            object[] values = new object[1] { id };
            ds = connection.FillDataSet(constant.FindByDeskid_Desk, CommandType.StoredProcedure, paras, values);
            return ds;
        }
    }
}
