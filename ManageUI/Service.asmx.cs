using PublicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ManageUI
{
    /// <summary>
    /// Service 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public object Send(string roots)
        {
            GearClass request = new GearClass(roots);

            //返回调用方法获取的值
            return request.Send();
        }

        [WebMethod(EnableSession = true)]
        public string GetSessionValue(string name)
        {
            return Session[name] + "";
        }
    }
}
