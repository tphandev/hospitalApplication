using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApplication.Entities.Models
{
    public class thuchi
    {
        public string soct { set; get; }

        public string mabn { get; set; }

        public string makb { get; set; }

        public int noitru { get; set; }
        public string thangkt { get; set; }
        public string namkt { get; set; }
        public int xoa { get; set; }
        public int loaibldv { set; get; }
        public string taikhoan { get; set; }
        public DateTime ngayct { get; set; }

        public virtual ICollection<chidinhcls> chidinhclss { get; set; }
        public virtual dmbenhnhan dmbenhnhan { get; set; }
    }
}
