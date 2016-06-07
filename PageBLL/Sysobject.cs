using DBHelp;
using Models.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PageBLL
{
    public class Sysobject : BLLBase<TheSysobjectsInfo>
    {
        public List<TheSysobjectsInfo> GetTableName()
        {
            try
            {
                query.Where(DBWhereType.AND, TheSysobjectsInfo.QueryItem.xtype, DBTermType.IN, "U，V");
                return query.SelectList("");
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
