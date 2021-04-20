using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hamstc.HYSystem.XModel
{
    public class XMemberInvite
    {
        public string MIID { get; set; }
        public string MID { get; set; }
        public string CID { get; set; }
        public int Grade1 { get; set; }
        public int Grade2 { get; set; }
        public int IsAdd { get; set; }
        public int IsReply { get; set; }
        public int SortIndex { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

    }
}
