
using System;
using PublicClass;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Serializable]
    [Table("Sys_JueSe_YongHu")]
    [ModelAttribute("Sys_JueSe_YongHu", "")]
    public class TheSysJueSeYongHuInfo : BaseMODEL
    {
        
        private int? _jsID;
        /// <summary>
        /// 角色编号
        /// </summary>
        [ModelAttribute(4,"","角色编号")]
        [Key]
        public int? jsID
        {
            get { return _jsID; }
            set { _jsID = value; }
        }


        private int? _yhID;
        /// <summary>
        /// 用户编号
        /// </summary>
        [ModelAttribute(4,"","用户编号")]
        public int? yhID
        {
            get { return _yhID; }
            set { _yhID = value; }
        }


        private int? _cdID;
        /// <summary>
        /// 菜单编号
        /// </summary>
        [ModelAttribute(4,"","菜单编号")]
        public int? cdID
        {
            get { return _cdID; }
            set { _cdID = value; }
        }


        /// <summary>
        /// 数据枚举
        /// </summary>
        public enum QueryItem
        {
           jsID , yhID , cdID 
        }
    }
}

