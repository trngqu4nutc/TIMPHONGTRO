using MODEL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIMPHONGTRO.Models.DAO
{
    public class QuanDuongDao
    {
        private readonly NTDbContext db;

        public QuanDuongDao()
        {
            db = new NTDbContext();
        }

        public IEnumerable<District> GetDistricts()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Districts.ToList();
        }

        public IEnumerable<Street> GetStreets(int DistrictId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Streets.Where(x => x.DistrictId == DistrictId).ToList();
        }
    }
}