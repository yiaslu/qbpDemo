using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace WinForms
{
    public partial class Main : Form
    {
        string DataSql = "";
        Form login = null;
        public Main()
        {
            InitializeComponent();
        }
        public void Login(string dataSql, Form loginFrom)
        {
            this.DataSql = dataSql;
            login = loginFrom;
            Login();
            txtFileName.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Models";
        }
        private void Login()
        {
            if (string.IsNullOrEmpty(this.DataSql))
            {
                WinForms.Login login = new Login(this);
                login.ShowDialog();
                return;
            }
            using (SqlConnection conn = new SqlConnection(DataSql))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = conn.CreateCommand();
                    string sql = "select [name] from sysobjects where xtype in ('U','V') order by [name]";
                    command.CommandText = sql;
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(dt);
                    dt.ExtendedProperties.Add("SQL", sql);
                    chkTableList.Items.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        chkTableList.Items.Add(row[0] + "");
                    }
                }
                catch
                {
                    MessageBox.Show("数据库异常请重新登陆！");
                    login.Visible = true;
                    return;
                }
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (login != null)
                if (!login.Visible)
                    login.Close();
        }

        //        private void chkTableList_ItemCheck(object sender, ItemCheckEventArgs e)
        //        {
        //            using (SqlConnection conn = new SqlConnection(DataSql))
        //            {
        //                try
        //                {
        //                    conn.Open();
        //                    SqlCommand command = conn.CreateCommand();
        //                    string sql = @"SELECT 
        // a.name AS 列名, 
        //       CASE WHEN EXISTS
        //           (SELECT 1
        //         FROM dbo.sysindexes si INNER JOIN
        //               dbo.sysindexkeys sik ON si.id = sik.id AND si.indid = sik.indid INNER JOIN
        //               dbo.syscolumns sc ON sc.id = sik.id AND sc.colid = sik.colid INNER JOIN
        //               dbo.sysobjects so ON so.name = si.name AND so.xtype = 'PK'
        //         WHERE sc.id = a.id AND sc.colid = a.colid) THEN '√' ELSE '' END AS 主键, 
        //       b.name AS 类型, a.length AS 长度, ISNULL(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0) AS 小数位数, 
        //       CASE WHEN a.isnullable = 1 THEN '√' ELSE '' END AS 允许空, ISNULL(e.text, '') 
        //       AS 默认值,c.value as 说明
        //FROM dbo.syscolumns a LEFT OUTER JOIN
        //       dbo.systypes b ON a.xtype = b.xusertype INNER JOIN
        //       dbo.sysobjects d ON a.id = d.id AND d.xtype in ('U','V') AND 
        //       d.status >= 0 LEFT OUTER JOIN
        //       dbo.syscomments e ON a.cdefault = e.id  left join
        //       sys.extended_properties as c on c.major_id=d.id and c.minor_id=a.colid 
        //where d.name='" + chkTableList.Items[e.Index] + "' ORDER BY d.name, a.colorder ";
        //                    command.CommandText = sql;
        //                    DataTable dt = new DataTable();
        //                    SqlDataAdapter da = new SqlDataAdapter(command);
        //                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
        //                    da.Fill(dt);
        //                    dt.ExtendedProperties.Add("SQL", sql);
        //                    dgidTableMessage.DataSource = dt;
        //                }
        //                catch
        //                {
        //                    MessageBox.Show("数据库异常请重新登陆！");
        //                    login.Visible = true;
        //                    return;
        //                }
        //            }
        //        }

        private void btnSet_Click(object sender, EventArgs e)
        {

            //if (!string.IsNullOrEmpty(txtFileName.Text) && !string.IsNullOrEmpty(txtNamespace.Text))
            //{
            if (chkTableList.CheckedItems.Count == 0)
            {
                MessageBox.Show("请选择要生成的表");
                return;
            }
            List<string> tableNameList = new List<string>();
            string strWhere = "'default'";
            foreach (string item in chkTableList.CheckedItems)
            {
                strWhere += ",'" + item + "'";
                tableNameList.Add(item);
            }
            using (SqlConnection conn = new SqlConnection(DataSql))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = conn.CreateCommand();
                    string sql = @"SELECT 
               d.name AS 表名,
 a.name AS 列名, 
       CASE WHEN EXISTS
           (SELECT 1
         FROM dbo.sysindexes si INNER JOIN
               dbo.sysindexkeys sik ON si.id = sik.id AND si.indid = sik.indid INNER JOIN
               dbo.syscolumns sc ON sc.id = sik.id AND sc.colid = sik.colid INNER JOIN
               dbo.sysobjects so ON so.name = si.name AND so.xtype = 'PK'
         WHERE sc.id = a.id AND sc.colid = a.colid) THEN '√' ELSE '' END AS 主键, 
       b.name AS 类型, a.length AS 长度, ISNULL(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0) AS 小数位数, 
       CASE WHEN a.isnullable = 1 THEN '√' ELSE '' END AS 允许空, ISNULL(e.text, '') 
       AS 默认值,c.value as 说明
FROM dbo.syscolumns a LEFT OUTER JOIN
       dbo.systypes b ON a.xtype = b.xusertype INNER JOIN
       dbo.sysobjects d ON a.id = d.id AND d.xtype in ('U','V') AND 
       d.status >= 0 LEFT OUTER JOIN
       dbo.syscomments e ON a.cdefault = e.id  left join
       sys.extended_properties as c on c.major_id=d.id and c.minor_id=a.colid 
where d.name in (" + strWhere + ") ORDER BY d.name, a.colorder ";
                    command.CommandText = sql;
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(dt);
                    dt.ExtendedProperties.Add("SQL", sql);
                    List<ModelBox> listBox = new List<ModelBox>();
                    foreach (string tableName in tableNameList)
                    {
                        string strSetWord = ModelText.Replace("{namespace}", this.txtNamespace.Text).Replace("{tableName}", "\"" + tableName + "\"").Replace("{tableMs}", "\"\"").Replace("{0}", GetClassName(tableName));
                        string strMain = "";
                        string itemName = "";
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["列名"] + "" != "CharSetCount")
                                if ((row["表名"] + "") == tableName)
                                {

                                    strMain += GetMain(row);
                                    itemName += (itemName == "" ? "" : " , ") + row["列名"];
                                }
                        }
                        strSetWord = strSetWord.Replace("{Main}", strMain).Replace("{ItemName}", itemName);

                        listBox.Add(new ModelBox(tableName, strSetWord));
                    }
                    string filePath = txtFileName.Text;
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    foreach (ModelBox box in listBox)
                    {
                        StreamWriter writ = File.CreateText(filePath + "\\" + GetClassName(box.TableName) + ".cs");
                        writ.WriteLine(box.ModelInnerText);
                        writ.Close();
                    }
                    MessageBox.Show("生成成功！");

                }
                catch
                {
                    MessageBox.Show("数据库异常请重新登陆！");
                    login.Visible = true;
                    return;
                }
            }
            //}
            //else
            //{
            //    MessageBox.Show("请选择生成路径或输入命名空间！");
            //}
        }
        private string GetClassName(string tableName)
        {
            return "The" + tableName.Replace("_", "") + "Info";
        }
        private string GetMain(DataRow row)
        {
            string strRowType = row["类型"] + "";
            if (strRowType == "int")
            {
                return GetMainStr("int?", row);
            }
            else if (strRowType == "decimal" || strRowType == "money" || strRowType == "numeric")
            {
                return GetMainStr("decimal?", row);
            }
            else if (strRowType == "uniqueidentifier")
            {
                return GetMainStr("Guid?", row);
            }
            else if (strRowType == "bit")
            {
                return GetMainStr("bool?", row);
            }
            else if (strRowType == "image")
            {
                return GetMainStr("Byte[]", row);
            }
            else if (strRowType == "date" || strRowType == "datetime" || strRowType == "smalldatetime")
            {
                return GetMainStr("DateTime?", row);
            }
            return GetMainStr("string", row);
        }
        private string GetMainStr(string type, DataRow row)
        {
            string strProperty = "";
            if ((row["主键"] + "") == "√")
            {
                strProperty = row["长度"] + ",\"" + (row["默认值"] + "").Replace("('", "").Replace("')", "") + "\",\"" + row["说明"] + "\"";
            }
            else
            {
                strProperty = row["长度"] + "," + row["小数位数"] + "," + (row["允许空"] + "" == "√" ? "false" : "true") + ",\"" + (row["默认值"] + "").Replace("('", "").Replace("')", "") + "\",\"" + row["说明"] + "\"";
            }

            return (ModelPropertyText.Replace("{type}", type).Replace("{pname}", row["列名"] + "").Replace("{setProperty}", strProperty).Replace("{ms}", row["说明"] + ""));
        }
        private string ModelText = @"
