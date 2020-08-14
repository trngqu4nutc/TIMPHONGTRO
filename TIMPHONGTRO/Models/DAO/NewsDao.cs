using MODEL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using TIMPHONGTRO.Models.DTO;

namespace TIMPHONGTRO.Models.DAO
{
    public class NewsDao : INewsDao
    {
        private readonly NTDbContext db;

        public NewsDao()
        {
            db = new NTDbContext();
        }

        public bool Add(News news)
        {
            try
            {
                db.News.Add(news);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                db.News.Remove(db.News.Find(id));
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<News> GetAll()
        {
            return db.News.Where(x => x.ActiveFlag == 1).OrderByDescending(x => x.StartDate).ToList();
        }

        public IEnumerable<NewsDTO> GetNewsDTOs()
        {
            var q = from n in db.News
                    join a in db.Accounts on n.AccountId equals a.AccountId
                    select new { n, a };
            List<NewsDTO> news = new List<NewsDTO>();
            foreach (var item in q)
            {
                NewsDTO newsDTO = new NewsDTO
                {
                    NewsId = item.n.NewsId,
                    Title = item.n.Title,
                    StartDate = item.n.StartDate,
                    SortContent = item.n.SortContent,
                    Content = item.n.Content
                };
                var p = item.n.Imgs.Where(x => x.NewsId == item.n.NewsId).ToList()[0].Picture;
                newsDTO.Picture = new List<string>
                {
                    p
                };
                newsDTO.Fullname = item.a.Fullname;
                news.Add(newsDTO);
            }
            return news;
        }


        public News GetById(int id)
        {
            return db.News.Find(id);
        }

        public bool Update(int id, News news)
        {
            try
            {
                var oldNews = db.News.Find(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Categories.OrderByDescending(x => x.CategoryId).ToList();
        }

        public IEnumerable<NewsDTO> FilterNews(FilterDTO filterDTO)
        {
            var dateTime = DateTime.Now;
            var news = db.News.Where(x => x.CategoryId == filterDTO.CategoryId && x.ActiveFlag == 1 && x.EndDate > dateTime && x.Price/1000000 > filterDTO.minP && x.Price/1000000 < filterDTO.maxP);
            if (filterDTO.ProvincialId != 0)
            {
                news = news.Where(x => x.ProvincialId == filterDTO.ProvincialId);
            }
            if (filterDTO.DistrictId != 0)
            {
                news = news.Where(x => x.ProvincialId == filterDTO.ProvincialId);
            }
            if (filterDTO.StreetId != 0)
            {
                news = news.Where(x => x.StreetId == filterDTO.StreetId);
            }
            if (filterDTO.Area != "0")
            {
                news = news.Where(x => x.Area.Equals(filterDTO.Area));
            }
            var dto = news.OrderByDescending(x => x.StartDate);

            var q = from n in dto
                    join a in db.Accounts on n.AccountId equals a.AccountId
                    select new { n, a };

            List<NewsDTO> filter = new List<NewsDTO>();
            foreach (var item in q)
            {
                var n = new NewsDTO
                {
                    NewsId = item.n.NewsId,
                    Picture = new List<string>
                {
                    item.n.Imgs.Where(x => x.NewsId == item.n.NewsId).ToList()[0].Picture
                },
                    SortContent = item.n.SortContent,
                    Title = item.n.Title,
                    Fullname = item.a.Fullname,
                    StartDate = item.n.StartDate
                };
                filter.Add(n);
            }
            return filter;
        }

    }
}