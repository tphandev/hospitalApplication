using HospitalApplication.Entities.Models;
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
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            DateTime date1 = DateTime.Now;
            Calendar cal = dfi.Calendar;
            int week = cal.GetWeekOfYear(date1, dfi.CalendarWeekRule,
                                                            dfi.FirstDayOfWeek);
            Console.WriteLine("{0:d}: Week {1} ({2})", date1,
                              cal.GetWeekOfYear(date1, dfi.CalendarWeekRule,
                                                dfi.FirstDayOfWeek),
                              cal.ToString().Substring(cal.ToString().LastIndexOf(".") + 1));
            return View();
        }
        private 
    }
}