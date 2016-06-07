
using System;
using PublicClass;

namespace Models
{
    [Serializable]
    [ModelAttribute("Sys_BuMen", "")]
    public class TheSysBuMenInfo : BaseMODEL
    {
        
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
        [ModelAttribute(4,0,true,"","单位编号")]
        public int? dwID
        {
            get { return _dwID; }
            set { _dwID = value; }
        }


        private string _bmName;
        /// <summary>
        /// 部门名称
        /// </summary>
        [ModelAttribute(50,0,false,"","部门名称")]
        public string bmName
        {
            get { return _bmName; }
            set { _bmName = value; }
        }


        private int? _pID;
        /// <summary>
        /// 父级编号
        /// </summary>
        [ModelAttribute(4,0,false,"","父级编号")]
        public int? pID
        {
            get { return _pID; }
            set { _pID = value; }
        }


        private string _bmMeno;
        /// <summary>
        /// 部门描述
        /// </summary>
        [ModelAttribute(500,0,false,"","部门描述")]
        public string bmMeno
        {
            get { return _bmMeno; }
            set { _bmMeno = value; }
        }


        /// <summary>
        /// 数据枚举
        /// </summary>
        public enum QueryItem
        {
           bmID , dwID , bmName , pID , bmMeno 
        }
    }
}

