using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace PublicClass
{
    public class GearClass : PublicClass
    {
        Dictionary<string, string> strValue = new Dictionary<string, string>();
        public GearClass(string json)
        {
            try
            {
                JObject jsonmodel = (JObject)JsonConvert.DeserializeObject(json);
                foreach (var jpropInfo in jsonmodel)
                {
                    strValue.Add(jpropInfo.Key + "", (jpropInfo.Value + "").Replace("[+|+]", "'"));
                }
            }
            catch (Exception)
            {

                throw new Exception("调用程序错误，无法返回请求");
            }
        }
        public GearClass(Dictionary<string, string> dx)
        {
            strValue = dx;
        }
        public string this[string key]
        {
            get
            {
                try
                {
                    if (strValue[key] == null)
                    {

                    }
                }
                catch (Exception)
                {
                    strValue.Add(key, "");
                }
                return strValue[key];
            }
            set
            {
                bool ishave = false;
                foreach (var item in strValue.Keys)
                {
                    if (item == key)
                    {
                        ishave = true;
                        strValue[key] = value;
                        break;
                    }
                }
                if (!ishave)
                {
                    strValue.Add(key, value);
                }
            }
        }

        public void Add(string key, object obj)
        {
            if (obj != null)
                if (obj.GetType().BaseType.Name == "BaseMODEL")
                {
                    strValue.Add(key, JsonConvert.SerializeObject(obj));
                }
                else
                    strValue.Add(key, obj.ToString());
        }
        public GearClass(string TypeName, string Method, string BusinessName)
        {
            this.BusinessName = BusinessName;
            this.TypeName = TypeName;
            this.Method = Method;
            this.Types = "Method";
        }
        public GearClass(string TypeName, string Method, string BusinessName, string ConfigFileName)
        {
            this.BusinessName = BusinessName;
            this.TypeName = TypeName;
            this.Method = Method;
            this.Types = "Method";
            this.ConfigFileName = ConfigFileName;
        }
        private string BusinessName
        {
            get { return this["BusinessName"]; }
            set { this["BusinessName"] = value; }
        }

        private string TypeName
        {
            get { return this["typeName"]; }
            set { this["typeName"] = value; }
        }

        private string ConfigFileName
        {
            get
            {
                if (string.IsNullOrEmpty(this["configFileName"]))
                {
                    this["configFileName"] = "license.crconfig";
                }
                return this["configFileName"];
            }
            set { this["configFileName"] = value; }
        }

        private string Method
        {
            get { return this["Method"]; }
            set { this["Method"] = value; }
        }
        private string Types
        {
            get { return this["types"]; }
            set { this["types"] = value; }
        }
        public object Send()
        {
            Assembly assembly = Assembly.LoadFrom(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + (PublicClass.UIClassName == "FrmUIBase" ? "" : @"\bin") + @"\" + this["BusinessName"] + ".dll");
            Type type = assembly.GetType(this["typeName"]);
            object obj = assembly.CreateInstance(type.FullName, true);
            if (this["types"] == "Method")
            {
                if (!string.IsNullOrEmpty(ConfigFileName))
                    SetConfig(ConfigFileName);
                MethodInfo mt = obj.GetType().GetMethod(this["Method"]);
                int length = mt.GetParameters().Length;
                ParameterInfo[] paramsInfo = mt.GetParameters();
                object[] objs = new object[length];
                for (int i = 0; i < length; i++)
                {

                    Type tType = paramsInfo[i].ParameterType;

                    //如果它是值类型,或者String   

                    if (tType.Equals(typeof(string)) || (!tType.IsInterface && !tType.IsClass))
                    {
                        //改变参数类型   
                        objs[i] = Convert.ChangeType(this[paramsInfo[i].Name], tType);
                    }
                    else if (tType.BaseType.Name == "BaseMODEL" && !string.IsNullOrEmpty(this[paramsInfo[i].Name]))
                    {
                        JObject jsonmodel = (JObject)JsonConvert.DeserializeObject(this[paramsInfo[i].Name]);

                        object info = tType.Assembly.CreateInstance(tType.FullName, true);
                        foreach (PropertyInfo propInfo in tType.GetProperties())
                        {
                            foreach (var jpropInfo in jsonmodel)
                            {
                                if (propInfo.Name == jpropInfo.Key)
                                {
                                    if (!string.IsNullOrEmpty(jpropInfo.Value + ""))
                                        propInfo.SetValue(info, GetMainValue(propInfo, jpropInfo.Value), null);
                                    break;
                                }
                            }
                        }
                        objs[i] = Convert.ChangeType(info, tType);
                    }
                }
                try
                {
                    object ob = mt.Invoke(obj, objs);
                    SetConfig("license.crconfig");
                    return ob;
                }
                catch (TargetInvocationException exp)
                {

                    throw new Exception(exp.InnerException.Message);
                }
            }
            return obj;
        }
        private object GetMainValue(PropertyInfo proper, object value)
        {
            string strRowType = proper.PropertyType.FullName.ToLower();
            object retValue = null;
            try
            {
                if (strRowType.IndexOf("int") != -1)
                {
                    retValue = int.Parse(value + "");
                }
                else if (strRowType.IndexOf("decimal") != -1)
                {
                    retValue = decimal.Parse(value + "");
                }
                else if (strRowType.IndexOf("bool") != -1)
                {
                    retValue = bool.Parse(value + "");
                }
                else if (strRowType.IndexOf("datetime") != -1)
                {
                    retValue = DateTime.Parse(value + "");
                }
                else
                    retValue = value + "";
            }
            catch
            {
            }
            return retValue;
        }
    }
}
