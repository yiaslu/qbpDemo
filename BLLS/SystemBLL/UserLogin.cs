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
                var User = new SysUserInfo();
                User.UserID = list[0].yhID + "";
                User.UserName = list[0].yhName + "";
                User.CaiDans = new CaiDan().GetUserCaiDan(list[0].yhID.Value);
                TheSysYongHuInfo yh = new TheSysYongHuInfo();
                yh.yhID = list[0].yhID;
                yh.yhLastLgoinTime = DateTime.Now;
                Update(yh);
                SetUserInfo(User);
                return true;
            }
            return false;
        }
    }
}
