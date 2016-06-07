
using System;
using PublicClass;

namespace Models
{
    [Serializable]
    [ModelAttribute("Sys_JueSe", "")]
    public class TheSysJueSeInfo : BaseMODEL
    {
        
        private int? _jsID;
        /// <summary>
        /// 角色编号
        /// </summary>
        [ModelAttribute(4,"","角色编号")]
        public int? jsID
        {
            get { return _jsID; }
            set { _jsID = value; }
        }


        private string _jsName;
        /// <summary>
        /// 角色名称
        /// </summary>
        [ModelAttribute(50,0,false,"","角色名称")]
        public string jsName
        {
            get { return _jsName; }
            set { _jsName = value; }
        }


        private string _jsMeno;
        /// <summary>
        /// 角色描述
        /// </summary>
        [ModelAttribute(500,0,false,"","角色描述")]
        public string jsMeno
        {
            get { return _jsMeno; }
            set { _jsMeno = value; }
        }


        private bool? _jsISTrue;
        /// <summary>
        /// 是否可用
        /// </summary>
        [ModelAttribute(1,0,false,"","是否可用")]
        public bool? jsISTrue
        {
            get { return _jsISTrue; }
            set { _jsISTrue = value; }
        }


        /// <summary>
        /// 数据枚举
        /// </summary>
        public enum QueryItem
        {
           jsID , jsName , jsMeno , jsISTrue 
        }
    }
}

