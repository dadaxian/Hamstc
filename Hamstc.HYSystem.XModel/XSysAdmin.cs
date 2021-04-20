using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hamstc.HYSystem.XModel
{
    public class XSysAdmin
    {
        public string SAID { get; set; }
        public string LoginName { get; set; }
        public string LoginPWD { get; set; }
        public string LoginSign { get; set; }
        public int Grade { get; set; }
        public int IsDisable { get; set; }
        public DateTime DisableTime { get; set; }
        public int SortIndex { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }


    }
}
