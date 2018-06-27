using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApplication.Entities.Models
{
    public class khambenh
    {
        public string mabn { get; set; }
        public string makb { get; set; }
        public string maba { get; set; }
        public string madv { get; set; }
        public DateTime ngaykcb { get; set; }
        public string maicd { get; set; }
        public int dakham { get; set; }
        public string maphong { get; set; }
        public int xoa { get; set; }

        public string thangkt { get; set; }
        public string namkt { get; set; }

        public virtual dmbenhnhan dmbenhnhan { get; set; }
        public virtual dmdonvi dmdonvi { get; set; }
       




    }
}
