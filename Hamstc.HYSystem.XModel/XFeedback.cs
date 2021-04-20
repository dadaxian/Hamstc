using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hamstc.HYSystem.XModel
{
    public class XFeedback
    {
        public string FID { get; set; }
        public string MID { get; set; }
        public string FType { get; set; }
        public string Info { get; set; }
        public int IsReply { get; set; }
        public int SortIndex { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

    }
}
