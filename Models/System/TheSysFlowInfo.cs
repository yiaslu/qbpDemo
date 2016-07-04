
using System;
using PublicClass;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Serializable]
    [Table("Sys_Flow")]
    [ModelAttribute("Sys_Flow", "")]
    public class TheSysFlowInfo : BaseMODEL
    {
        
        private int? _flowid;
        /// <summary>
        /// 流程编号
        /// </summary>
        [ModelAttribute(4,"","流程编号")]
        [Key]
        public int? flowid
        {
            get { return _flowid; }
            set { _flowid = value; }
        }


        private string _flowname;
        /// <summary>
        /// 流程名称
        /// </summary>
        [ModelAttribute(50,0,false,"","流程名称")]
        public string flowname
        {
            get { return _flowname; }
            set { _flowname = value; }
        }


        private DateTime? _createtime;
        /// <summary>
        /// 创建日期
        /// </summary>
        [ModelAttribute(4,0,false,"","创建日期")]
        public DateTime? createtime
        {
            get { return _createtime; }
            set { _createtime = value; }
        }


        private string _flownodes;
        /// <summary>
        /// 流程节点
        /// </summary>
        [ModelAttribute(2000,0,false,"","流程节点")]
        public string flownodes
        {
            get { return _flownodes; }
            set { _flownodes = value; }
        }


        private string _flowlines;
        /// <summary>
        /// 流程线
        /// </summary>
        [ModelAttribute(2000,0,false,"","流程线")]
        public string flowlines
        {
            get { return _flowlines; }
            set { _flowlines = value; }
        }


        private string _flowsets;
        /// <summary>
        /// 节点操作
        /// </summary>
        [ModelAttribute(16,0,false,"","节点操作")]
        public string flowsets
        {
            get { return _flowsets; }
            set { _flowsets = value; }
        }


        /// <summary>
        /// 数据枚举
        /// </summary>
        public enum QueryItem
        {
           flowid , flowname , createtime , flownodes , flowlines , flowsets 
        }
    }
}

