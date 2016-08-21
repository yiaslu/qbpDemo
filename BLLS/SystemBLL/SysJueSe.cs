using DBHelp;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemBLL
{
    public class SysJueSe : BLLBase<TheSysJueSeInfo>
    {
        public override List<TheSysJueSeInfo> GetListAll()
        {
            query.Where(DBWhereType.AND, TheSysJueSeInfo.QueryItem.jsISTrue, DBTermType.Equal, true);
            return base.GetListAll();
        }

        public override void Insert(Models.TheSysJueSeInfo info)
        {
            info.jsISTrue = true;
            base.Insert(info);
        }
    }
}
