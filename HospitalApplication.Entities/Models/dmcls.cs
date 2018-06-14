using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApplication.Entities.Models
{
    public class dmcls
    {
        public string macls { get; set; }
        public string tencls { get; set; }

        public virtual ICollection<chidinhcls> chidinhclss { get; set; }
    }
}
