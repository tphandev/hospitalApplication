using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalApplication.Web.Models
{
    public class LichSuKCB
    {
        public string maKetQua { get; set; }
        public string hoTen { get; set; }
        public string gioiTinh { get; set; }
        public string diaChi { get; set; }
        public string maDKBD { get; set; }
        public string cqBHXH { get; set; }
        public string gtTheTu { get; set; }
        public string gtTheDen { get; set; }
        public string maKV { get; set; }
        public string ngayDu5Nam { get; set; }
        public IEnumerable<dsLichSuKCB> dsLichSuKCB { get; set; }
    }
    public class dsLichSuKCB
    {
        public string maHoSo { get; set; }
        public string maCSKCB { get; set; }
        public string tuNgay { get; set; }
        public string denNgay { get; set; }
        public string tenBenh { get; set; }
        public string tinhTrang { get; set; }
        public string kqDieuTri { get; set; }
    }
}