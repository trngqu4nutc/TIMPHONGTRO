using MODEL.DTO;
using MODEL.EF;
using MODEL.UTIL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DAO
{
    public class NewsDAO
    {
        private NTDbContext _context;
        public NewsDAO()
        {
            _context = new NTDbContext();
        }
        public List<ProvincialDTO> GetProvincials()
        {
            var provincials = _context.Provincials.Select(x => new ProvincialDTO()
            {
                ProvincialId = x.ProvincialId,
                Name = x.Name
            }).ToList();
            return provincials;
        }
        public List<DistrictDTO> GetDistricts(int provincialId)
        {
            var districts = _context.Districts
                .Where(x => x.ProvincialId == provincialId)
                .Select(x => new DistrictDTO()
                {
                    DistrictId = x.DistrictId,
                    Name = x.Name
                }).ToList();
            return districts;
        }
        public List<WardDTO> GetWards(int districtId)
        {
            var wards = _context.Wards
                .Where(x => x.DistrictId == districtId)
                .Select(x => new WardDTO()
                {
                    WardId = x.WardId,
                    Name = x.Name
                }).ToList();
            return wards;
        }
        public List<StreetDTO> GetStreets(int districtId)
        {
            var streets = _context.Streets
                .Where(x => x.DistrictId == districtId)
                .Select(x => new StreetDTO()
                {
                    StreetId = x.StreetId,
                    Name = x.Name
                }).ToList();
            return streets;
        }
        public int AddNew(NewDTO newDTO)
        {
            News news = NewsMapper.toNews(newDTO);
            try
            {
                _context.News.Add(news);
                _context.SaveChanges();
                var newsId = (_context.News.FirstOrDefault(x => x.Title == newDTO.title)).NewsId;
                AddImages(newsId, newDTO.baseImages);
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        private void AddImages(int newsId, List<string> images)
        {
            foreach(var item in images)
            {
                var image = new Img()
                {
                    NewsId = newsId,
                    Picture = item
                };
                _context.Imgs.Add(image);
                _context.SaveChanges();
            }
        }
    }
}
