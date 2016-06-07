using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    [Serializable]
    public class SysUserInfo
    {
        private string _UserID;

        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private List<SysUserRole> _Roles;

        public List<SysUserRole> Roles
        {
            get { return _Roles; }
            set { _Roles = value; }
        }
    }
    [Serializable]
    public class SysUserRole
    {
        private string _vModularCode;

        public string vModularCode
        {
            get { return _vModularCode; }
            set { _vModularCode = value; }
        }
        private string _vParentID;

        public string vParentID
        {
            get { return _vParentID; }
            set { _vParentID = value; }
        }
        private string _RoleName;

        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }

    }
}
