using DBHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace SystemBLL
{
    public class UserLogin : BLLBase<Models.TheSysYongHuInfo>
    {

        public bool Login(string userName, string userPass)
        {
            query.Where(DBWhereType.AND, TheSysYongHuInfo.QueryItem.yhLoginName, DBTermType.Equal, userName);
            query.Where(DBWhereType.AND, TheSysYongHuInfo.QueryItem.yhPassWord, DBTermType.Equal, PublicClass.PublicClass.Encrypt(userPass));
            var list = query.SelectList("");
            if (list.Count == 1)
            {
                UserInfo = new SysUserInfo();
                UserInfo.UserID = list[0].yhID + "";
                UserInfo.UserName = list[0].yhName + "";
                UserInfo.CaiDans = new CaiDan().GetUserCaiDan(list[0].yhID.Value);
                return true;
            }
            return false;
        }
    }
}
