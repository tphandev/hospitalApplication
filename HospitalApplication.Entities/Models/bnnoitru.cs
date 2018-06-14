using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApplication.Entities.Models
{
    public class bnnoitru
    {
       
        public string mabn { get; set; }
        public string makb { get; set; }
        public string maba { get; set; }
        public string madv { get; set; }
        public string madt { get; set; }
        public string maphong { get; set; }
        public string maicd { get; set; }
        public string kqcdoan{ get; set; }
        public DateTime ngayvv { get; set; }
        public DateTime? ngayrv { get; set; }
        public string mathe { get; set; }
        public DateTime ngaybd { get; set; }
        public DateTime ngaykt { get; set; }
        public string mabvdk { get; set; }
        public string thangkt { get; set; }
        public string namkt { get; set; }
        public int namsinh { get; set; }
        public virtual dmbenhnhan dmbenhnhan { get; set; }
        public virtual dmdonvi dmdonvi { get; set; }

    }
}
