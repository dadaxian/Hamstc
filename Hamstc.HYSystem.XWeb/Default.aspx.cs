using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.IO;

namespace Hamstc.HYSystem.XWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPreInit(EventArgs e)
        {
            string masterPath = "";
            if (Request.QueryString["cmd"] != null)
            {
                string cmd = Server.UrlEncode(Request.QueryString["cmd"]);
                masterPath = WebConfigurationManager.AppSettings.Get(cmd);
            }
            if (masterPath != null && masterPath.Length > 0)
            {
                if (File.Exists(Server.MapPath(masterPath))) this.MasterPageFile = masterPath;
            }
        }
    }
}