using HospitalApplication.Entities.Models;
using HospitalApplication.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
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
            return View();
        }
        private string CovertDate(DateTime day)
        {
            string stringDay = "";
            DayOfWeek dow = day.DayOfWeek;
            string thu = "";
            switch (dow)
            {
                case DayOfWeek.Friday:
                    thu = "T6";
                    break;
                case DayOfWeek.Monday:
                    thu = "T2";
                    break;
                case DayOfWeek.Saturday:
                    thu = "T7";
                    break;
                case DayOfWeek.Sunday:
                    thu = "CN";
                    break;
                case DayOfWeek.Thursday:
                    thu = "T5";
                    break;
                case DayOfWeek.Tuesday:
                    thu = "T3";
                    break;
                case DayOfWeek.Wednesday:
                    thu = "T4";
                    break;
            }
            string shortDay = day.ToShortDateString();
            stringDay = string.Format("{0}({1})", thu, shortDay.Substring(0, shortDay.Length - 5));
            return stringDay;
        }
        public ActionResult getTinhHinhKhamBenh(DateTime tuNgay, DateTime denNgay)
        {
            List<int> ccData = new List<int>();
            List<int> nhiData = new List<int>();
            List<int> sanData = new List<int>();
            List<int> ngoaiData = new List<int>();
            List<int> rhmData = new List<int>();
            var listDate = Enumerable.Range(0, 1 + denNgay.Subtract(tuNgay).Days)
         .Select(offset => tuNgay.AddDays(offset))
         .ToList();
            denNgay = denNgay.AddDays(1);
            List<TinhHinhKhamBenhModel> dem = storeDB.khambenh.Where(t => tuNgay <= t.ngaykcb && denNgay >= t.ngaykcb && !string.IsNullOrEmpty(t.maicd)).Select(t => new KhamBenhModel { ngaykcb = DbFunctions.TruncateTime(t.ngaykcb).Value, madv = t.madv, makb = t.makb }).GroupBy(x => new { x.madv, x.ngaykcb }).OrderBy(k => k.Key.ngaykcb)
                .Select(m => new TinhHinhKhamBenhModel { madv = m.Key.madv, ngaykcb = m.Key.ngaykcb, soluong = m.Count() })
                .ToList();


            foreach (var item in listDate)
            {
                var tmp = dem.Where(t => t.ngaykcb == item);
                int cctmp = 0, santmp = 0, nhitmp = 0, ngoaitmp = 0, rhmtmp = 0;
                foreach (var i in tmp)
                {
                    
                    switch (i.madv)
                    {
                        case "KD028":
                            cctmp = i.soluong;
                            break;
                        case "KD021":
                            santmp = i.soluong;
                            break;
                        case "KD016":
                            nhitmp = i.soluong;
                            break;
                        case "KD017":
                            ngoaitmp = i.soluong;
                            break;
                        case "KD025":
                            rhmtmp = i.soluong;
                            break;
                        default:
                            break;
                    }
                   
                }
                ccData.Add(cctmp);
                sanData.Add(santmp);
                nhiData.Add(nhitmp);
                ngoaiData.Add(ngoaitmp);
                rhmData.Add(rhmtmp);
            }
            var date = listDate.Select(y => CovertDate(y)).ToList();

            List<ChartModel> chartModel = new List<ChartModel> {
                 new ChartModel { Label = "Cấp cứu",Date=date, Data = ccData },
                  new ChartModel { Label="Nhi",Data=nhiData },
                new ChartModel { Label="Sản",Data=sanData },
            new ChartModel { Label="Ngoại Nhi",Data=ngoaiData},
             new ChartModel { Label="RHM",Data=rhmData} ,
           };


            return Json(chartModel, JsonRequestBehavior.AllowGet);
        }
    }
}