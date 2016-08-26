using DBHelp;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PageBLL
{
    public class DataSet : BLLBase<TheSysTableInfo>
    {
        #region 重写数据操作
        public override void Insert(TheSysTableInfo info)
        {
            try
            {
                query.Where(DBWhereType.AND, TheSysTableInfo.QueryItem.tablecode, DBTermType.Equal, info.tablecode);
                if (query.SelectList("").Count == 1)
                {
                    throw new Exception("该表格已存在请重新录入！");
                }
                exec.ExecuteSQL(CreateTableSQL(info), new List<DataParameter>());
                TheSysTableColumnInfo infoCol = new TheSysTableColumnInfo();
                infoCol.colid = 1;
                infoCol.colcode = "tid";
                infoCol.colname = "主键";
                infoCol.coltype = "int";
                infoCol.isnull = false;
                infoCol.isprimary = true;
                info.creadtime = DateTime.Now;
                infoCol.order = 1;
                infoCol.listorder = 1;
                List<TheSysTableColumnInfo> List = new List<TheSysTableColumnInfo>();
                List.Add(infoCol);
                info.tablecolumns = JsonConvert.SerializeObject(List);
                exec.Add(info);
                exec.DBCommit();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public void Imput(TheSysTableInfo info)
        {
            try
            {
                query.Where(DBWhereType.AND, TheSysTableInfo.QueryItem.tablecode, DBTermType.Equal, info.tablecode);
                if (query.SelectList("").Count == 1)
                {
                    throw new Exception("该表格已存在请重新录入！");
                }
                Sysobjcol obc = new Sysobjcol();
                List<ThesystablecolInfo> colList = obc.GetTableCol(info.tablecode);
                List<TheSysTableColumnInfo> List = new List<TheSysTableColumnInfo>();
                info.tabletype = "Table";
                info.creadtime = DateTime.Now;
                foreach (var item in colList)
                {
                    TheSysTableColumnInfo infoCol = new TheSysTableColumnInfo();
                    infoCol.colid = List.Count + 1;
                    infoCol.colcode = item.colcode;
                    infoCol.colname = item.colname;
                    infoCol.coltype = item.coltype;
                    infoCol.isnull = item.colisnull;
                    infoCol.length = item.collength;
                    infoCol.isprimary = item.isPKey;
                    infoCol.order = List.Count + 1;
                    infoCol.listorder = List.Count + 1;
                    List.Add(infoCol);
                }
                info.tablecolumns = JsonConvert.SerializeObject(List);
                exec.Add(info);
                exec.DBCommit();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
        #endregion

        #region 操作数据列

        public List<TheSysTableColumnInfo> GetListColumn(string tableid, string oname)
        {
            try
            {
                List<TheSysTableColumnInfo> retList = new List<TheSysTableColumnInfo>();
                TheSysTableInfo info = Get(tableid);
                if (!string.IsNullOrEmpty(info.tablecolumns))
                {
                    retList = JsonConvert.DeserializeObject<List<TheSysTableColumnInfo>>(info.tablecolumns).Where(iem => iem.isdelete == false).ToList();
                }
                if (oname == "tb")
                {
                    return retList.OrderBy(item => item.order).ToList();
                }
                return retList.OrderBy(item => item.listorder).ToList();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
        public TheSysTableColumnInfo GetColumn(string tableid, string colid)
        {
            TheSysTableColumnInfo info = new TheSysTableColumnInfo();
            if (!string.IsNullOrEmpty(tableid) && !string.IsNullOrEmpty(colid))
            {
                List<TheSysTableColumnInfo> List = new List<TheSysTableColumnInfo>();
                TheSysTableInfo table = Get(tableid);
                if (!string.IsNullOrEmpty(table.tablecolumns))
                {
                    List = JsonConvert.DeserializeObject<List<TheSysTableColumnInfo>>(table.tablecolumns);
                }
                foreach (var item in List)
                {
                    if (item.colid == int.Parse(colid))
                    {
                        info = item;
                    }
                }
            }
            return info;
        }
        public void AddColumn(TheSysTableColumnInfo info, string tableid)
        {
            try
            {
                TheSysTableInfo table = Get(tableid);
                List<TheSysTableColumnInfo> List = new List<TheSysTableColumnInfo>();
                if (!string.IsNullOrEmpty(table.tablecolumns))
                {
                    List = JsonConvert.DeserializeObject<List<TheSysTableColumnInfo>>(table.tablecolumns);
                }
                int? count = List.Max(col => col.colid);
                info.colid = (count == null ? 0 : count.Value) + 1;
                info.order = List.Max(col => col.order) + 1;
                info.listorder = List.Max(col => col.listorder) + 1;
                List.Add(info);
                table.tablecolumns = JsonConvert.SerializeObject(List);
                exec.ExecuteSQL(GetAddColumnSQL(info, "ADD", table.tablecode), new List<DataParameter>());
                exec.Update(table);
                exec.DBCommit();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
        public void UpdateColumn(TheSysTableColumnInfo info, string tableid)
        {
            try
            {
                TheSysTableInfo table = Get(tableid);
                List<TheSysTableColumnInfo> List = new List<TheSysTableColumnInfo>();
                if (!string.IsNullOrEmpty(table.tablecolumns))
                {
                    List = JsonConvert.DeserializeObject<List<TheSysTableColumnInfo>>(table.tablecolumns);
                }
                foreach (var item in List)
                {
                    if (item.colid == info.colid)
                    {
                        List.Remove(item);
                        break;
                    }
                }
                List.Add(info);
                table.tablecolumns = JsonConvert.SerializeObject(List);
                exec.ExecuteSQL(GetAddColumnSQL(info, "ALTER COLUMN", table.tablecode), new List<DataParameter>());
                exec.Update(table);
                exec.DBCommit();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
        public void DeleteColumn(string ID, string tableid)
        {
            try
            {
                TheSysTableInfo table = Get(tableid);
                List<TheSysTableColumnInfo> List = new List<TheSysTableColumnInfo>();
                if (!string.IsNullOrEmpty(table.tablecolumns))
                {
                    List = JsonConvert.DeserializeObject<List<TheSysTableColumnInfo>>(table.tablecolumns);
                }
                foreach (var item in List)
                {
                    if (item.colid == int.Parse(ID))
                    {
                        item.isdelete = true;
                        break;
                    }
                }
                table.tablecolumns = JsonConvert.SerializeObject(List);
                exec.Update(table);
                exec.DBCommit();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public void UpOrder(string order, string tableid, string oname)
        {
            try
            {
                TheSysTableInfo table = Get(tableid);
                List<TheSysTableColumnInfo> List = new List<TheSysTableColumnInfo>();
                if (!string.IsNullOrEmpty(table.tablecolumns))
                {
                    List = JsonConvert.DeserializeObject<List<TheSysTableColumnInfo>>(table.tablecolumns);
                }
                bool set = true;
                if (oname == "tb")
                {
                    foreach (var item in List)
                    {
                        if (item.order == int.Parse(order))
                        {
                            item.order = (item.order - 1);
                            set = false;
                        }
                        if (item.order == (int.Parse(order) - 1))
                        {
                            if (set)
                                item.order = (item.order + 1);
                        }
                        set = true;
                    }

                }
                else
                {
                    foreach (var item in List)
                    {
                        if (item.listorder == int.Parse(order))
                        {
                            item.listorder = (item.listorder - 1);
                            set = false;
                        }
                        if (item.listorder == (int.Parse(order) - 1))
                        {
                            if (set)
                                item.listorder = (item.listorder + 1);
                        }
                        set = true;
                    }
                }
                table.tablecolumns = JsonConvert.SerializeObject(List);
                exec.Update(table);
                exec.DBCommit();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
        public void DownOrder(string order, string tableid, string oname)
        {
            try
            {
                TheSysTableInfo table = Get(tableid);
                List<TheSysTableColumnInfo> List = new List<TheSysTableColumnInfo>();
                if (!string.IsNullOrEmpty(table.tablecolumns))
                {
                    List = JsonConvert.DeserializeObject<List<TheSysTableColumnInfo>>(table.tablecolumns);
                }
                bool set = true;
                if (oname == "tb")
                {
                    foreach (var item in List)
                    {
                        if (item.order == int.Parse(order))
                        {
                            item.order = (item.order + 1);
                            set = false;
                        }
                        if (item.order == (int.Parse(order) + 1))
                        {
                            if (set)
                                item.order = (item.order - 1);
                        }
                        set = true;
                    }

                }
                else
                {
                    foreach (var item in List)
                    {
                        if (item.listorder == int.Parse(order))
                        {
                            item.listorder = (item.listorder + 1);
                            set = false;
                        }
                        if (item.listorder == (int.Parse(order) + 1))
                        {
                            if (set)
                                item.listorder = (item.listorder - 1);
                        }
                        set = true;
                    }
                }
                table.tablecolumns = JsonConvert.SerializeObject(List);
                exec.Update(table);
                exec.DBCommit();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        #endregion

        #region 操作数据库
        private string CreateTableSQL(TheSysTableInfo table)
        {
            string strTableSql = @"create table {0} 
                                    ( 
                                        tid int identity(1,1) not null,
                                        execjson varchar(1000),
                                        constraint PK_S_{0}_tid primary key (tid)
                                    );";

            return string.Format(strTableSql, table.tablecode);
        }

        private string GetAddColumnSQL(TheSysTableColumnInfo column, string state, string tablecode)
        {
            string strColumnSql = @"ALTER TABLE {0} {1} {2} {3} NULL;";
            strColumnSql = string.Format(strColumnSql, tablecode, state, column.colcode, getDataType(column.coltype, column.length + ""));
            return strColumnSql;
        }

        private static string getDataType(string type, string length)
        {
            string reString = "";
            switch (type)
            {
                case "varchar": reString = string.Format("varchar({0})", length); break;
                case "int": reString = "int"; break;
                case "decimal": reString = "decimal(18,4)"; break;
                case "datetime": reString = "datetime"; break;
                case "smalldatetime": reString = "smalldatetime"; break;
                case "char": reString = string.Format("char({0})", length); break;
                case "nvarchar": reString = string.Format("nvarchar({0})", length); break;
                default: reString = (length == "0" || length == "" ? type : string.Format("{0}({1})", type, length)); break;
            }
            return reString;
        }
        #endregion


        #region 页面操作

        public List<TheSysTableColumnInfo> GetTableColumn(string tableid)
        {
            try
            {
                List<TheSysTableColumnInfo> retList = new List<TheSysTableColumnInfo>();
                TheSysTableInfo info = Get(tableid);
                if (!string.IsNullOrEmpty(info.tablecolumns))
                {
                    retList = JsonConvert.DeserializeObject<List<TheSysTableColumnInfo>>(info.tablecolumns).Where(iem => iem.isdelete == false).ToList();
                }
                return retList.OrderBy(item => item.order).ToList();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public ArrayList GetBindTable(string tableid)
        {
            try
            {
                TheSysTableInfo info = Get(tableid);

                string sqlStr = "SELECT * FROM " + info.tablecode;

                List<DataParameter> listPara = new List<DataParameter>();

                var table = DBQuery.SelDataTable(sqlStr, listPara);

                return DataTableToJsonList(table);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
        public void InsertTable(string tableid, string jsoninfo)
        {
            JObject info = (JObject)JsonConvert.DeserializeObject(jsoninfo);
            try
            {
                List<DataParameter> listPara = new List<DataParameter>();
                TheSysTableInfo table = Get(tableid);

                string col = "", colval = "";
                int j = 0;
                foreach (var item in info)
                {
                    col += (j != 0 ? "," : "") + item.Key;
                    colval += (j != 0 ? "," : "") + "@" + item.Key;
                    listPara.Add(new DataParameter("@" + item.Key, item.Value + ""));
                    j++;
                }
                string sqlStr = "INSERT INTO " + table.tablecode + " (" + col + ") VALUES(" + colval + ")";
                exec.ExecuteSQL(sqlStr, listPara);
                exec.DBCommit();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }

        }
        public void UpdateTable(string tableid, string jsoninfo)
        {
            JObject info = (JObject)JsonConvert.DeserializeObject(jsoninfo);
            try
            {
                if (!string.IsNullOrEmpty(info["tid"] + ""))
                {
                    List<DataParameter> listPara = new List<DataParameter>();
                    TheSysTableInfo table = Get(tableid);

                    string colval = "";
                    int j = 0;
                    foreach (var item in info)
                    {
                        if (item.Key != "tid")
                        {
                            colval += (j != 0 ? "," : "") + item.Key + "=@" + item.Key;
                            listPara.Add(new DataParameter("@" + item.Key, item.Value + ""));
                            j++;
                        }
                    }
                    listPara.Add(new DataParameter("@tid", (info["tid"] + "")));
                    string sqlStr = "UPDATE " + table.tablecode + " SET " + colval + " WHERE tid=@tid";
                    exec.ExecuteSQL(sqlStr, listPara);
                    exec.DBCommit();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public void DeleteTable(string tableid, string tid)
        {
            try
            {
                if (!string.IsNullOrEmpty(tid))
                {
                    List<DataParameter> listPara = new List<DataParameter>();
                    TheSysTableInfo table = Get(tableid);
                    listPara.Add(new DataParameter("@tid", tid));
                    string sqlStr = "DELETE " + table.tablecode + " WHERE tid=@tid";
                    exec.ExecuteSQL(sqlStr, listPara);
                    exec.DBCommit();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        private ArrayList DataTableToJsonList(DataTable dt)
        {
            ArrayList arrayList = new ArrayList();
            foreach (DataRow dataRow in dt.Rows)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();  //实例化一个参数集合
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    dictionary.Add(dataColumn.ColumnName, (dataRow[dataColumn.ColumnName] + ""));
                }
                arrayList.Add(dictionary); //ArrayList集合中添加键值
            }

            return arrayList;  //返回一个json字符串
        }
        #endregion
    }
}
