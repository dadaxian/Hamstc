using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hamstc.HYSystem.XModel
{
    public class XMember
    {
        public string MID { get; set; }
        public string PCode { get; set; }
        public string PCodeStr { get; set; }
        public string CurrentCode { get; set; }
        public string NickName { get; set; }
        public string WXCode { get; set; }
        public string Phone { get; set; }
        public string InviteCode { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Remark { get; set; }
        public string LoginName { get; set; }
        public string LoginPWD { get; set; }
        public string LoginSign { get; set; }
        public string LoginIP { get; set; }
        public DateTime LoginTime { get; set; }
        public string ManagePWD { get; set; }
        public int IsDisable { get; set; }
        public int Grade { get; set; }
        public int SortIndex { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

    }
}
