using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace WinForms
{
    public partial class Login : Form
    {
        List<loginBox> boxList = new List<loginBox>();
        public Login(Main main)
        {
            InitializeComponent();
            try
            {
                this.main = main;
                StreamReader file = new StreamReader("Login.logtext");
                string boxText = file.ReadToEnd();
                file.Close();
                boxList = JsonConvert.DeserializeObject<List<loginBox>>(boxText);
                foreach (loginBox box in boxList)
                {
                    comService.Items.Add(box.Service);
                }
            }
            catch (Exception)
            {
            }
        }
        string DataStrFormat = "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";
        private void combo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comService.Text) && !string.IsNullOrEmpty(comLoginName.Text) && !string.IsNullOrEmpty(comLoginPwd.Text))
            {
                string dataStr = string.Format(DataStrFormat, comService.Text, "master", comLoginName.Text, comLoginPwd.Text);

                using (SqlConnection conn = new SqlConnection(dataStr))
                {
                    try
                    {
                        conn.Open();
                    }
                    catch
                    {
                        lolMessage.Text = "请正确输入登陆数据库信息！";
                        lolMessage.ForeColor = Color.Red;
                        btnLogin.Enabled = false;
                        return;
                    }
                    try
                    {
                        SqlCommand command = conn.CreateCommand();
                        string sql = " SELECT [name] FROM sys.databases";
                        command.CommandText = sql;
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        da.Fill(dt);
                        dt.ExtendedProperties.Add("SQL", sql);
                        comDataBase.Items.Clear();
                        foreach (DataRow row in dt.Rows)
                        {
                            comDataBase.Items.Add(row[0] + "");
                        }
                    }
                    catch
                    {
                        lolMessage.Text = "输入信息不正确无法读取！";
                        lolMessage.ForeColor = Color.Red;
                        btnLogin.Enabled = false;
                        return;
                    }
                }
                lolMessage.Text = "请选择数据库！";
                lolMessage.ForeColor = Color.Black;
            }
        }

        private void comDataBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comDataBase.Text != "")
            {
                lolMessage.Text = "请登陆！";
                lolMessage.ForeColor = Color.Black;
                btnLogin.Enabled = true;
            }
        }
        Main main = null;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            main.Login(string.Format(DataStrFormat, comService.Text, comDataBase.Text, comLoginName.Text, comLoginPwd.Text), this);
            loginBox box = new loginBox();
            box.Service = comService.Text;
            box.LoginName = comLoginName.Text;
            box.LoginPwd = comLoginPwd.Text;
            SaveLoginString(box);
            this.Close();
        }

        private void SaveLoginString(loginBox box)
        {
            StreamReader file = new StreamReader("Login.logtext");
            string boxText = file.ReadToEnd();
            file.Close();
            List<loginBox> listBox = JsonConvert.DeserializeObject<List<loginBox>>(boxText);
            if (listBox == null) { listBox = new List<loginBox>(); }
            if (listBox.Where(b => b.Service == box.Service && b.LoginName == box.LoginName && b.LoginPwd == box.LoginPwd).Count() == 0)
            {
                listBox.Add(box);
                string saveString = JsonConvert.SerializeObject(listBox);
                StreamWriter writ = File.CreateText("Login.logtext");
                writ.WriteLine(saveString);
                writ.Close();
            }
        }

        private void comService_SelectedIndexChanged(object sender, EventArgs e)
        {
            comLoginName.Items.Clear();
            foreach (loginBox box in boxList)
            {
                if (comService.Text == box.Service)
                {
                    comLoginName.Items.Add(box.LoginName);
                }
            }
            if (comLoginName.Items.Count != 0)
            {
                comLoginName.Text = comLoginName.Items[0] + "";
                comLoginName_SelectedIndexChanged(null, null);
            }
        }

        private void comLoginName_SelectedIndexChanged(object sender, EventArgs e)
        {
            comLoginPwd.Items.Clear();
            foreach (loginBox box in boxList)
            {
                if (comLoginName.Text == box.LoginName)
                {
                    comLoginPwd.Items.Add(box.LoginPwd);
                }
            }
            if (comLoginPwd.Items.Count != 0)
            {
                comLoginPwd.Text = comLoginPwd.Items[0] + "";
            }
        }
    }
    [Serializable]
    class loginBox
    {
        private string _Service;

        public string Service
        {
            get { return _Service; }
            set { _Service = value; }
        }
        private string _LoginName;

        public string LoginName
        {
            get { return _LoginName; }
            set { _LoginName = value; }
        }
        private string _LoginPwd;

        public string LoginPwd
        {
            get { return _LoginPwd; }
            set { _LoginPwd = value; }
        }
    }
}
