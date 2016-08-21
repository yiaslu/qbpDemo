using DBHelp;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemBLL
{
    public class SysJueSeYongHu : BLLBase<TheSysJueSeYongHuInfo>
    {
        public List<TheSysJueSeYongHuInfo> GetListJsByYongHu(int yhID)
        {
            query.Where(DBWhereType.AND, TheSysJueSeYongHuInfo.QueryItem.yhID, DBTermType.Equal, yhID);
            return base.GetListAll();
        }

        public void SaveJsYongHu(List<TheSysJueSeYongHuInfo> list)
        {
            if (list != null)
            {
                List<DataParameter> listpara = new List<DataParameter>();
                listpara.Add(new DataParameter("@yhID", list[0].yhID));
                exec.ExecuteSQL("delete Sys_JueSe_YongHu where yhID=@yhID", listpara);
                foreach (var item in list)
                {
                    exec.Add(item);
                }
                exec.DBCommit();
            }
        }

    }
}
