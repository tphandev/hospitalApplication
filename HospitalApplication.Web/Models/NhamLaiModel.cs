using HospitalApplication.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalApplication.Web.Models
{
    public class NhamLaiModel
    {

        public int stt { get; set; }
        public string mabn { get; set; }
        public string makb { get; set; }
        public string hoten { get; set; }
        public string noitru { get; set; }
        public string macls { get; set; }
        public string tencls { get; set; }
        public int soluong { get; set; }
        public decimal thanhtien { get; set; }
        public DateTime ngayct { get; set; }       
        public string taikhoan { get; set; }
        public ICollection<chidinhcls> chidinhclss { get; set; }
    }
}