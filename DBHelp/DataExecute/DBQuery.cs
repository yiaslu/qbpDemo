using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.IO;
using PublicClass;

namespace DBHelp
{
    public class DBQuery<T> : BaseQuery<T> where T : BaseMODEL, new()
    {
        #region 私有方法
        /// <summary>
        /// 获得数据表中的数据
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="whereList">防注入参数</param>
        /// <returns></returns>
        private DataTable SelDataTable(string sql)
        {
            DataTable dt = new DataTable();
            using (DbConnection conn = CaertDB())
            {
                DbCommand command = conn.CreateCommand();
                command.CommandText = sql;
                foreach (DataParameter dtp in listDara)
                {
                    DbParameter par = command.CreateParameter();
                    par.ParameterName = dtp.Key;
                    par.Value = dtp.Value;
                    command.Parameters.Add(par);
                }
                try
                {
                    DbDataAdapter da = CaertAdapter(command);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(dt);
                    dt.ExtendedProperties.Add("SQL", sql);
                }
                catch (Exception exp)
                {
                    throw exp;
                }
                finally
                {
                    command.Dispose();
                    command = null;
                    conn.Close();
                }
            }
            WhereStr = "";
            listDara.Clear();
            return dt;
        }

        /// <summary>
        /// 判断 DataReader 里面是否包含指定的列(提供时间戳 避免并发)
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private static bool ReaderExists(DbDataReader dr, string columnName)
        {
            int count = dr.FieldCount;
            for (int i = 0; i < count; i++)
            {
                if (dr.GetName(i).Equals(columnName))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        public override List<T> SelectList(string orderbystr)
        {
            List<T> retList = new List<T>();
            string strSql = "SELECT * FROM " + GetTableName(typeof(T)) + " WHERE 1=1 " + WhereStr + (orderbystr != "" ? " ORDER BY " + (orderbystr.Replace(" order by", "")) : "");
            DataTable dt = SelDataTable(strSql);
            Type type = typeof(T);
            PropertyInfo[] p = type.GetProperties();
            bool isAddSTimeStamp = dt.Columns.Contains("CharSetCount");
            foreach (DataRow reade in dt.Rows)
            {
                Dictionary<string, object> setVa = new Dictionary<string, object>();
                object obj = type.Assembly.CreateInstance(type.FullName);
                foreach (PropertyInfo propInfo in p)
                {
                    if (dt.Columns.Contains(propInfo.Name) && reade[propInfo.Name] + "" != "")
                    {
                        setVa.Add(propInfo.Name, reade[propInfo.Name]);
                        propInfo.SetValue(obj, reade[propInfo.Name], null);
                    }
                }
                if (isAddSTimeStamp)
                    (obj as BaseMODEL).SetCharSetCount((Byte[])reade["CharSetCount"]);
                (obj as BaseMODEL).AssignmentModel(setVa);
                retList.Add((T)obj);
            }
            WhereStr = "";
            listDara.Clear();
            return retList;
        }
      
        public override T GetModel(T obj)
        {
            Type type = typeof(T);
            using (DbConnection conn = CaertDB())
            {
                Dictionary<string, object> setVa = new Dictionary<string, object>();
                DbCommand command = conn.CreateCommand();
                PropertyInfo[] p = type.GetProperties();
                List<KeyBox> kbox = GetKeyList(p, obj);
                string whereStr = "";
                int js = 0;
                foreach (KeyBox box in kbox)
                {
                    whereStr += (js == 0 ? "" : " AND ") + box.Key + "=" + box.Dbpara.ParameterName;
                    command.Parameters.Add(box.Dbpara);
                    js++;
                }
                command.CommandText = "SELECT * FROM  " + PublicClass.PublicClass.GetTableName(typeof(T)) + "  WHERE " + whereStr;
                try
                {
                    DbDataReader reade = command.ExecuteReader();
                    if (reade.Read())
                    {
                        foreach (PropertyInfo propInfo in p)
                        {
                            if (reade[propInfo.Name] + "" != "")
                            {
                                setVa.Add(propInfo.Name, reade[propInfo.Name]);
                                propInfo.SetValue(obj, reade[propInfo.Name], null);
                            }
                        }
                        if (ReaderExists(reade, "CharSetCount"))
                            (obj as BaseMODEL).SetCharSetCount((Byte[])reade["CharSetCount"]);
                        (obj as BaseMODEL).AssignmentModel(setVa);
                    }
                    else
                        obj = (T)type.Assembly.CreateInstance(type.FullName, true);
                }
                catch (Exception exp)
                {
                    throw exp;
                }
                finally
                {
                    command.Dispose();
                    command = null;
                }
            }
            return (T)obj;
        }

    }
}