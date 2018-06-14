using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApplication.Entities.Models
{
    public class chidinhcls
    {
        public string makb { get; set; }
        public string macls { get; set; }
        public string soctvp { get; set; }
        public decimal thanhtien { get; set; }
        public int soluong { get; set; }
        public string thangkt { get; set; }
        public string namkt { get; set; }

        public virtual dmcls dmcls { get; set; }
        public virtual thuchi thuchi { get; set; }
    }
}
