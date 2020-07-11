using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIMPHONGTRO.Models.DTO
{
    public class NewsListDTO
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string SortContent { get; set; }
        public string Picture { get; set; }
        public DateTime StartDate { get; set; }
        public string Fullname { get; set; }
    }
}