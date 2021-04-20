using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
namespace Hamstc.HYSystem.XCommon
{
    public class Uitl
    {
        public static string MD5(string txt)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txt, "MD5").ToLower();
        }
    }
}
