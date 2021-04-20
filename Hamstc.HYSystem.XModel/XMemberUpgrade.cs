using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hamstc.HYSystem.XModel
{
    public class XMemberUpgrade
    {
        public string MUID { get; set; }
        public string SID { get; set; }
        public int Grade1 { get; set; }
        public int Grade2 { get; set; }
        public string CID { get; set; }
        public int IsReply { get; set; }
        public int SortIndex { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

    }
}
