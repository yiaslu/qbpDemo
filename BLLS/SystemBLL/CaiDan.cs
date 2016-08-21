using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DBHelp;

namespace SystemBLL
{
    public class CaiDan : BLLBase<TheSysCaiDanInfo>
    {
        public List<TheSysCaiDanInfo> GetUserCaiDan(int UserID)
        {
            var listDara = new List<DataParameter>();
            listDara.Add(new DataParameter("@userid", UserID));
            query.AddListPara(listDara);
            query.WhereAddSql(DBWhereType.AND, TheSysCaiDanInfo.QueryItem.cdID, DBTermType.IN, "(select cdID from Sys_JueSe_CaiDan where jsID in (select jsID from Sys_JueSe_YongHu where yhid=@userid))");
            return query.SelectList("cdxh");
        }
    }
}
