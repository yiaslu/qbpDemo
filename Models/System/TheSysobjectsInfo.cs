using PublicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.System
{
    [Serializable]
    [ModelAttribute("sysobjects", "")]
    public class TheSysobjectsInfo : BaseMODEL
    {
        private string _name;
        /// <summary>
        /// 表名
        /// </summary>
        [ModelAttribute(50, 0, false, "", "表名")]
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _xtype;

        /// <summary>
        /// 类型
        /// </summary>
        [ModelAttribute(50, 0, false, "", "类型")]
        public string xtype
        {
            get { return _xtype; }
            set { _xtype = value; }
        }

        /// <summary>
        /// 数据枚举
        /// </summary>
        public enum QueryItem
        {
            xtype, name
        }
    }
}
