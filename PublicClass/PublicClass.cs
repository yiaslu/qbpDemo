using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Reflection;
using System.Web;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Data.OracleClient;
using Newtonsoft.Json;

namespace PublicClass
{
    public enum DbType
    {
        Ass, SQL, Oracle, Null
    }

    public class ConnBox
    {
        public string DataType { get; set; }
        public string ConnStr { get; set; }
        public string UIClassName { get; set; }
        public string ExecType { get; set; }
    }
    public class PublicClass
    {
        private static ConnBox box;
        public static ConnBox BOX
        {
            get { return box; }
        }

        private static IUIHelp _UIHelp;

        protected static IUIHelp UIHelp
        {
            get
            {
                if (_UIHelp == null)
                {
                    Assembly assembly = Assembly.Load(new AssemblyName("UIHelpBase"));
                    object obj = assembly.CreateInstance("UIHelpBase." + UIClassName);
                    _UIHelp = (IUIHelp)obj;
                }
                return _UIHelp;
            }
        }
        private static string _connStr = "";
        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        protected static string connStr
        {
            get
            {
                if (string.IsNullOrEmpty(_connStr))
                {
                    SetConfig();
                }
                return _connStr;
                //return string.Format(ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString, GetServiceString);
            }
        }
        private static string _dataType = null;
        /// <summary>
        /// 数据库类型
        /// </summary>
        protected static string dataType
        {
            get
            {
                if (string.IsNullOrEmpty(_dataType))
                {
                    SetConfig();
                }
                return _dataType;
                //return System.Configuration.ConfigurationManager.ConnectionStrings["DataType"].ConnectionString;
            }
        }
        private static string _UIClassName;
        protected static string UIClassName
        {
            get
            {
                if (string.IsNullOrEmpty(_UIClassName))
                {
                    SetConfig();
                }
                return _UIClassName;
            }
        }
        private static string configFileName = "license.crconfig";

        protected void SetConfig(string fileName)
        {
            configFileName = fileName;
            SetConfig();
        }

        private static void SetConfig()
        {
            try
            {
                StreamReader file = new StreamReader(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + configFileName);
                string boxText = file.ReadToEnd();
                file.Close();
                box = JsonConvert.DeserializeObject<ConnBox>(Decrypt(boxText));
                _connStr = box.ConnStr;
                _dataType = box.DataType;
                _UIClassName = box.UIClassName;
            }
            catch (Exception)
            {
                throw new Exception("未生成项目配置文件！");
            }
        }
        /// 获得数据库类型
        /// <summary>
        /// 获得数据库类型
        /// </summary>
        /// <returns></returns>
        protected static DbType GetDbType()
        {
            if (string.IsNullOrEmpty(dataType))
            {
                throw new Exception("没有找到对应的数据库类型");
            }
            if (dataType == "Ass")
            {
                return DbType.Ass;
            }
            else if (dataType == "Sql")
            {
                return DbType.SQL;
            }
            else if (dataType == "Oracle")
            {
                return DbType.Oracle;
            }
            else
            {
                throw new Exception("没有找到对应的数据库类型");
            }
        }

        ///创建数据库
        /// <summary> 
        /// 创建数据库
        /// </summary>
        /// <param name="type">数据库类型</param>
        /// <param name="conStr">数据库连接字符串</param>
        /// <returns></returns>
        protected static DbConnection CaertDB()
        {
            DbType type = GetDbType();
            DbConnection connection = null;
            if (type == DbType.Ass)
            {
                connection = new OleDbConnection(connStr);
            }
            else if (type == DbType.SQL)
            {
                connection = new SqlConnection(connStr);
            }
            else
            {
                throw new Exception("没有找到配置文件");
            }
            try
            {
                connection.Open();
            }
            catch (Exception exception)
            {
                connection.Dispose();
                connection = null;
                throw exception;
            }
            return connection;
        }

        ///创建数据库
        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="type">数据库类型</param>
        /// <param name="conStr">数据库连接字符串</param>
        /// <returns></returns>
        protected static DbDataAdapter CaertAdapter(DbCommand cmd)
        {
            DbType type = GetDbType();
            DbDataAdapter Adapter = null;
            if (type == DbType.Ass)
            {
                Adapter = new OleDbDataAdapter((OleDbCommand)cmd);
            }
            else if (type == DbType.SQL)
            {
                Adapter = new SqlDataAdapter((SqlCommand)cmd);
            }
            else
            {
                throw new Exception("没有找到配置文件");
            }
            return Adapter;
        }

