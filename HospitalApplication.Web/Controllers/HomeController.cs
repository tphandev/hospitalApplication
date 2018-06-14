using HospitalApplication.Entities.Models;
using HospitalApplication.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace HospitalApplication.Web.Controllers
{
    public class HomeController : Controller
    {
        HADbContext storeDB = new HADbContext();
        string userName = "94029_TC";
        string passWord = "887d97739f09f12eb13f8b430ae4ccc2";
        private List<bnnoitruModels> GetBNDangDieuTri(string madv, string thangkt, string namkt)
        {
            List<bnnoitruModels> models = new List<bnnoitruModels>();
            IQueryable<bnnoitru> query;
            if (madv == "All")
            {
                 query = storeDB.bnnoitru.Where(t => t.ngayrv == null && t.thangkt == thangkt && t.namkt == namkt  && (t.madt == "01" || t.madt == "03")).OrderBy(t => t.ngayvv);
            }
            else
            {
                 query = storeDB.bnnoitru.Where(t => t.ngayrv == null && t.thangkt == thangkt && t.namkt == namkt && t.madv == madv && (t.madt == "01" || t.madt == "03")).OrderBy(t => t.ngayvv);
            }
          
            if (query != null)
            {
                models = query.Select(m => new bnnoitruModels
                {
                    ngayvv = m.ngayvv,
                    ngayrv = null,
                    maba = m.maba,
                    mabn = m.mabn,
                    madv = m.madv,
                    holot = m.dmbenhnhan.holot,
                    ten = m.dmbenhnhan.ten,
                    tendv = m.dmdonvi.tendv,
                    ngaysinh = m.dmbenhnhan.ngaysinh,
                    gioitinh = m.dmbenhnhan.gioitinh,
                    mathe = m.mathe,
                    mabvdk = m.mabvdk,
                    ngaybd = m.ngaybd,
                    ngaykt = m.ngaykt,
                    namsinh = m.namsinh
                }).ToList();
            }
            return models;
        }

        private List<bnnoitruModels> GetBNXuatVien(string madv, DateTime tungay, DateTime denngay)
        {
            List<bnnoitruModels> models = new List<bnnoitruModels>();
            IQueryable<bnnoitru> models1 ;
            if (madv== "All")
            {
                 models1 = storeDB.bnnoitru.Where(t => tungay <= t.ngayrv && denngay >= t.ngayrv && (t.madt == "01" || t.madt == "03")).OrderBy(t=>t.ngayrv);
            }
            else {
                models1 = storeDB.bnnoitru.Where(t => tungay <= t.ngayrv && denngay >= t.ngayrv && t.madv == madv && (t.madt == "01" || t.madt == "03")).OrderBy(t => t.ngayrv);
            }
            
            if (models1 != null)
            {
                models = models1.Select(m => new bnnoitruModels
                {
                    ngayvv = m.ngayvv,
                    ngayrv = m.ngayrv.Value,
                    maba = m.maba,
                    mabn = m.mabn,
                    madv = m.madv,
                    holot = m.dmbenhnhan.holot,
                    ten = m.dmbenhnhan.ten,
                    tendv = m.dmdonvi.tendv,
                    ngaysinh = m.dmbenhnhan.ngaysinh,
                    gioitinh = m.dmbenhnhan.gioitinh,
                    mathe = m.mathe,
                    mabvdk = m.mabvdk,
                    ngaybd = m.ngaybd,
                    ngaykt = m.ngaykt,
                    namsinh = m.namsinh
                }).ToList();
            }
            return models;
        }

        private async Task<SessionModels> DangNhap()
        {
            SessionModels dangnhap = new SessionModels();
            using (var handler = new HttpClientHandler())
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri("http://egw.baohiemxahoi.gov.vn/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //http post
                var value = new Dictionary<string, string>
                {
                    {"username",userName },
                    {"password",passWord }
                };
                var content = new FormUrlEncodedContent(value);
                HttpResponseMessage response = await client.PostAsync("api/token/take", content);
                string responseBody = await response.Content.ReadAsStringAsync();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                dangnhap = serializer.Deserialize<SessionModels>(responseBody);
            }
            return dangnhap;
        }
        private async Task<LichSuKCB> KTBHYT(bnnoitruModels model, string access_token, string id_token)
        {
            LichSuKCB lskcb = new LichSuKCB();

            int gt = 1;
            string ngaysinh;
            if (model.gioitinh == 0)
            {
                gt = 2;
            }
            if (model.namsinh == 1)
            {
                ngaysinh = model.ngaysinh.ToString("yyyy");
            }
            else
            {
                ngaysinh = model.ngaysinh.ToString("dd/MM/yyyy");
            }
            using (var handler = new HttpClientHandler())
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri("http://egw.baohiemxahoi.gov.vn/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string querypara = string.Format("token={0}&id_token={1}&username={2}&password={3}", access_token, id_token, userName, passWord);
                var value = new Dictionary<string, string>
                {
                    {"maThe",model.mathe },
                    {"hoTen",string.Format("{0} {1}",model.holot,model.ten)},
                    {"ngaySinh",ngaysinh},
                    {"gioiTinh",gt.ToString()},
                    {"maCSKCB",model.mabvdk},
                    {"ngayBD",model.ngaybd.ToString("dd/MM/yyyy")},
                    {"ngayKT",model.ngaykt.ToString("dd/MM/yyyy")},
                };
                var content = new FormUrlEncodedContent(value);
                HttpResponseMessage response = await client.PostAsync("api/egw/KQNhanLichSuKCB595?" + querypara, content);

                string responseBody = await response.Content.ReadAsStringAsync();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                lskcb = serializer.Deserialize<LichSuKCB>(responseBody);
            }
            return lskcb;
        }
        private string TenLoi(string maKQ)
        {
            string kq = "";
            switch (maKQ)
            {
                case "000":
                    kq = maKQ + " - Thông tin thẻ chính xác";
                    break;
                case "001":
                    kq = maKQ + " - Thẻ do BHXH Bộ Quốc Phòng quản lý, đề nghị kiểm tra thẻ và thông tin giấy tờ tùy thân";
                    break;
                case "002":
                    kq = maKQ + " - Thẻ do BHXH Bộ Công An quản lý, đề nghị kiểm tra thẻ và thông tin giấy tờ tùy thân";
                    break;
                case "010":
                    kq = maKQ + " - Thẻ hết giá trị sử dụng";
                    break;
                case "051":
                    kq = maKQ + " - Mã thẻ không đúng";
                    break;
                case "052":
                    kq = maKQ + " - Mã tỉnh cấp thẻ (kí tự thứ 4,5 của mã thẻ) không đúng";
                    break;
                case "053":
                    kq = maKQ + " - Mã quyền lợi thẻ (kí tự thứ 3 của mã thẻ) không đúng";
                    break;
                case "050":
                    kq = maKQ + " - Không thấy thông tin thẻ BHYT";
                    break;
                case "060":
                    kq = maKQ + " - Thẻ sai họ tên";
                    break;
                case "061":
                    kq = maKQ + " - Thẻ sai họ tên (đúng kí tự đầu)";
                    break;
                case "070":
                    kq = maKQ + " - Thẻ sai ngày sinh";
                    break;
                case "080":
                    kq = maKQ + " - Thẻ sai giới tính";
                    break;
                case "090":
                    kq = maKQ + " - Thẻ sai nơi đăng ký KCB ban đầu";
                    break;
                case "100":
                    kq = maKQ + " - Lỗi khi lấy dữ liệu thẻ";
                    break;
                case "101":
                    kq = maKQ + " - Lỗi server";
                    break;
                case "110":
                    kq = maKQ + " - Thẻ đã thu hồi";
                    break;
                case "120":
                    kq = maKQ + " - Thẻ đã báo giảm";
                    break;
                case "121":
                    kq = maKQ + " - Thẻ đã báo giảm. Giảm chuyển ngoại tỉnh";
                    break;
                case "122":
                    kq = maKQ + " - Thẻ đã báo giảm. Giảm chuyển nội tỉnh";
                    break;
                case "123":
                    kq = maKQ + " - Thẻ đã báo giảm. Thu hồi do tăng lại cùng đơn vị";
                    break;
                case "124":
                    kq = maKQ + " - Thẻ đã báo giảm. Ngừng tham gia";
                    break;
                case "130":
                    kq = maKQ + " - Trẻ em không xuất trình thẻ";
                    break;
                default:                    
                    break;

            }
            return kq;
        }

        private List<SelectListModels> GetKhoa()
        {
            List<SelectListModels> models = new List<SelectListModels>();
            models = storeDB.dmdonvi.Where(t => t.xoa == 0 && t.khoaduoc == 4).Select(m => new SelectListModels
            {
                Value = m.madv,
                Name = m.tendv

            }).ToList();

            return models;
        }
        public ActionResult Index()
        {
            //bnnoitruModels model = new bnnoitruModels(); 
            //model=storeDB.bnnoitru.Where(t => t.maba == "2017009456").Select(m=> new bnnoitruModels { maba=m.maba,mabn=m.mabn,holot=m.dmbenhnhan.holot,ten=m.dmbenhnhan.ten}).Single();

            List<SelectListModels> khoa = GetKhoa();
            khoa.Add(new SelectListModels { Value = "All", Name = "TẤT CẢ" });

            ViewBag.khoalist = new SelectList(khoa, "Value", "Name");


            return View();
        }


        public async Task<ActionResult> AjaxHandler(jQueryDataTableParamModel param, string enddate, string startdate, string madv, int isXuatVien)
        {
            List<bnnoitruModels> allBN = new List<bnnoitruModels>();
            if (isXuatVien == 1)
            {
                DateTime tungay = DateTime.ParseExact(startdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime denngay = DateTime.ParseExact(enddate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                allBN = GetBNXuatVien(madv, tungay, denngay.AddDays(1));
            }
            else
            {
                string thangkt = DateTime.Now.Month.ToString("d2");
                string namkt = DateTime.Now.Year.ToString();
                allBN = GetBNDangDieuTri(madv, thangkt, namkt);
            }

            IEnumerable<bnnoitruModels> filteredBN;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredBN = allBN
                         .Where(c => c.maba.Contains(param.sSearch)
                                     ||
                          c.holot.ToLower().Contains(param.sSearch.ToLower())
                                     ||
                                     c.ten.ToLower().Contains(param.sSearch.ToLower()));
            }
            else
            {
                filteredBN = allBN;
            }
            SessionModels session = await DangNhap();
            int i = 1;
            foreach (var item in filteredBN)
            {
                LichSuKCB ls = await KTBHYT(item, session.Apikey.access_token, session.Apikey.id_token);
                item.maKetQua = ls.maKetQua;
                item.ketQua = TenLoi(item.maKetQua);
                //KTBHYT();
                if(item.maKetQua != "000")//&& item.mabvdk.Substring(2, 3) != "000"
                {
                    item.stt = i++;
                }
            }
            var displayedBn = filteredBN.Where(t => t.maKetQua != "000"); //&& t.mabvdk.Substring(2, 3) != "000"
            var result = from c in displayedBn
                         select new[] { c.stt.ToString() ,c.maba, c.holot, c.ten, c.ngayvv.ToString("d/M/yyyy H:mm"), c.ngayrv.HasValue ? c.ngayrv.Value.ToString("d/M/yyyy H:mm"): "", c.tendv, c.ketQua };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = displayedBn.Count(),
                iTotalDisplayRecords = displayedBn.Count(),
                aaData = result
            },
                             JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}