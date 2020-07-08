using MODEL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIMPHONGTRO.Common
{
    public static class GenerateName
    {
        public static string doGenerate(string phoneNumber)
        {
            return phoneNumber + "_"
                + Guid.NewGuid().ToString().Substring(0, 4) + "_"
                + DateTimeOffset.Now.ToUnixTimeMilliseconds() + ".jpg";
        }
    }
}