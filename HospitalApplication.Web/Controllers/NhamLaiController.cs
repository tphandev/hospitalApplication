using HospitalApplication.Entities.Models;
using HospitalApplication.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalApplication.Web.Controllers
{
    public class NhamLaiController : Controller
    {

        HADbContext storeDB = new HADbContext();
        // GET: NhamLai
        public ActionResult Index()
        {
            List<SelectListModels> thangkt = new List<SelectListModels>();
            List<SelectListModels> namkt = new List<SelectListModels>();
            for (int i = 1; i < 13; i++)
            {
                if (i < 10)
                {
                    string thang = "0" + i.ToString();
                    thangkt.Add(new SelectListModels { Value = thang, Name = thang });
                }
                else
                {
                    string thang = i.ToString();
                    thangkt.Add(new SelectListModels { Value = thang, Name = thang });
                }
            }
            ViewBag.thanglist = new SelectList(thangkt, "Value", "Name");
           int year = DateTime.Now.Year;
            for (int i = year; i > year -3; i--)
            {
                namkt.Add(new SelectListModels { Value = i.ToString(), Name = i.ToString() });
            }
            ViewBag.namlist = new SelectList(namkt, "Value", "Name");
            return View();
        }

        private List<NhamLaiModel> GetThuChi(string thangkt, string namkt)
        {
            List<NhamLaiModel> models = new List<NhamLaiModel>();
            IQueryable<NhamLaiModel> query;

            //query = storeDB.thuchi.Where(t=>t.thangkt==thangkt && t.namkt==namkt
            //                                  && t.xoa==0 && t.loaibldv==0 && (t.chidinhclss.Where(c=>c.macls.StartsWith("DV_") ||c.macls.StartsWith("KB_DV")).Count()>0) );
            query = from t in storeDB.thuchi
                   join c in storeDB.chidinhcls on t.soct equals c.soctvp
                   join d in storeDB.dmcls on c.macls equals d.macls
                   where t.thangkt ==thangkt && t.namkt==namkt && t.xoa==0 && t.loaibldv==0 && c.namkt==namkt
                  // && (c.macls.StartsWith("DV") || c.macls.StartsWith("KB_DV") || c.macls.StartsWith("DVBS"))     
                   group c by new {t.mabn,t.makb,t.dmbenhnhan.holot,t.dmbenhnhan.ten,t.noitru,c.macls,d.tencls,t.ngayct,t.taikhoan } into g
                   select new NhamLaiModel
                   {
                       mabn = g.Key.mabn,
                       makb = g.Key.makb,
                       hoten = g.Key.holot + " " + g.Key.ten,
                       noitru = g.Key.noitru.ToString(),
                       tencls=g.Key.tencls,
                       ngayct = g.Key.ngayct,
                       taikhoan = g.Key.taikhoan,
                       macls=g.Key.macls,
                       soluong = g.Sum(t3=>t3.soluong),
                       thanhtien = g.Sum(t3 => t3.thanhtien)
                   };

            if (query != null)
            {
                
                models = query.OrderBy(t => t.ngayct).ToList();
                models = models.Where(c => c.macls.StartsWith("DV") || c.macls.StartsWith("KB_DV") || c.macls.StartsWith("DVBS")).ToList();
            }
            return models;
        }
        private List<NhamLaiModel> GetThuChi2(string thangkt, string namkt)
        {
            List<NhamLaiModel> models = new List<NhamLaiModel>();
            IQueryable<NhamLaiModel> query;

            //query = storeDB.thuchi.Where(t=>t.thangkt==thangkt && t.namkt==namkt
            //                                  && t.xoa==0 && t.loaibldv==0 && (t.chidinhclss.Where(c=>c.macls.StartsWith("DV_") ||c.macls.StartsWith("KB_DV")).Count()>0) );
            query = from t in storeDB.thuchi
                    join c in storeDB.chidinhcls on t.soct equals c.soctvp
                    join d in storeDB.dmcls on c.macls equals d.macls
                    where t.thangkt == thangkt && t.namkt == namkt && t.xoa == 0 && t.loaibldv == 1 && c.namkt == namkt
                    // && (c.macls.StartsWith("DV") || c.macls.StartsWith("KB_DV") || c.macls.StartsWith("DVBS"))     
                    group c by new { t.mabn, t.makb, t.dmbenhnhan.holot, t.dmbenhnhan.ten, t.noitru, c.macls, d.tencls, t.ngayct, t.taikhoan } into g
                    select new NhamLaiModel
                    {
                        mabn = g.Key.mabn,
                        makb = g.Key.makb,
                        hoten = g.Key.holot + " " + g.Key.ten,
                        noitru = g.Key.noitru.ToString(),
                        tencls = g.Key.tencls,
                        ngayct = g.Key.ngayct,
                        taikhoan = g.Key.taikhoan,
                        macls = g.Key.macls,
                        soluong = g.Sum(t3 => t3.soluong),
                        thanhtien = g.Sum(t3 => t3.thanhtien)
                    };

            if (query != null)
            {

                models = query.OrderBy(t => t.ngayct).ToList();
                models = models.Where(c => !c.macls.StartsWith("DV") && !c.macls.StartsWith("KB_DV") && !c.macls.StartsWith("DVBS")).ToList();
            }
            return models;
        }

        public ActionResult AjaxHandler(jQueryDataTableParamModel param, string thangkt, string namkt)
        {
            List<NhamLaiModel> allBN = new List<NhamLaiModel>();
            List<NhamLaiModel> viewmodel = new List<NhamLaiModel>();
            int i = 1;
            allBN = GetThuChi(thangkt,namkt);
           
            IEnumerable<NhamLaiModel> filteredBN;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredBN = viewmodel
                         .Where(c => c.makb.Contains(param.sSearch)
                                     ||
                          c.hoten.ToLower().Contains(param.sSearch.ToLower())
                                     ||
                                     c.tencls.ToLower().Contains(param.sSearch.ToLower()));
            }
            else
            {
                filteredBN = allBN;
            }
            foreach (var item in filteredBN)
            {
                item.stt = i++;
                if (item.noitru == "1")
                {
                    item.noitru = "X";
                }
                else
                {
                    item.noitru = "";
                }
            }
           
            var displayedBn = filteredBN; //&& t.mabvdk.Substring(2, 3) != "000"
            var result = from c in displayedBn
                         select new object[] { c.stt.ToString(), c.mabn, c.makb, c.hoten,
                             c.noitru, c.tencls, c.soluong, c.thanhtien.ToString("N0"),c.ngayct.ToString("d/M/yyyy H:mm"),c.taikhoan };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = displayedBn.Count(),
                iTotalDisplayRecords = displayedBn.Count(),
                aaData = result
            },
                             JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjaxHandler2(jQueryDataTableParamModel param, string thangkt, string namkt)
        {
            List<NhamLaiModel> allBN = new List<NhamLaiModel>();
            List<NhamLaiModel> viewmodel = new List<NhamLaiModel>();
            int i = 1;
            allBN = GetThuChi2(thangkt, namkt);

            IEnumerable<NhamLaiModel> filteredBN;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredBN = viewmodel
                         .Where(c => c.makb.Contains(param.sSearch)
                                     ||
                          c.hoten.ToLower().Contains(param.sSearch.ToLower())
                                     ||
                                     c.tencls.ToLower().Contains(param.sSearch.ToLower()));
            }
            else
            {
                filteredBN = allBN;
            }
            foreach (var item in filteredBN)
            {
                item.stt = i++;
                if (item.noitru == "1")
                {
                    item.noitru = "X";
                }
                else
                {
                    item.noitru = "";
                }
            }

            var displayedBn = filteredBN; //&& t.mabvdk.Substring(2, 3) != "000"
            var result = from c in displayedBn
                         select new object[] { c.stt.ToString(), c.mabn, c.makb, c.hoten,
                             c.noitru, c.tencls, c.soluong, c.thanhtien.ToString("N0"),c.ngayct.ToString("d/M/yyyy H:mm"),c.taikhoan };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = displayedBn.Count(),
                iTotalDisplayRecords = displayedBn.Count(),
                aaData = result
            },
                             JsonRequestBehavior.AllowGet);
        }
    }
}