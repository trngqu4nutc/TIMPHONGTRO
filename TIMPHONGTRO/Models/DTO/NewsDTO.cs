using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIMPHONGTRO.Models.DTO
{
    public class NewsDTO
    {
        public int NewsId { get; set; }

        public int AccountId { get; set; }

        public int CategoryId { get; set; }

        public int ProvincialId { get; set; }

        public int DistrictId { get; set; }

        public int WardId { get; set; }

        public string HomeNum { get; set; }

        public string Address { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string SortContent { get; set; }

        public decimal Price { get; set; }

        public string Area { get; set; }

        public string Sex { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int ActiveFlag { get; set; }

        public List<string> Picture { get; set; }

        public string Fullname { get; set; }
    }
}