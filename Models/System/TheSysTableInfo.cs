
using System;
using PublicClass;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Serializable]
    [Table("Sys_Table")]
    [ModelAttribute("Sys_Table", "")]
    public partial class TheSysTableInfo : BaseMODEL
    {
        
        private int? _tableid;
        /// <summary>
        /// 表格编号
        /// </summary>
        [ModelAttribute(4,"","表格编号")]
        [Key]
        public int? tableid
        {
            get { return _tableid; }
            set { _tableid = value; }
        }


        private string _tablename;
        /// <summary>
        /// 表格名称
        /// </summary>
        [ModelAttribute(50,0,false,"","表格名称")]
        public string tablename
        {
            get { return _tablename; }
            set { _tablename = value; }
        }


        private string _tablecode;
        /// <summary>
        /// 数据表名称
        /// </summary>
        [ModelAttribute(50,0,false,"","数据表名称")]
        public string tablecode
        {
            get { return _tablecode; }
            set { _tablecode = value; }
        }


        private string _tabletype;
        /// <summary>
        /// 表类型（table，view）
        /// </summary>
        [ModelAttribute(50,0,false,"","表类型（table，view）")]
        public string tabletype
        {
            get { return _tabletype; }
            set { _tabletype = value; }
        }


        private DateTime? _creadtime;
        /// <summary>
        /// 创建日期
        /// </summary>
        [ModelAttribute(4,0,false,"","创建日期")]
        public DateTime? creadtime
        {
            get { return _creadtime; }
            set { _creadtime = value; }
        }


        private string _tablecolumns;
        /// <summary>
        /// 表格列数据
        /// </summary>
        [ModelAttribute(1000,0,false,"","表格列数据")]
        public string tablecolumns
        {
            get { return _tablecolumns; }
            set { _tablecolumns = value; }
        }


        /// <summary>
        /// 数据枚举
        /// </summary>
        public enum QueryItem
        {
           tableid , tablename , tablecode , tabletype , creadtime , tablecolumns 
        }
    }
}

