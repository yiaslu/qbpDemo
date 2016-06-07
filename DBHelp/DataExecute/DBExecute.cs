using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Reflection;
using System.Data;
using PublicClass;

namespace DBHelp
{

    public class DBExecute : BaseExecute
    {

        #region 初始化操作
        //实例化后的事务
        private DbTransaction trans = null;
        //数据库连接对象
        private DbConnection myCon = null;
        //数据库操作对象
        private DbCommand myCom = null;

        ///数据库操作对象
        /// <summary>
        /// 数据库操作对象
        /// </summary>
        private DbCommand dbComm
        {
            get
            {
                if (myCom == null)
                {
                    myCom = dbcon.CreateCommand();
                    if (trans == null)
                    {
                        throw new Exception("未开启事务！");
                    }
                    myCom.Transaction = trans;
                    return myCom;
                }
                return myCom;
            }
        }


        ///数据库链接对象
        /// <summary>
        /// 数据库链接对象
        /// </summary>
        private DbConnection dbcon
        {
            get
            {
                if (myCon == null)
                {
                    myCon = CaertDB();
                    return myCon;
                }
                else
                    return myCon;
            }
        }


        #endregion

        #region 操作方法
        int AddModel(IMODEL obj)
        {
            int executeNum = 0;
            if (obj != null)
            {
                List<DataParameter> listPara = new List<DataParameter>();
                Type type = obj.GetType();
                string info = "";
                string infoValue = "";
                PropertyInfo[] p = type.GetProperties();
                int i = 0;
                try
                {
                    foreach (PropertyInfo propInfo in p)
                    {
                        if (propInfo.GetValue(obj, null) != null)
                        {
                            if (i > 0)
                            {
                                info += ",[" + propInfo.Name + "]";
                                infoValue += ",@" + propInfo.Name + "" + "";
                            }
                            else
                            {
                                info += "[" + propInfo.Name + "]";
                                infoValue += "@" + propInfo.Name + "" + "";
                            }
                            string paravalue = propInfo.GetValue(obj, null) + "";
                            if (propInfo.PropertyType.FullName.ToLower().IndexOf("datetime") != -1)
                                paravalue = DateTime.Parse(paravalue).ToString("yyyy-MM-dd");
                            listPara.Add(new DataParameter("@" + propInfo.Name, paravalue));
                            i++;
                        }
                    }
                }
                catch (Exception exp)
                {
                    throw exp;
                }
                executeNum = ExecuteNonQuery("INSERT INTO  " + GetTableName(obj.GetType()) + " (" + info + ") VALUES(" + infoValue + ")", listPara);

            }
            return executeNum;
        }
        int UpdateModel(IMODEL obj)
        {
            int executeNum = 0;
            if (obj != null)
            {
                Type type = obj.GetType();
                string info = "";
                PropertyInfo[] p = type.GetProperties();
                List<DataParameter> listPara = new List<DataParameter>();
                List<KeyBox> kbox = GetKeyList(p, obj);
                foreach (KeyBox item in kbox)
                {
                    if (string.IsNullOrEmpty(item.Dbpara.Value + ""))
                    {
                        throw new Exception("未将主键赋值");
                    }
                }
                int i = 0;
                try
                {
                    foreach (PropertyInfo propInfo in p)
                    {
                        //object[] list = propInfo.GetCustomAttributes(true).Where(infos => infos.GetType().Name == "ModelAttribute").ToArray();
                        List<object> list = new List<object>();
                        foreach (var item in propInfo.GetCustomAttributes(true))
                        {
                            if (item.GetType().Name == "ModelAttribute")
                            {
                                list.Add(item);
                            }
                        }
                        if (list.Count != 0)
                        {
                            ModelAttribute matt = list[0] as ModelAttribute;
                            if (propInfo.GetValue(obj, null) != null && !matt.ModelContent.IsKey)
                            {
                                info += (i != 0 ? "," : "") + "[" + propInfo.Name + "]=@" + propInfo.Name;
                                DbParameter par = dbComm.CreateParameter();
                                par.ParameterName = "@" + propInfo.Name;
                                string paravalue = propInfo.GetValue(obj, null) + "";
                                if (propInfo.PropertyType.FullName.ToLower().IndexOf("datetime") != -1)
                                    paravalue = DateTime.Parse(paravalue).ToString("yyyy-MM-dd");
                                listPara.Add(new DataParameter("@" + propInfo.Name, paravalue));
                                i++;
                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    throw exp;
                }
                string UpdateStr = "UPDATE " + GetTableName(obj.GetType()) + " SET " + info;

                if (kbox.Count != 0)
                {
                    int js = 0;
                    foreach (KeyBox box in kbox)
                    {
                        UpdateStr += (js == 0 ? " WHERE " : " AND ") + box.Key + "=@" + box.Key;
                        listPara.Add(new DataParameter(box.Dbpara.ParameterName, box.Dbpara.Value));
                        js++;
                    }
                    executeNum = ExecuteNonQuery(UpdateStr, listPara);
                }
            }
            return executeNum;
        }
        int DeleteModel(IMODEL obj)
        {
            int executeNum = 0;
            if (obj != null)
            {
                Type type = obj.GetType();
                PropertyInfo[] p = type.GetProperties();
                List<KeyBox> kbox = GetKeyList(p, obj);
                List<DataParameter> listPara = new List<DataParameter>();
                string whereStr = "";
                try
                {
                    int js = 0;
                    foreach (KeyBox box in kbox)
                    {
                        whereStr += (js == 0 ? "" : " AND ") + box.Key + "=" + box.Dbpara.ParameterName;
                        listPara.Add(new DataParameter(box.Dbpara.ParameterName, box.Dbpara.Value));
                        js++;
                    }
                }
                catch (Exception exp)
                {
                    throw exp;
                }
                executeNum = ExecuteNonQuery("DELETE FROM  " + GetTableName(obj.GetType()) + " WHERE " + whereStr, listPara);
            }
            return executeNum;
        }
        /// <summary>
        /// 操作数据库
        /// </summary>
        /// <param name="comText"></param>
        /// <param name="whereList"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string comText, List<DataParameter> whereList)
        {
            dbComm.Parameters.Clear();
            dbComm.CommandText = comText;
            foreach (DataParameter dtp in whereList)
            {
                DbParameter par = dbComm.CreateParameter();
                par.ParameterName = dtp.Key;
                par.Value = dtp.Value;
                dbComm.Parameters.Add(par);
            }
            return dbComm.ExecuteNonQuery();
        }

        #endregion

        #region 执行操作

        #region 内部变量及事务操作
        //打开数据库
        void Open()
        {
            if (dbcon.State != ConnectionState.Open)
            {
                dbcon.Open();
            }
        }
        //开启事务
        void BeginTransaction()
        {
            trans = dbcon.BeginTransaction();
        }
        //提交事务
        void Commit()
        {
            trans.Commit();
        }
        //事务回滚
        void Rollback()
        {
            trans.Rollback();
        }
        //关闭数据库连接对象
        void Close()
        {
            if (trans != null)
            {
                trans.Dispose();
                trans = null;
            }
            if (myCom != null)
            {
                myCom.Dispose();
                myCom = null;
            }
            try
            {
                dbcon.Close();
            }
            catch (Exception)
            {
            }
            this.myCon.Dispose();
            this.myCon = null;
        }

        #endregion

        /// <summary>
        /// 执行以上操作
        /// </summary>
        /// <returns>相应行数</returns>
        public override int DBCommit()
        {
            int retNum = 0;
            Open();
            try
            {
                BeginTransaction();
                foreach (ExecuteBox box in exceList)
                {
                    retNum += ExecuteNonQuery(box.ExecSql, box.WhereList);
                }
                foreach (IMODEL model in addList)
                {
                    retNum += AddModel(model);
                }
                foreach (IMODEL model in updateList)
                {
                    retNum += UpdateModel(model);
                }
                foreach (IMODEL model in deleteList)
                {
                    retNum += DeleteModel(model);
                }
                Commit();
                Clear();
            }
            catch (Exception exp)
            {
                Rollback();
                throw exp;
            }
            finally
            {
                Close();
            }
            return retNum;
        }


        #endregion
    }



}
