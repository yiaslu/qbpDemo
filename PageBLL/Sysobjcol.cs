using DBHelp;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PageBLL
{
    public class Sysobjcol : BLLBase<ThesystablecolInfo>
    {
        public List<ThesystablecolInfo> GetTableCol(string tablecode)
        {
            try
            {
                query.Where(DBWhereType.AND, ThesystablecolInfo.QueryItem.tablecode, DBTermType.Equal, tablecode);
                return query.SelectList("isPKey desc");
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
