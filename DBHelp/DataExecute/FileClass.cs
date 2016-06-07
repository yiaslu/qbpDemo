using PublicClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DBHelp
{
    public class FileClass
    {

        public static FileClass GetFileClassBymodel(BaseMODEL info)
        {
            string dataSaveFile = "/DataFile/JSONDATA";
            string fileName = info.GetType().Name + ".datas";
            return new FileClass(dataSaveFile, fileName);
        }
        public FileClass(string url, string name)
        {
            this.fileUrl = url;
            this.fileName = name;
        }
        string fileUrl = "";
        public string fileName { get; set; }
        /// <summary>
        /// 将内容写入执行文件
        /// </summary>
        /// <param name="swStr"></param>
        public void WriteInFile(string swStr, bool isCover)
        {
            //string yStr = ReadFile();
            string str = System.Environment.CurrentDirectory + fileUrl;
            IsHaveDirectory(str);
            string toString = "";
            if (!isCover)
                toString = ReadFile();
            FileStream fs = File.Open(str + "\\" + fileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            toString = toString + swStr;
            sw.Write(toString);
            sw.Close();
            fs.Close();
        }

        public void ClearFile()
        {
            string str = System.Environment.CurrentDirectory + fileUrl;
            IsHaveDirectory(str);
            FileStream fs = File.Open(str + "\\" + fileName, FileMode.OpenOrCreate);
            fs.Seek(0, SeekOrigin.Begin);
            fs.SetLength(0); ;
            fs.Close();
        }
        /// <summary>
        /// 判断目录是否存在
        /// </summary>
        private void IsHaveDirectory(string urlPath)
        {
            if (!Directory.Exists(urlPath))
            {
                Directory.CreateDirectory(urlPath);
            }
        }
        /// <summary>
        /// 读取文本文件
        /// </summary>
        /// <returns></returns>
        public string ReadFile()
        {
            string str = System.Environment.CurrentDirectory + fileUrl;
            IsHaveDirectory(str);
            FileStream fs = File.Open(str + "\\" + fileName, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);
            string retValue = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            return retValue;
        }
    }
}
