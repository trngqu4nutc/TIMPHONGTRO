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
        public static News toNews(News news, NewDTO newDTO)
        {
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
            if(newDTO.newId == 0)
            {
                news.AccountId = newDTO.accountId;
                news.ActiveFlag = 0;
                news.StartDate = DateTime.Now;
            }
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
            return news;
        }
        public static NewDTO toDTO(News news)
        {
            var newDTO = new NewDTO();
            newDTO.newId = news.NewsId;
            newDTO.accountId = news.AccountId;
            newDTO.provincialId = news.ProvincialId;
            newDTO.districtId = news.DistrictId;
            newDTO.wardId = news.WardId;
            newDTO.streetId = news.StreetId;
            newDTO.categoryId = news.CategoryId;
            newDTO.title = news.Title;
            newDTO.shortContent = news.SortContent;
            newDTO.content = news.Content;
            newDTO.address = news.Address;
            newDTO.homeNum = news.HomeNum;
            newDTO.price = news.Price;
            newDTO.area = news.Area;
            newDTO.sex = news.Sex;
            newDTO.type = "day";
            newDTO.time = int.Parse((((((DateTimeOffset)news.EndDate).ToUnixTimeMilliseconds() - ((DateTimeOffset)news.StartDate).ToUnixTimeMilliseconds()) / 86400000)).ToString()) + 1;
            return newDTO;
        }
    }
}
