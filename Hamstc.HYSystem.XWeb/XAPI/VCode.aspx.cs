using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Hamstc.HYSystem.XCommon;

namespace XinChuang.HYSystem2.XWeb.XAPI
{
    public partial class VCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string checkCode = CreateRandom.Num(4);
            Session["XCVCode"] = checkCode;
            CreateImage(checkCode);
        }
        private void CreateImage(string checkCode)
        {
            int iwidth = (int)(checkCode.Length * 30); //宽度
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 36);
            Graphics g = Graphics.FromImage(image);
            Font f = new System.Drawing.Font("Arial", 20, System.Drawing.FontStyle.Bold);
            Brush b = new System.Drawing.SolidBrush(Color.White);
            g.Clear(Color.FromArgb(200, 0, 0));
            g.DrawString(checkCode, f, b, 13, 3);
            //干扰线
            Pen blackPen = new Pen(Color.FromArgb(149, 215, 45), 0);
            Random rand = new Random();
            for (int i = 0; i < 4; i++)
            {
                int y = rand.Next(image.Height);
                g.DrawLine(blackPen, 0, y, image.Width, y);
            }
            //输出图片
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            Response.ClearContent();
            Response.ContentType = "image/Jpeg";
            Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            image.Dispose();
            ms.Close(); 
        }
    }
}