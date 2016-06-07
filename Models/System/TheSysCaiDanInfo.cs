
using System;
using PublicClass;

namespace Models
{
    [Serializable]
    [ModelAttribute("Sys_CaiDan", "")]
    public class TheSysCaiDanInfo : BaseMODEL
    {
        
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


        private string _cdName;
        /// <summary>
        /// 菜单名称
        /// </summary>
        [ModelAttribute(50,0,false,"","菜单名称")]
        public string cdName
        {
            get { return _cdName; }
            set { _cdName = value; }
        }


        private string _cdURL;
        /// <summary>
        /// 菜单地址
        /// </summary>
        [ModelAttribute(100,0,false,"","菜单地址")]
        public string cdURL
        {
            get { return _cdURL; }
            set { _cdURL = value; }
        }


        private string _cdImg;
        /// <summary>
        /// 菜单图片
        /// </summary>
        [ModelAttribute(20,0,false,"","菜单图片")]
        public string cdImg
        {
            get { return _cdImg; }
            set { _cdImg = value; }
        }


        private int? _cdPID;
        /// <summary>
        /// 父级菜单编号
        /// </summary>
        [ModelAttribute(4,0,false,"","父级菜单编号")]
        public int? cdPID
        {
            get { return _cdPID; }
            set { _cdPID = value; }
        }


        /// <summary>
        /// 数据枚举
        /// </summary>
        public enum QueryItem
        {
           cdID , cdName , cdURL , cdImg , cdPID 
        }
    }
}

