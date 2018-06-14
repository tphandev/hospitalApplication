using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalApplication.Web.Models
{
    public class SessionModels
    {
        public string maKetQua { get; set; }
        public APIKey Apikey { get; set; }
    }
    public class APIKey
    {
        public string access_token { get; set; }
        public string id_token { get; set; }
        public DateTime expires_in { get; set; }
        public string token_type { get; set; }

    }
}