        ///创建数据库
        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="type">数据库类型</param>
        /// <param name="conStr">数据库连接字符串</param>
        /// <returns></returns>
        protected static DbParameter CreateParameler()
        {
            DbType type = GetDbType();
            DbParameter para = null;
            if (type == DbType.Ass)
            {
                para = new OleDbParameter();
            }
            else if (type == DbType.SQL)
            {
                para = new SqlParameter();
            }
            else if (type == DbType.Oracle)
            {
                para = new OracleParameter();
            }
            else
            {
                throw new Exception("没有找到配置文件");
            }
            return para;
        }
        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="bh"></param>
        /// <param name="p"></param>
        /// <param name="obj"></param>
        protected static void SetKeyValue(string bh, object obj)
        {
            PropertyInfo[] p = obj.GetType().GetProperties();
            foreach (PropertyInfo pinfo in p)
            {
                //object[] list = pinfo.GetCustomAttributes(true).Where(info => info.GetType().Name == "ModelAttribute").ToArray();
                List<object> list = new List<object>();
                foreach (var item in pinfo.GetCustomAttributes(true))
                {
                    if (item.GetType().Name == "ModelAttribute")
                    {
                        list.Add(item);
                    }
                }
                if (list.Count != 0)
                {
                    ModelAttribute matt = list[0] as ModelAttribute;
                    if (matt.ModelContent.IsKey)
                    {
                        if (pinfo.PropertyType.FullName.ToLower().IndexOf("int") != -1)
                            pinfo.SetValue(obj, int.Parse(bh), null);
                        else
                            pinfo.SetValue(obj, bh, null);
                        return;
                    }
                }
            }
        }
        protected static List<KeyBox> GetKeyList(PropertyInfo[] p, object obj)
        {
            List<KeyBox> dpList = new List<KeyBox>();
            foreach (PropertyInfo pinfo in p)
            {
                //object[] list = pinfo.GetCustomAttributes(true).Where(info => info.GetType().Name == "ModelAttribute").ToArray();
                List<object> list = new List<object>();
                
                foreach (var item in pinfo.GetCustomAttributes(true))
                {
                    if (item.GetType().Name == "ModelAttribute")
                    {
                        list.Add(item);
                    }
                }
                if (list.Count != 0)
                {
                    foreach (ModelAttribute matt in list)
                    {
                        if (matt.ModelContent.IsKey)
                        {
                            KeyBox kbox = new KeyBox();
                            kbox.Key = pinfo.Name;
                            kbox.Pinfo = pinfo;
                            kbox.Dbpara = CreateParameler();
                            kbox.Dbpara.ParameterName = "@" + pinfo.Name;
                            kbox.Dbpara.Value = pinfo.GetValue(obj, null) + "";
                            dpList.Add(kbox);
                        }
                    }
                }
            }
            return dpList;
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected static string GetTableName(Type type)
        {
            //object[] list = type.GetCustomAttributes(true).Where(info => info.GetType().Name == "ModelAttribute").ToArray();
            List<object> list = new List<object>();
            foreach (var item in type.GetCustomAttributes(true))
            {
                if (item.GetType().Name == "ModelAttribute")
                {
                    list.Add(item);
                }
            }
            if (list.Count != 0)
            {
                ModelAttribute matt = list[0] as ModelAttribute;

                return matt.ModelContent.TableName;
            }
            return type.Name;
        }


        public static string Decrypt(string pToDecrypt)
        {
            return Decrypt(pToDecrypt, "YIAS8888");
        }

        public static string Encrypt(string pToEncrypt)
        {
            return Encrypt3DES(pToEncrypt, "YIAS8888");
        }
        private static string Encrypt3DES(string pToEncrypt, string sKey)
        {
            if (pToEncrypt == null)
            {
                return null;
            }
            else if (pToEncrypt == "")
            {
                return "";
            }
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }
        /**/
        /// <summary>
        /// 进行DES解密。
        /// </summary>
        /// <param name="pToDecrypt">要解密的以Base64</param>
        /// <param name="sKey">密钥，且必须为8位。</param>
        /// <returns>已解密的字符串。</returns>
        private static string Decrypt(string pToDecrypt, string sKey)
        {
            if (pToDecrypt == null)
            {
                return null;
            }
            else if (pToDecrypt == "")
            {
                return "";
            }
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();

            return System.Text.Encoding.Default.GetString(ms.ToArray());

        }
    }

    ///传值对象
    /// <summary>
    /// 传值对象
    /// </summary>
    public class KeyBox
    {
        string key;

        public string Key
        {
            get { return key; }
            set { key = value; }
        }
        DbParameter dbpara;

        public DbParameter Dbpara
        {
            get { return dbpara; }
            set { dbpara = value; }
        }
        PropertyInfo pinfo;

        public PropertyInfo Pinfo
        {
            get { return pinfo; }
            set { pinfo = value; }
        }
    }
}
