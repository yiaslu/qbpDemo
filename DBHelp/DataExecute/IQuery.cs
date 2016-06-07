using PublicClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DBHelp
{
    #region 基础查询枚举
    public enum DBWhereType
    {
        AND, OR
    }
    public enum DBTermType
    {
        /// <summary>
        /// 空
        /// </summary>
        None,
        /// <summary>
        /// ==
        /// </summary>
        Equal,

        /// <summary>
        /// <>
        /// </summary>
        NOEqual,

        /// <summary>
        /// IN 添加多个值需要添加全角，
        /// </summary>
        IN,

        /// <summary>
        /// 模糊查询
        /// </summary>
        LIKE,

        /// <summary>
        /// 模糊查询
        /// </summary>
        NOLIKE,

        /// <summary>
        /// 区间条件<* and >*
        /// </summary>
        Section,

        /// <summary>
        /// 区间等于条件<=* and >=*
        /// </summary>
        Section_Equal,

        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan,

        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterThan_Equal,

        /// <summary>
        /// 小于
        /// </summary>
        LessThan,

        /// <summary>
        /// 小于等于
        /// </summary>
        LessThan_Equal
    }

    public enum DBOrder
    {
        ASC, DESC
    }
    #endregion
    public interface IQuery<T> where T : PublicClass.BaseMODEL, new()
    {
        List<T> SelectList(string orderbystr);

        object SelectScalar<TKey>(Expression<Func<T, TKey>> selectColumn);

        T GetModel(T obj);
        T GetModel(string vID);
    }
    public abstract class BaseQuery<T> : PublicClass.PublicClass, IQuery<T> where T : PublicClass.BaseMODEL, new()
    {
        #region 初始设置
        private static string GetTypeSqlStr(string sql)
        {
            if (GetDbType() == PublicClass.DbType.Oracle)
            {
                return sql.Replace("@", ":");
            }
            return sql;
        }


        protected string WhereStr = "";

        protected List<DataParameter> listDara = new List<DataParameter>();
        public void AddListPara(List<DataParameter> listPara)
        {
            listDara.AddRange(listPara);
        }
        protected string GetOperatorStr(DBTermType terType)
        {
            string strOperator = "";
            switch (terType)
            {
                case DBTermType.Equal: strOperator = "=";
                    break;

                case DBTermType.NOEqual: strOperator = "<>";
                    break;

                case DBTermType.IN: strOperator = " IN ";
                    break;

                case DBTermType.LIKE: strOperator = " LIKE ";
                    break;
                case DBTermType.NOLIKE: strOperator = " NOT LIKE ";
                    break;

                case DBTermType.GreaterThan: strOperator = " > ";
                    break;
                case DBTermType.GreaterThan_Equal: strOperator = " >= ";
                    break;
                case DBTermType.LessThan: strOperator = " < ";
                    break;
                case DBTermType.LessThan_Equal: strOperator = " <= ";
                    break;
                default:
                    break;
            }
            return strOperator;
        }

        #endregion 初始设置

        #region 查询条件

        /// <summary>
        /// 子查询条件
        /// </summary>
        /// <param name="whereType"></param>
        /// <param name="ColumnName"></param>
        /// <param name="termType"></param>
        /// <param name="Sql"></param>
        public void WhereAddISNULL(DBWhereType whereType, object ColumnName)
        {
            string strWhere = "{2} {0} {1} IS NULL";
            WhereStr = string.Format(strWhere, whereType, ColumnName, WhereStr);
        }

        /// <summary>
        /// 子查询条件
        /// </summary>
        /// <param name="whereType"></param>
        /// <param name="ColumnName"></param>
        /// <param name="termType"></param>
        /// <param name="Sql"></param>
        public void WhereAddSql(DBWhereType whereType, object ColumnName, DBTermType termType, string Sql)
        {
            string strWhere = "{4} {0} {1} {2} {3}";
            if (termType == DBTermType.None)
                strWhere = "{4} {0} {3}";
            WhereStr = string.Format(strWhere, whereType, ColumnName, GetOperatorStr(termType), Sql, WhereStr);
        }

        public void Where(DBWhereType whereType, object ColumnName, DBTermType termType, object Value)
        {
            Where(whereType, ColumnName, termType, Value, "");
        }

        public void Where(DBWhereType whereType, object ColumnName, DBTermType termType, object StartValue, object EndValue)
        {
            object item = null;
            if (ColumnName.GetType().Name == "QueryItem")
                item = new object[] { ColumnName };
            else
                item = (object[])ColumnName;
            Where(whereType, (object[])item, termType, StartValue, EndValue);
        }

        public void Where(DBWhereType whereType, object[] ColumnName, DBTermType termType, object StartValue, object EndValue)
        {
            object Value = StartValue;
            if (ColumnName.Length == 1)
            {
                string _ColumnName = ColumnName[0] + "";
                string _KeyColumnName = ColumnName[0] + "" + listDara.Count;
                if ((termType == DBTermType.Section || termType == DBTermType.Section_Equal))
                {
                    string dtj = termType == DBTermType.Section ? ">" : ">=";
                    string xtj = termType == DBTermType.Section ? "<" : "<=";
                    string andStr = "";
                    if (!string.IsNullOrEmpty(StartValue + ""))
                    {
                        andStr += " AND " + _ColumnName + dtj + "@" + _KeyColumnName + "Start";
                        listDara.Add(new DataParameter("@" + _KeyColumnName + "Start", StartValue));
                    }
                    if (!string.IsNullOrEmpty(EndValue + ""))
                    {
                        andStr += " AND " + _ColumnName + xtj + "@" + _KeyColumnName + "End";
                        listDara.Add(new DataParameter("@" + _KeyColumnName + "End", EndValue));
                    }

                    WhereStr += " " + whereType + " (1=1" + andStr + ")";
                }
                else if (Value != null && Value + "" != "")
                {
                    WhereStr += " " + whereType + " " + (termType == DBTermType.NOLIKE ? "ISNULL(" + _ColumnName + ",'')" : _ColumnName) + GetOperatorStr(termType) + "{0}";
                    object ColumnNameValue = "";
                    switch (termType)
                    {
                        case DBTermType.Equal: ColumnNameValue = "@" + _KeyColumnName;
                            listDara.Add(new DataParameter("@" + _KeyColumnName, Value));
                            break;

                        case DBTermType.NOEqual: ColumnNameValue = "@" + _KeyColumnName;
                            listDara.Add(new DataParameter("@" + _KeyColumnName, Value));
                            break;

                        case DBTermType.IN:
                            ColumnNameValue = "(";
                            int js = 0;
                            foreach (string item in Value.ToString().Split('，'))
                            {
                                ColumnNameValue += (js == 0 ? "" : ",") + "@" + _KeyColumnName + js;
                                listDara.Add(new DataParameter("@" + _KeyColumnName + js, item));
                                js++;
                            }
                            ColumnNameValue += ")";
                            break;

                        case DBTermType.LIKE: ColumnNameValue = "@" + _KeyColumnName;
                            listDara.Add(new DataParameter("@" + _KeyColumnName, "%" + Value + "%"));
                            break;
                        case DBTermType.NOLIKE: ColumnNameValue = "@" + _KeyColumnName;
                            listDara.Add(new DataParameter("@" + _KeyColumnName, "%" + Value + "%"));
                            break;

                        default: ColumnNameValue = "@" + _KeyColumnName;
                            listDara.Add(new DataParameter("@" + _KeyColumnName, Value));
                            break;
                    }
                    WhereStr = string.Format(WhereStr, ColumnNameValue);
                }
            }
            else
            {
                if (Value != null && Value + "" != "")
                {
                    WhereStr += " " + whereType + " ( {0} )";
                    string InLogo = "InValue";
                    string ColumnNameValue = "";
                    foreach (object _ColumnName in ColumnName)
                    {
                        switch (termType)
                        {
                            case DBTermType.Equal:
                                ColumnNameValue += (ColumnNameValue == "" ? "" : " or ") + _ColumnName + GetOperatorStr(termType) + "@" + _ColumnName + InLogo;
                                listDara.Add(new DataParameter("@" + _ColumnName + InLogo, Value));
                                break;

                            case DBTermType.NOEqual:
                                ColumnNameValue += (ColumnNameValue == "" ? "" : " or ") + _ColumnName + GetOperatorStr(termType) + "@" + _ColumnName + InLogo;
                                listDara.Add(new DataParameter("@" + _ColumnName + InLogo, Value));
                                break;

                            case DBTermType.IN:
                                ColumnNameValue = (ColumnNameValue == "" ? "" : " or ") + _ColumnName + GetOperatorStr(termType) + "(";
                                int js = 0;
                                foreach (string item in Value.ToString().Split('，'))
                                {
                                    listDara.Add(new DataParameter("@" + _ColumnName + js, item));
                                    js++;
                                }
                                ColumnNameValue += ")";
                                break;

                            case DBTermType.LIKE:
                                ColumnNameValue += (ColumnNameValue == "" ? "" : " or ") + _ColumnName + GetOperatorStr(termType) + "@" + _ColumnName + InLogo;
                                listDara.Add(new DataParameter("@" + _ColumnName + InLogo, "%" + Value + "%"));
                                break;
                            case DBTermType.NOLIKE:
                                ColumnNameValue += (ColumnNameValue == "" ? "" : " or ") + "ISNULL(" + _ColumnName + ",'')" + GetOperatorStr(termType) + "@" + _ColumnName + InLogo;
                                listDara.Add(new DataParameter("@" + _ColumnName + InLogo, "%" + Value + "%"));
                                break;

                            default:
                                ColumnNameValue += (ColumnNameValue == "" ? "" : " or ") + _ColumnName + GetOperatorStr(termType) + "@" + _ColumnName + InLogo;
                                listDara.Add(new DataParameter("@" + _ColumnName + InLogo, Value));
                                break;
                        }
                    }

                    WhereStr = string.Format(WhereStr, ColumnNameValue);
                }
            }
        }

        #endregion

        public abstract List<T> SelectList(string orderbystr);

        public abstract object SelectScalar<TKey>(Expression<Func<T, TKey>> selectColumn);


        public abstract T GetModel(T obj);


        public T GetModel(string vID)
        {
            Type type = typeof(T);
            object obj = type.Assembly.CreateInstance(type.FullName, true);
            if (!string.IsNullOrEmpty(vID))
                SetKeyValue(vID, obj);
            else
                return (T)obj;
            return GetModel((obj as T));
        }
    }
}