using System;
using PublicClass;

namespace {namespace}
{
    [Serializable]
    [ModelAttribute({tableName}, {tableMs})]
    public class {0} : BaseMODEL
    {
        {Main}
        /// <summary>
        /// 数据枚举
        /// </summary>
        public enum QueryItem
        {
           {ItemName} 
        }
    }
}
";
        private string ModelPropertyText = @"
        private {type} _{pname};
        /// <summary>
        /// {ms}
        /// </summary>
        [ModelAttribute({setProperty})]
        public {type} {pname}
        {
            get { return _{pname}; }
            set { _{pname} = value; }
        }

";

        private void chkTableList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkTableList.SelectedIndex == -1)
                return;
            using (SqlConnection conn = new SqlConnection(DataSql))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = conn.CreateCommand();
                    string sql = @"SELECT c.value as 说明,
 a.name AS 列名, 
       CASE WHEN EXISTS
           (SELECT 1
         FROM dbo.sysindexes si INNER JOIN
               dbo.sysindexkeys sik ON si.id = sik.id AND si.indid = sik.indid INNER JOIN
               dbo.syscolumns sc ON sc.id = sik.id AND sc.colid = sik.colid INNER JOIN
               dbo.sysobjects so ON so.name = si.name AND so.xtype = 'PK'
         WHERE sc.id = a.id AND sc.colid = a.colid) THEN '√' ELSE '' END AS 主键, 
       b.name AS 类型, a.length AS 长度, ISNULL(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0) AS 小数位数, 
       CASE WHEN a.isnullable = 1 THEN '√' ELSE '' END AS 允许空, ISNULL(e.text, '') 
       AS 默认值
