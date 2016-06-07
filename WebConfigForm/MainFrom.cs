using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace WebConfigForm
{
    public partial class MainFrom : Form
    {
        public MainFrom()
        {
            InitializeComponent();
            dropDataType_SelectedIndexChanged(null, null);
        }

        public class ConnBox
        {
            public string DataType { get; set; }
            public string ConnStr { get; set; }
            public string UIClassName { get; set; }
            public string ExecType { get; set; }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dropDataType.Text))
            {
                string strText = "";
                ConnBox box = new ConnBox();
                box.DataType = dropDataType.Text;
                box.ConnStr = string.Format("{4}{3}{0}Persist Security Info=True;{1}{2}", (txtDBName.Text != "" ? "Initial Catalog=" + txtDBName.Text + ";" : "")
                    , (txtName.Text != "" ? "User ID=" + txtName.Text + ";" : "")
                    , (txtPWD.Text != "" ? "Password=" + txtPWD.Text + ";" : "")
                    , (txtIP.Text != "" ? "Data Source=" + txtIP.Text + ";" : "")
                    , (dropBb.Text != "" ? "Provider=" + dropBb.Text + ";" : ""));
                if (string.IsNullOrEmpty(comboBox1.Text))
                    MessageBox.Show("请选择UI类的名称");
                box.UIClassName = comboBox1.Text;
                box.ExecType = comExecType.Text;
                strText = JsonConvert.SerializeObject(box);
                StreamWriter writ = File.CreateText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\license.crconfig");
                writ.Write(Encrypt(strText));
                writ.Close();
                MessageBox.Show("文件已生成到桌面");
            }
            else { MessageBox.Show("请选择生成的数据库类型！"); }
        }

        public static string Encrypt(string pToEncrypt)
        {
            return Encrypt(pToEncrypt, "YIAS8888");
        }

        /// <summary>
        /// 进行DES加密。
        /// </summary>
        /// <param name="pToEncrypt">要加密的字符串。</param>
        /// <param name="sKey">密钥，且必须为8位。</param>
        /// <returns>以Base64格式返回的加密字符串。</returns>
        public static string Encrypt(string pToEncrypt, string sKey)
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

        private void dropDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIP.Enabled = true;
            txtDBName.Enabled = true;
            dropBb.Enabled = true;
            switch (dropDataType.Text)
            {
                case "Ass": txtDBName.Text = ""; txtDBName.Enabled = false; break;
                case "Oracle": txtDBName.Text = ""; txtDBName.Enabled = false; dropBb.Text = ""; dropBb.Enabled = false; break;
                case "Sql": dropBb.Text = ""; dropBb.Enabled = false; break;
                default:
                    break;
            }
        }
    }
}
