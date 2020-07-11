using MODEL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIMPHONGTRO.Models.DAO
{
    public interface INewsDao
    {
        bool Add(News news);
        IEnumerable<News> GetAll();
        bool Update(int id, News news);
        bool Delete(int id);
        News GetById(int id);
    }
}