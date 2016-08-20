using DBHelp;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemBLL
{
    public class SysYongHu : BLLBase<TheSysYongHuInfo>
    {
        public override void Insert(TheSysYongHuInfo info)
        {
            query.Where(DBWhereType.AND, TheSysYongHuInfo.QueryItem.yhLoginName, DBTermType.Equal, info.yhLoginName);
            if (query.SelectList("").Count != 0)
            {
                throw new Exception("该用户已存在，请重新录入");
            }
            info.yhPassWord = "59434FEEB47B2564";
            base.Insert(info);
        }
        public void UpdatePassword(string ymm, string xmm)
        {
            query.Where(DBWhereType.AND, TheSysYongHuInfo.QueryItem.yhID, DBTermType.Equal, UserInfo.UserID);
            query.Where(DBWhereType.AND, TheSysYongHuInfo.QueryItem.yhPassWord, DBTermType.Equal, PublicClass.PublicClass.Encrypt(ymm));
            if (query.SelectList("").Count != 1)
            {
                throw new Exception("用户原密码错误，无法修改！");
            }
            var user = new TheSysYongHuInfo();
            user.yhID = int.Parse(UserInfo.UserID);
            user.yhPassWord = PublicClass.PublicClass.Encrypt(xmm);
            base.Update(user);
        }
    }
}
