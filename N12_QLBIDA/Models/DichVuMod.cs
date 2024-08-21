using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N12_QLBIDA.Models
{
    class DichVuMod
    {
        protected long id { get; set; }
        protected string name { get; set; }
        protected string unit { get; set; }
        protected float price { get; set; }
        protected string description { get; set; }

        public DichVuMod(long _id)
        {
            this.id = _id;
        }
        public DichVuMod()
        {

        }
        public DichVuMod(long _id, string _name, string _unit, float _price, string _description)
        {
            this.id = _id;
            this.name = _name;
            this.unit = _unit;
            this.price = _price;
            this.description = _description;
        }
        public long generalid()
        {
            return long.Parse(connection.ExcuteScalar(string.Format(constant.createid_Menu)));
        }
        public int insert()
        {
            int i = 0;
            string[] paras = new string[5] { "@id", "@name", "@unit", "@price", "@description" };
            object[] values = new object[5] { id, name, unit, price, description };
            i = connection.Excute_Sql(constant.insert_Menu, CommandType.StoredProcedure, paras, values);
            return i;
        }
        public int update()
        {
            int i = 0;
            string[] paras = new string[5] { "@id", "@name", "@unit", "@price", "@description" };
            object[] values = new object[5] { id, name, unit, price, description };
            i = connection.Excute_Sql(constant.update_Menu, CommandType.StoredProcedure, paras, values);
            return i;
        }
        public int delete()
        {
            int i = 0;
            string[] paras = new string[1] { "@id" };
            object[] values = new object[1] { id };
            i = connection.Excute_Sql(constant.delete_Menu, CommandType.StoredProcedure, paras, values);
            return i;
        }
        public DataSet findView()
        {
            return connection.FillDataSet(constant.findView_Menu, CommandType.StoredProcedure);
        }
    }
}
