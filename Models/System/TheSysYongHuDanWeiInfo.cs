
using System;
using PublicClass;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Serializable]
    [Table("Sys_YongHu_DanWei")]
    [ModelAttribute("Sys_YongHu_DanWei", "")]
    public class TheSysYongHuDanWeiInfo : BaseMODEL
    {
        
        private int? _yhID;
        /// <summary>
        /// 用户编号
        /// </summary>
        [ModelAttribute(4,"","用户编号")]
        [Key]
        public int? yhID
        {
            get { return _yhID; }
            set { _yhID = value; }
        }


        private int? _bmID;
        /// <summary>
        /// 部门编号
        /// </summary>
        [ModelAttribute(4,"","部门编号")]
        public int? bmID
        {
            get { return _bmID; }
            set { _bmID = value; }
        }


        private int? _dwID;
        /// <summary>
        /// 单位编号
        /// </summary>
        [ModelAttribute(4,"","单位编号")]
        public int? dwID
        {
            get { return _dwID; }
            set { _dwID = value; }
        }


        /// <summary>
        /// 数据枚举
        /// </summary>
        public enum QueryItem
        {
           yhID , bmID , dwID 
        }
    }
}

