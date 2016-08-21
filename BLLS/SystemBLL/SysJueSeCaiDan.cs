using DBHelp;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemBLL
{
    public class SysJueSeCaiDan : BLLBase<TheSysJueSeCaiDanInfo>
    {
        public List<TheSysJueSeCaiDanInfo> GetCaiDanByJs(int JsID)
        {
            query.Where(DBWhereType.AND, TheSysJueSeCaiDanInfo.QueryItem.JsID, DBTermType.Equal, JsID);
            return GetListAll();
        }
        public void Save(List<TheSysJueSeCaiDanInfo> list)
        {
            if (list != null)
            {

                List<DataParameter> listpara = new List<DataParameter>();
                listpara.Add(new DataParameter("@jsID", list[0].JsID));
                exec.ExecuteSQL("delete Sys_JueSe_CaiDan where jsID=@jsID", listpara);
                foreach (var item in list)
                {
                    exec.Add(item);
                }
                exec.DBCommit();
            }
        }
    }
}
