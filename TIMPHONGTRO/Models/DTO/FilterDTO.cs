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
        public int Price { get; set; }
        public string Area { get; set; }
    }
}