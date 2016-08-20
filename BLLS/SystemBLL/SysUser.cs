using DBHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemBLL
{
    public class SysUser : BLLBase<Models.TheSysYongHuInfo>
    {
        public Models.SysUserInfo GetUser()
        {
            return UserInfo;
        }

        public void Logout()
        {
            SetUserInfo(null);
        }
    }
}
