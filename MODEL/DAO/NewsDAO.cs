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
            News news = new News();
            news = NewsMapper.toNews(news, newDTO);
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
        public int UpdateNew(NewDTO newDTO)
        {
            News news = _context.News.Find(newDTO.newId);
            news = NewsMapper.toNews(news, newDTO);
            try
            {
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
        public NewDTO FindById(int newsId)
        {
            var news = _context.News.Find(newsId);
            var newDTO = NewsMapper.toDTO(news);
            newDTO.baseImages = new List<string>();
            return newDTO;
        }
        public NewDTO GetDetail(int newsId)
        {
            var news = _context.News.Find(newsId);
            var newDTO = new NewDTO();
            newDTO.address = news.Address;
            newDTO.title = news.Title;
            newDTO.shortContent = news.SortContent;
            newDTO.content = news.Content;
            return newDTO;
        }
        public PagedResult<NewDTO> GetAllPaging(string fullname, string sex, int status, int page, int pageSize)
        {
            var query = from n in _context.News
                        select n;
            if (!string.IsNullOrEmpty(fullname))
            {
                query = query.Where(x => x.Account.Fullname.Contains(fullname));
            }
            if (!string.IsNullOrEmpty(sex))
            {
                query = query.Where(x => x.Sex == sex);
            }
            if (status != -2)
            {
                query = query.Where(x => x.ActiveFlag == status);
            }
            var result = new PagedResult<NewDTO>();
            result.TotalRecord = query.Count();
            result.Items = query.OrderByDescending(x => x.StartDate)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .Select(x => new NewDTO()
                {
                    newId = x.NewsId,
                    fullname = x.Account.Fullname,
                    address = x.Address,
                    price = x.Price,
                    area = x.Area,
                    sex = x.Sex,
                    type = (x.EndDate.Day + "/" + x.EndDate.Month + "/" + x.EndDate.Year),
                    time = x.ActiveFlag
                }).ToList();
            return result;
        }
        public PagedResult<NewDTO> GetAllPaging(string sex, int status, int page, int pageSize, int accountId)
        {
            var query = from n in _context.News
                        where n.AccountId == accountId
                        select n;
            if(!string.IsNullOrEmpty(sex))
            {
                query = query.Where(x => x.Sex == sex);
            }
            if(status != -2)
            {
                query = query.Where(x => x.ActiveFlag == status);
            }
            var result = new PagedResult<NewDTO>();
            result.TotalRecord = query.Count();
            result.Items = query.OrderByDescending(x => x.StartDate)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .Select(x => new NewDTO()
                {
                    newId = x.NewsId,
                    address = x.Address,
                    price = x.Price,
                    area = x.Area,
                    sex = x.Sex,
                    type = (x.EndDate.Day + "/" + x.EndDate.Month + "/" + x.EndDate.Year),
                    time = x.ActiveFlag
                }).ToList();
            return result;
        }
        public int UpdateStatus(int newId, int status)
        {
            var news = _context.News.Find(newId);
            news.ActiveFlag = status;
            try
            {
                _context.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public List<string> GetImagesByNewId(int newId)
        {
            return _context.Imgs
                .Where(x => x.NewsId == newId)
                .Select(x => x.Picture)
                .ToList();
        }
        public int DeleteImage(string picture)
        {
            var img = _context.Imgs.FirstOrDefault(x => x.Picture == picture);
            try
            {
                _context.Imgs.Remove(img);
                _context.SaveChanges();
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
