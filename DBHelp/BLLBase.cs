using Models;
using PublicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace DBHelp
{
    public class BLLBase<T> : PublicClass.PublicClass where T : PublicClass.BaseMODEL, new()
    {
        protected BaseQuery<T> query = ConfigClass<T>.GetQuery();
        protected IExecute exec = ConfigClass<T>.GetExecute();

        public SysUserInfo UserInfo
        {
            get
            {
                if (HttpContext.Current.Session["UserInfo"] == null)
                {
                    throw new Exception("UserLoginTimeOut");
                }
                return HttpContext.Current.Session["UserInfo"] as SysUserInfo;
            }
        }

        public void SetUserInfo(SysUserInfo info)
        {
            HttpContext.Current.Session["UserInfo"] = info;
        }

        public virtual List<T> GetListAll()
        {
            try
            {
                List<T> retList = new List<T>();
                retList = query.SelectList("");
                return retList;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
        public virtual T Get(string vID)
        {
            try
            {
                return query.GetModel(vID);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
        public virtual void Update(T info)
        {
            try
            {
                exec.Update(info);
                exec.DBCommit();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
        public virtual void Insert(T info)
        {
            try
            {
                exec.Add(info);
                exec.DBCommit();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
        public virtual void Delete(T info)
        {
            try
            {
                exec.Delete(info);
                exec.DBCommit();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
        public virtual void Del(string vID)
        {
            try
            {
                Type type = typeof(T);
                object obj = type.Assembly.CreateInstance(type.FullName, true);
                if (!string.IsNullOrEmpty(vID))
                    SetKeyValue(vID, obj);
                exec.Delete(obj as BaseMODEL);
                exec.DBCommit();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
