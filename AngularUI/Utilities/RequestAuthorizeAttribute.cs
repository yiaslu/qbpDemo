using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace AngularUI.Utilities
{
    public class RequestAuthorizeAttribute : AuthorizeAttribute
    {
        //重写基类的验证方式，加入我们自定义的Ticket验 证
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //从http请求的头里面获取身份验证信息，验证是否是请求发起方的ticket

            var authorization = actionContext.Request.Headers.Authorization;


            if ((authorization != null) && (authorization.Parameter != null))
            {
                ////解密用户ticket,并校验用户名密码是否匹配
                //var encryptTicket = authorization.Parameter;
                //if (ValidateTicket(encryptTicket))
                //{
                //    base.IsAuthorized(actionContext);
                //}
                //else
                //{
                //    HandleUnauthorizedRequest(actionContext);
                //}
            }
            //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401
            else
            {

                //var attributes = actionContext.ActionDescriptor.GetType().GetCustomAttributes(true).ToList();
                //bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);
                //if (isAnonymous) base.OnAuthorization(actionContext);
                //else HandleUnauthorizedRequest(actionContext);
            }
        }
    }
}