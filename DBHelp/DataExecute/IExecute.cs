using PublicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBHelp
{
    #region 接口
    public interface IExecute
    {
        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="model"></param>
        void Add(BaseMODEL model);

        /// <summary>
        /// 修改对象
        /// </summary>
        /// <param name="model"></param>
        void Update(BaseMODEL model);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model"></param>
        void Delete(BaseMODEL model);

        /// <summary>
        /// 执行语句
        /// </summary>
        /// <param name="comText"></param>
        /// <param name="whereList"></param>
        void ExecuteSQL(string comText, List<DataParameter> whereList);

        /// <summary>
        /// 提交执行
        /// </summary>
        /// <returns></returns>
        int DBCommit();
    }
    #endregion

    #region 辅助类

    [Serializable]
    public class DataParameter : PublicClass.PublicClass
    {
        public DataParameter()
        {
        }
        public DataParameter(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
        public DataParameter(string key, object value)
        {
            this.key = key;
            this.value = value;
        }
        private string key;

        public string Key
        {
            get
            {
                if (PublicClass.PublicClass.GetDbType() == PublicClass.DbType.Oracle)
                    return key.Replace("@", ":");
                return key;
            }
            set { key = value; }
        }
        private object value;

        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }


    public class ExecuteBox
    {
        public ExecuteBox(string comText, List<DataParameter> whereList)
        {
            this.ExecSql = comText;
            this.WhereList = whereList;
        }
        public string ExecSql { get; set; }
        public List<DataParameter> WhereList { get; set; }
    }
    #endregion

    #region 父类

    public abstract class BaseExecute : PublicClass.PublicClass, IExecute
    {

        protected List<BaseMODEL> addList = new List<BaseMODEL>();

        protected List<BaseMODEL> updateList = new List<BaseMODEL>();

        protected List<BaseMODEL> deleteList = new List<BaseMODEL>();

        protected List<ExecuteBox> exceList = new List<ExecuteBox>();

        protected void Clear()
        {
            addList.Clear();
            updateList.Clear();
            deleteList.Clear();
            exceList.Clear();
        }
        /// <summary>
        /// 添加添加对象
        /// </summary>
        /// <param name="model">工具生成的对象</param>
        public void Add(BaseMODEL model)
        {
            ////自动添加int主键值
            //List<KeyBox> kbox = GetKeyList(model.GetType().GetProperties(), model);
            //if (kbox.Where(key => key.Dbpara.Value == null && key.Pinfo.PropertyType.FullName.ToLower().IndexOf("int") != -1).ToList().Count == 1)
            //{

            //}
            addList.Add(model);
        }
        /// <summary>
        /// 添加修改对象
        /// </summary>
        /// <param name="model">工具生成的对象</param>
        public void Update(BaseMODEL model)
        {
            updateList.Add(model);
        }
        /// <summary>
        /// 添加删除对象
        /// </summary>
        /// <param name="model">工具生成的对象</param>
        public void Delete(BaseMODEL model)
        {
            deleteList.Add(model);
        }

        /// <summary>
        /// 添加执行语句
        /// </summary>
        /// <param name="comText"></param>
        /// <param name="whereList"></param>
        public void ExecuteSQL(string comText, List<DataParameter> whereList)
        {
            exceList.Add(new ExecuteBox(comText, whereList));
        }

        /// <summary>
        /// 提交执行
        /// </summary>
        /// <returns></returns>
        public abstract int DBCommit();
    }
    #endregion
}
