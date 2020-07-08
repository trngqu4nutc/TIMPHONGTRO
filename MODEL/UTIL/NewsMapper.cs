using MODEL.DTO;
using MODEL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.UTIL
{
    public static class NewsMapper
    {
        public static News toNews(NewDTO newDTO)
        {
            News news = new News();
            news.AccountId = newDTO.accountId;
            news.ProvincialId = newDTO.provincialId;
            news.DistrictId = newDTO.districtId;
            news.WardId = newDTO.wardId;
            news.StreetId = newDTO.streetId;
            news.HomeNum = newDTO.homeNum;
            news.Address = newDTO.address;
            news.CategoryId = newDTO.categoryId;
            news.Title = newDTO.title;
            news.SortContent = newDTO.shortContent;
            news.Content = newDTO.content;
            news.Price = newDTO.price;
            news.Area = newDTO.area;
            news.Sex = newDTO.sex;
            news.StartDate = DateTime.Now;
            var startMiliseconds = ((DateTimeOffset)news.StartDate).ToUnixTimeMilliseconds();
            if (newDTO.type.Equals("day"))
            {
                var totalMiliseconds = startMiliseconds + newDTO.time * 86400000;
                var date = (new DateTime(1970, 1, 1)).AddMilliseconds(totalMiliseconds);
                news.EndDate = date;
            }else if (newDTO.type.Equals("week"))
            {
                var totalMiliseconds = startMiliseconds + newDTO.time * 7 * 86400000;
                var date = (new DateTime(1970, 1, 1)).AddMilliseconds(totalMiliseconds);
                news.EndDate = date;
            }
            else
            {
                var totalMiliseconds = startMiliseconds + newDTO.time * 30 * 86400000;
                var date = (new DateTime(1970, 1, 1)).AddMilliseconds(totalMiliseconds);
                news.EndDate = date;
            }
            news.ActiveFlag = 0;
            return news;
        }
    }
}
