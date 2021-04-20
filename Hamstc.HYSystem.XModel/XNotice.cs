using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hamstc.HYSystem.XModel
{
    public class XNotice
    {
        public string NID { get; set; }
        public string NType { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public int IsPublish { get; set; }
        public int SortIndex { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

    }
}