FROM dbo.syscolumns a LEFT OUTER JOIN
       dbo.systypes b ON a.xtype = b.xusertype INNER JOIN
       dbo.sysobjects d ON a.id = d.id AND d.xtype in ('U','V') AND 
       d.status >= 0 LEFT OUTER JOIN
       dbo.syscomments e ON a.cdefault = e.id  left join
       sys.extended_properties as c on c.major_id=d.id and c.minor_id=a.colid 
where d.name='" + chkTableList.Items[chkTableList.SelectedIndex] + "' ORDER BY d.name, a.colorder ";
                    command.CommandText = sql;
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(dt);
                    dt.ExtendedProperties.Add("SQL", sql);
                    dgidTableMessage.DataSource = dt;
                    string vID = "";
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["主键"] + "" == "√")
                        {
                            vID = row["列名"] + "";
                            break;
                        }
                    }
                    ShowTableCode(chkTableList.Items[chkTableList.SelectedIndex] + "", vID);
                }
                catch
                {
                    MessageBox.Show("数据库异常请重新登陆！");
                    login.Visible = true;
                    return;
                }
            }
        }
        private string CodeModel = @"
    /// <summary>
    /// 业务类
    /// </summary>
    [Serializable]
    public class [className]Business : UIHelpBase.UIBase
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<[modelName]> GetList()
        {
            List<[modelName]> list = new List<[modelName]>();
            try
            {
                DBHelp.DBQuery<[modelName]> dbq = new DBHelp.DBQuery<[modelName]>();
                list = dbq.SelectList("""");
            }
            catch (Exception exp)
            {
                ShowError(exp);
            }
            return list;
        }

        /// <summary>
        /// 获取方法
        /// </summary>
        /// <param name=""vID""></param>
        public [modelName] Get(string vID)
        {
            [modelName] info = new [modelName]();    
            if (!string.IsNullOrEmpty(vID))
            {
                try
                {
                    DBHelp.DBQuery<[modelName]> dbq = new DBHelp.DBQuery<[modelName]>();
                    info = dbq.GetModel(vID);
                }
                catch (Exception exp)
                {
                    ShowError(exp);
                }
            }
            return info;
        }
        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name=""info""></param>
        public void Save([modelName] info)
        {
            try
            {
                DBHelp.DBExecute exec = new DBHelp.DBExecute();

                if (string.IsNullOrEmpty(info.[vID]))
                {
                    info.[vID] = new PublicClass.ModelKey().GetObjKey(info);
                    exec.Add(info);
                }
                else
                {
                    exec.Update(info);
                }
                exec.DBCommit();
            }
            catch (Exception exp)
            {
                ShowError(exp);
            }
        }

        /// <summary>
        /// 添加方法
        /// </summary>
        /// <param name=""info""></param>
        public void Add([modelName] info)
        {
            try
            {
                DBHelp.DBExecute exec = new DBHelp.DBExecute();
                info.[vID] = new PublicClass.ModelKey().GetObjKey(info);
                exec.Add(info);
                exec.DBCommit();
            }
            catch (Exception exp)
            {
                ShowError(exp);
            }
        }
        /// <summary>
        /// 修改方法
        /// </summary>
        /// <param name=""info""></param>
        public void Update([modelName] info)
        {
            try
            {
                DBHelp.DBExecute exec = new DBHelp.DBExecute();
                exec.Update(info);
                exec.DBCommit();
            }
            catch (Exception exp)
            {
                ShowError(exp);
            }
        }
        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name=""info""></param>
        public void Delete(string vID)
        {
            try
            {
                [modelName] info=new [modelName]();
                info.[vID]=vID;
                DBHelp.DBExecute exec = new DBHelp.DBExecute();
                exec.Delete(info);
                exec.DBCommit();
            }
            catch (Exception exp)
            {
                ShowError(exp);
            }
        }
    }
";
        private void ShowTableCode(string tableName, string vID)
        {
            string className = tableName.Replace("_", "");
            string modelName = "The" + tableName.Replace("_", "") + "Info";
            textBox1.Text = CodeModel.Replace("[modelName]", modelName).Replace("[className]", className).Replace("[vID]", vID);
        }
        private void btnSuaxin_Click(object sender, EventArgs e)
        {
            Login();
        }
    }
    class ModelBox
    {
        public ModelBox(string tableName, string modelInnerText)
        {
            this.tableName = tableName;
            this.modelInnerText = modelInnerText;
        }
        private string tableName;

        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        private string modelInnerText;

        public string ModelInnerText
        {
            get { return modelInnerText; }
            set { modelInnerText = value; }
        }
    }
}
