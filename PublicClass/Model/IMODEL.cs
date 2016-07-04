using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicClass
{
    public interface IMODEL
    {
    }
    public class BaseMODEL : IMODEL
    {
        protected Dictionary<string, object> ValueList;

        protected Byte[] CharSetCount;
        /// <summary>
        /// 为时间戳赋值
        /// </summary>
        /// <param name="TimeStamp"></param>
        public void SetCharSetCount(Byte[] TimeStamp)
        {
            CharSetCount = TimeStamp;
        }
        /// <summary>
        /// 赋原始值
        /// </summary>
        /// <param name="ValueList"></param>
        public void AssignmentModel(Dictionary<string, object> ValueList)
        {
            this.ValueList = ValueList;
        }
        /// <summary>
        /// 更新回原始值
        /// </summary>
        public void RecoveryModel()
        {
            if (this.ValueList != null)
            {
                PropertyInfo[] p = GetType().GetProperties();
                foreach (PropertyInfo propInfo in p)
                {
                    foreach (var item in ValueList)
                    {
                        if (item.Key == propInfo.Name)
                        {
                            propInfo.SetValue(this, ValueList[propInfo.Name], null);
                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 通过当前对象更改原始值
        /// </summary>
        public void SetAssignmentModel()
        {
            if (this.ValueList != null)
            {
                try
                {
                    PropertyInfo[] p = GetType().GetProperties();
                    foreach (PropertyInfo propInfo in p)
                    {
                        if (propInfo.GetValue(this, null) != null)
                        {
                            bool isHave = false;
                            foreach (var item in ValueList)
                            {
                                if (item.Key == propInfo.Name)
                                {
                                    isHave = true;
                                    break;
                                }
                            }
                            if (isHave)
                            {
                                ValueList[propInfo.Name] = propInfo.GetValue(this, null);
                            }
                            else
                            {
                                ValueList.Add(propInfo.Name, propInfo.GetValue(this, null));
                            }
                        }
                    }
                }
                catch (Exception)
                { }
            }
        }
        /// <summary>
        /// 将本对象赋值给其他对象
        /// </summary>
        /// <param name="obj"></param>
        public void CopyMyToModel(IMODEL obj)
        {
            List<string> list = new List<string>();
            list.Add("ItemNo");
            list.Add("KeyID");
            list.Add("OrgKey");
            list.Add("CreateUserID");
            list.Add("CreateTime");
            list.Add("UpdateUserID");
            list.Add("UpdateTime");
            list.Add("DeleteUserID");
            list.Add("DeleteTime");
            list.Add("ConfirmStatus");
            list.Add("Status");
            list.Add("CheckedStatusCode");
            list.Add("isCheckedUserID");
            list.Add("CheckedTime");
            list.Add("isUnChecked");
            list.Add("UnCheckedUserID");
            list.Add("UnCheckedTime");
            CopyMyToModel(obj, list);
        }

        public void CopyMyToModel(IMODEL obj, List<string> notKey)
        {
            PropertyInfo[] p = obj.GetType().GetProperties();
            foreach (PropertyInfo toinfo in p)
            {
                foreach (PropertyInfo pinfo in this.GetType().GetProperties())
                {
                    if (toinfo.Name == pinfo.Name && notKey.IndexOf(pinfo.Name) == -1)
                    {
                        if (pinfo.GetValue(this, null) != null)
                            toinfo.SetValue(obj, pinfo.GetValue(this, null), null);
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 返回时间戳
        /// </summary>
        /// <returns></returns>
        public Byte[] GetTimeStamp()
        {
            return CharSetCount;
        }
    }
    public class ModelContent
    {
        private string tableName;
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        private bool isKey;
        /// <summary>
        /// 是否是主键
        /// </summary>
        public bool IsKey
        {
            get { return isKey; }
            set { isKey = value; }
        }
        private int length;
        /// <summary>
        /// 字段长度
        /// </summary>
        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        private int floatLength;

        /// <summary>
        /// 小数点长度
        /// </summary>
        public int FloatLength
        {
            get { return floatLength; }
            set { floatLength = value; }
        }


        private bool isRequired;
        /// <summary>
        /// 是否必填true为必填
        /// </summary>
        public bool IsRequired
        {
            get { return isRequired; }
            set { isRequired = value; }
        }
        private object defaultValue;
        /// <summary>
        /// 默认值
        /// </summary>
        public object DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }
        private string explain;
        /// <summary>
        /// 说明
        /// </summary>
        public string Explain
        {
            get { return explain; }
            set { explain = value; }
        }




    }
    public class ModelAttribute : Attribute
    {
        private ModelContent modelContent = new ModelContent();

        public ModelContent ModelContent
        {
            get { return modelContent; }
        }
        /// <summary>
        /// Class类型设置表名称
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="tableName">说明</param>
        public ModelAttribute(string tableName, string Explain)
        {
            NewModelAttr(tableName, false, 0, false, "", Explain);
        }
        /// <summary>
        /// 普通字段
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="floatLength">小数点长度</param>
        /// <param name="isRequired">是否必填true为必填</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="Explain">说明</param>
        public ModelAttribute(int length, int floatLength, bool isRequired, object defaultValue, string Explain)
        {
            NewModelAttr("", false, length, isRequired, defaultValue, Explain);
        }
        /// <summary>
        /// 主键字段
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="Explain">说明</param>
        public ModelAttribute(int length, object defaultValue, string Explain)
        {
            NewModelAttr("", true, length, true, defaultValue, Explain);
        }
        private void NewModelAttr(string tableName, bool isKey, int length, bool isRequired, object defaultValue, string explain)
        {
            NewModelAttr(tableName, isKey, length, 0, isRequired, defaultValue, explain);
        }
        private void NewModelAttr(string tableName, bool isKey, int length, int floatLength, bool isRequired, object defaultValue, string Explain)
        {
            modelContent.TableName = tableName;
            modelContent.IsKey = isKey;
            modelContent.Length = length;
            modelContent.IsRequired = isRequired;
            modelContent.DefaultValue = defaultValue;
            modelContent.FloatLength = floatLength;
        }


    }
}
