
using System;
using PublicClass;

namespace Models
{
    [Serializable]
    [ModelAttribute("Sys_DanWei", "")]
    public class TheSysDanWeiInfo : BaseMODEL
    {
        
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


        private string _dwName;
        /// <summary>
        /// 单位名称
        /// </summary>
        [ModelAttribute(50,0,false,"","单位名称")]
        public string dwName
        {
            get { return _dwName; }
            set { _dwName = value; }
        }


        private string _dwMeno;
        /// <summary>
        /// 单位描述
        /// </summary>
        [ModelAttribute(500,0,false,"","单位描述")]
        public string dwMeno
        {
            get { return _dwMeno; }
            set { _dwMeno = value; }
        }


        /// <summary>
        /// 数据枚举
        /// </summary>
        public enum QueryItem
        {
           dwID , dwName , dwMeno 
        }
    }
}

