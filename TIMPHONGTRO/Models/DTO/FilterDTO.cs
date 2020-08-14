using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIMPHONGTRO.Models.DTO
{
    public class FilterDTO
    {
        public int CategoryId { get; set; }
        public int ProvincialId { get; set; }
        public int DistrictId { get; set; }
        public int StreetId { get; set; }
        public int minP { get; set; }
        public int maxP { get; set; }
        public string Area { get; set; }
    }
}