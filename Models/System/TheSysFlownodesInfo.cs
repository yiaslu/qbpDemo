
using System;
using PublicClass;

namespace Models
{
    [Serializable]
    [ModelAttribute("Sys_Flow_nodes", "")]
    public class TheSysFlownodesInfo : BaseMODEL
    {
        
        private string _nodecode;
        /// <summary>
        /// 节点编码
        /// </summary>
        [ModelAttribute(50,0,false,"","节点编码")]
        public string nodecode
        {
            get { return _nodecode; }
            set { _nodecode = value; }
        }


        private string _name;
        /// <summary>
        /// 节点名称
        /// </summary>
        [ModelAttribute(50,0,false,"","节点名称")]
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }


        private decimal? _left;
        /// <summary>
        /// 左
        /// </summary>
        [ModelAttribute(9,14,false,"","左")]
        public decimal? left
        {
            get { return _left; }
            set { _left = value; }
        }


        private decimal? _top;
        /// <summary>
        /// 上
        /// </summary>
        [ModelAttribute(9,14,false,"","上")]
        public decimal? top
        {
            get { return _top; }
            set { _top = value; }
        }


        private int? _height;
        /// <summary>
        /// 高
        /// </summary>
        [ModelAttribute(4,0,false,"","高")]
        public int? height
        {
            get { return _height; }
            set { _height = value; }
        }


        private int? _width;
        /// <summary>
        /// 宽
        /// </summary>
        [ModelAttribute(4,0,false,"","宽")]
        public int? width
        {
            get { return _width; }
            set { _width = value; }
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
           nodecode , name , left , top , height , width , type 
        }
    }
}

