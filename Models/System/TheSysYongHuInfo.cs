
using System;
using PublicClass;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Serializable]
    [Table("Sys_YongHu")]
    [ModelAttribute("Sys_YongHu", "")]
    public class TheSysYongHuInfo : BaseMODEL
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


        private string _yhName;
        /// <summary>
        /// 用户昵称
        /// </summary>
        [ModelAttribute(50,0,false,"","用户昵称")]
        public string yhName
        {
            get { return _yhName; }
            set { _yhName = value; }
        }


        private string _yhLoginName;
        /// <summary>
        /// 登陆名称
        /// </summary>
        [ModelAttribute(50,0,false,"","登陆名称")]
        public string yhLoginName
        {
            get { return _yhLoginName; }
            set { _yhLoginName = value; }
        }


        private string _yhPassWord;
        /// <summary>
        /// 用户密码
        /// </summary>
        [ModelAttribute(50,0,false,"","用户密码")]
        public string yhPassWord
        {
            get { return _yhPassWord; }
            set { _yhPassWord = value; }
        }


        private string _yhWeiXin;
        /// <summary>
        /// 用户微信
        /// </summary>
        [ModelAttribute(50,0,false,"","用户微信")]
        public string yhWeiXin
        {
            get { return _yhWeiXin; }
            set { _yhWeiXin = value; }
        }


        private string _yhTx;
        /// <summary>
        /// 头像
        /// </summary>
        [ModelAttribute(50,0,false,"","头像")]
        public string yhTx
        {
            get { return _yhTx; }
            set { _yhTx = value; }
        }


        private DateTime? _yhLastLgoinTime;
        /// <summary>
        /// 最后一次登陆时间
        /// </summary>
        [ModelAttribute(8,3,false,"","最后一次登陆时间")]
        public DateTime? yhLastLgoinTime
        {
            get { return _yhLastLgoinTime; }
            set { _yhLastLgoinTime = value; }
        }


        /// <summary>
        /// 数据枚举
        /// </summary>
        public enum QueryItem
        {
           yhID , yhName , yhLoginName , yhPassWord , yhWeiXin , yhTx , yhLastLgoinTime 
        }
    }
}

