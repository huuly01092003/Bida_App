using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N12_QLBIDA.Models
{
    public class cmbSelection
    {
        public int id { get; set; }
        public long lid { get; set; }
        public string name { get; set; }

        public cmbSelection(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        public cmbSelection(long lid, string name)
        {
            this.lid = lid;
            this.name = name;
        }
        public override string ToString()
        {
            return name;
        }
    }
}