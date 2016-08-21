
using System;
using PublicClass;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Serializable]
    [Table("Sys_JueSe_CaiDan")]
    [ModelAttribute("Sys_JueSe_CaiDan", "")]
    public class TheSysJueSeCaiDanInfo : BaseMODEL
    {
        
        private int? _JsID;
        /// <summary>
        /// 
        /// </summary>
        [ModelAttribute(4,"","")]
        [Key]
        public int? JsID
        {
            get { return _JsID; }
            set { _JsID = value; }
        }


        private int? _cdID;
        /// <summary>
        /// 
        /// </summary>
        [ModelAttribute(4,"","")]
        [Key]
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
           JsID , cdID 
        }
    }
}

