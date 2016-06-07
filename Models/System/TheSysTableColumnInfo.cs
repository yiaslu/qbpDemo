
using System;
using PublicClass;

namespace Models
{
    [Serializable]
    [ModelAttribute("Sys_Table_Column", "")]
    public class TheSysTableColumnInfo : BaseMODEL
    {

        private int? _colid;
        /// <summary>
        /// 列编号
        /// </summary>
        [ModelAttribute(4, "", "列编号")]
        public int? colid
        {
            get { return _colid; }
            set { _colid = value; }
        }


        private string _colname;
        /// <summary>
        /// 列名称
        /// </summary>
        [ModelAttribute(50, 0, false, "", "列名称")]
        public string colname
        {
            get { return _colname; }
            set { _colname = value; }
        }


        private string _colcode;
        /// <summary>
        /// 数据列名称
        /// </summary>
        [ModelAttribute(50, 0, false, "", "数据列名称")]
        public string colcode
        {
            get { return _colcode; }
            set { _colcode = value; }
        }


        private string _coltype;
        /// <summary>
        /// 列类型
        /// </summary>
        [ModelAttribute(50, 0, false, "", "列类型")]
        public string coltype
        {
            get { return _coltype; }
            set { _coltype = value; }
        }


        private int? _length;
        /// <summary>
        /// 长度
        /// </summary>
        [ModelAttribute(4, 0, false, "", "长度")]
        public int? length
        {
            get { return _length; }
            set { _length = value; }
        }


        private bool? _isnull;
        /// <summary>
        /// 是否为空
        /// </summary>
        [ModelAttribute(1, 0, false, "", "是否为空")]
        public bool? isnull
        {
            get { return _isnull; }
            set { _isnull = value; }
        }


        private bool? _isprimary;
        /// <summary>
        /// 是否是主键
        /// </summary>
        [ModelAttribute(1, 0, false, "", "是否是主键")]
        public bool? isprimary
        {
            get { return _isprimary; }
            set { _isprimary = value; }
        }

        private string _coldefault;

        /// <summary>
        /// 默认值
        /// </summary>
        [ModelAttribute(1000, 0, false, "", "默认值")]
        public string coldefault
        {
            get { return _coldefault; }
            set { _coldefault = value; }
        }

        private int? _order;

        public int? order
        {
            get { return _order; }
            set { _order = value; }
        }
        private bool? _show;
        public bool? show
        {
            get { return _show; }
            set { _show = value; }
        }

        private int? _listorder;

        public int? listorder
        {
            get { return _listorder; }
            set { _listorder = value; }
        }

        private bool? _listshow;

        public bool? listshow
        {
            get { return _listshow; }
            set { _listshow = value; }
        }

        private string _control;

        public string control
        {
            get { return _control; }
            set { _control = value; }
        }

        public bool isdelete
        {
            get;
            set;
        }

        /// <summary>
        /// 数据枚举
        /// </summary>
        public enum QueryItem
        {
            colid, colname, colcode, coltype, length, isnull, isprimary
        }
    }
}

