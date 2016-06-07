
using System;
using PublicClass;

namespace Models
{
    [Serializable]
    [ModelAttribute("systablecol", "")]
    public class ThesystablecolInfo : BaseMODEL
    {
        
        private string _colname;
        /// <summary>
        /// 
        /// </summary>
        [ModelAttribute(8016,0,false,"","")]
        public string colname
        {
            get { return _colname; }
            set { _colname = value; }
        }


        private string _colcode;
        /// <summary>
        /// 
        /// </summary>
        [ModelAttribute(256,0,false,"","")]
        public string colcode
        {
            get { return _colcode; }
            set { _colcode = value; }
        }


        private bool? _isPKey;
        /// <summary>
        /// 
        /// </summary>
        [ModelAttribute(5,0,true,"","")]
        public bool? isPKey
        {
            get { return _isPKey; }
            set { _isPKey = value; }
        }


        private string _coltype;
        /// <summary>
        /// 
        /// </summary>
        [ModelAttribute(256,0,false,"","")]
        public string coltype
        {
            get { return _coltype; }
            set { _coltype = value; }
        }


        private int? _collength;
        /// <summary>
        /// 
        /// </summary>
        [ModelAttribute(2,0,true,"","")]
        public int? collength
        {
            get { return _collength; }
            set { _collength = value; }
        }


        private bool? _colisnull;
        /// <summary>
        /// 
        /// </summary>
        [ModelAttribute(5,0,true,"","")]
        public bool? colisnull
        {
            get { return _colisnull; }
            set { _colisnull = value; }
        }


        private string _tablecode;
        /// <summary>
        /// 
        /// </summary>
        [ModelAttribute(256,0,true,"","")]
        public string tablecode
        {
            get { return _tablecode; }
            set { _tablecode = value; }
        }


        /// <summary>
        /// 数据枚举
        /// </summary>
        public enum QueryItem
        {
           colname , colcode , isPKey , coltype , collength , colisnull , tablecode 
        }
    }
}

