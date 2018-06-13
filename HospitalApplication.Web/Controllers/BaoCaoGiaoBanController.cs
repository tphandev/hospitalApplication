using HospitalApplication.Entities.Models;
using HospitalApplication.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalApplication.Web.Controllers
{
    public class BaoCaoGiaoBanController : Controller
    {

        HADbContext storeDB = new HADbContext();
        // GET: BaoCaoGiaoBan
        public ActionResult Index()
        {
            DateTime tuNgay = new DateTime(2018, 5, 7);
            DateTime denNgay = new DateTime(2018, 5, 13);
            getTinhHinhKhamBenh(tuNgay, denNgay);
            return View();
        }
        private List<TinhHinhKhamBenhModel> getTinhHinhKhamBenh(DateTime tuNgay, DateTime denNgay)
        {
           
           
            var tmp = storeDB.khambenh.Where(t => tuNgay <= t.ngaykcb && denNgay >= t.ngaykcb).ToList()
                .Select(y=>new TinhHinhKhamBenhModel {
                madv=y.madv,ngaykcb=new DateTime(y.ngaykcb.Year,y.ngaykcb.Month,y.ngaykcb.Day)});
           var query= tmp.GroupBy(y => new { y.madv, y.ngaykcb }).ToList().Select(t => new TinhHinhKhamBenhModel { madv = t.Key.madv, soluong = t.Key.madv.Count(), ngaykcb = t.Key.ngaykcb });
            List<TinhHinhKhamBenhModel> models = query.ToList();

            return models;
        }
    }
}