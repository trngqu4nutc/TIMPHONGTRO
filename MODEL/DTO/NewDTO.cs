using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTO
{
    public class NewDTO
    {
        public int newId { get; set; }
        public int accountId { get; set; }
        public int provincialId { get; set; }
        public int districtId { get; set; }
        public int wardId { get; set; }
        public int streetId { get; set; }
        public int activeFlag { get; set; }
        public string fullname { get; set; }
        public string homeNum { get; set; }
        public string address { get; set; }
        public int categoryId { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string shortContent { get; set; }
        public decimal price { get; set; }
        public string area { get; set; }
        public string sex { get; set; }
        public List<string> baseImages { get; set; }
        public string type { get; set; }
        public int time { get; set; }
    }
}
