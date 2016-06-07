using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace PublicClass
{
    public interface IUIHelp
    {
        void ShowMessage(string Message);
        void ShowError(Exception exp);
        SysUserInfo GetUserInfo();
        void SetUserInfo(SysUserInfo user);
        void SavePwd(ref string loginName, ref string pwd);
    }
}
