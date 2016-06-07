using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBHelp;
using Models;

namespace PageBLL
{
    public class FlowSet : BLLBase<TheSysFlowInfo>
    {
        public override void Insert(Models.TheSysFlowInfo info)
        {
            base.Insert(info);
        }
    }
}
