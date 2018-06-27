using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalApplication.Web.Models
{
    public class ChartModel
    {
        public string Label { get; set; }
        public List<string> Date { get; set; }
        public List<int> Data { get; set; }
    }
}