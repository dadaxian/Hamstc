using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hamstc.HYSystem.XWeb.XAdmin
{
    public partial class XChangePWD : AdminMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrAdmin == null) RedirectToLogin();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (CurrAdmin.LoginPWD ==XCommon.Uitl.MD5(txtOPWD.Text)) //比较原来密码
            {
                CurrAdmin.LoginPWD =XCommon.Uitl.MD5(txtNPWD1.Text);
                bllXSysAdmin.Update(CurrAdmin);
                JscriptMsg("已成功提交保存！", string.Empty);
            }
            else
            {
                JscriptMsg("原来密码不正确，无法修改！", string.Empty);
            }
        }
    }
}