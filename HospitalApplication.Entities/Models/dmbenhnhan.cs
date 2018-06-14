using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApplication.Entities.Models
{
    public class dmbenhnhan
    {
        public string mabn { get; set; }
        public string holot { get; set; }
        public string ten { get; set; }
        public DateTime ngaysinh { get; set; }
        public byte gioitinh { get; set; }
        public virtual ICollection<bnnoitru> bnnoitru { get; set; }
        public virtual ICollection<thuchi> thuchi { get; set; }
    }
}
