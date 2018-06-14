using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApplication.Entities.Models
{
    public class dmdonvi
    {
        public string madv { get; set; }
        public string tendv { get; set; }
        public byte xoa { get; set; }
        public byte loaidv { get; set; }
        public string ma_khoa_cv2348 { get; set; }
        public string dieutri { get; set; }
        public byte khoaduoc { get; set; }
        public virtual ICollection<bnnoitru> bnnoitru { get; set; }
    }
}
