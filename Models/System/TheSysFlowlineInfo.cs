
using System;
using PublicClass;

namespace Models
{
    [Serializable]
    [ModelAttribute("Sys_Flow_line", "")]
    public class TheSysFlowlineInfo : BaseMODEL
    {
        
        private string _linecode;
        /// <summary>
        /// 线编码
        /// </summary>
        [ModelAttribute(50,0,false,"","线编码")]
        public string linecode
        {
            get { return _linecode; }
            set { _linecode = value; }
        }


        private string _name;
        /// <summary>
        /// 名称
        /// </summary>
        [ModelAttribute(50,0,false,"","名称")]
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }


        private string _from;
        /// <summary>
        /// 发起节点
        /// </summary>
        [ModelAttribute(50,0,false,"","发起节点")]
        public string from
        {
            get { return _from; }
            set { _from = value; }
        }


        private string _to;
        /// <summary>
        /// 结束节点
        /// </summary>
        [ModelAttribute(50,0,false,"","结束节点")]
        public string to
        {
            get { return _to; }
            set { _to = value; }
        }


        private bool? _marked;
        /// <summary>
        /// 标记
        /// </summary>
        [ModelAttribute(1,0,false,"","标记")]
        public bool? marked
        {
            get { return _marked; }
            set { _marked = value; }
        }


        private string _type;
        /// <summary>
        /// 类型
        /// </summary>
        [ModelAttribute(10,0,false,"","类型")]
        public string type
        {
            get { return _type; }
            set { _type = value; }
        }


        /// <summary>
        /// 数据枚举
        /// </summary>
        public enum QueryItem
        {
           linecode , name , from , to , marked , type 
        }
    }
}

