using AngularUI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AngularUI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ServiceController : ApiController
    {
        [RequestAuthorize]
        [HttpPost]
        [Route("test")]
        public object LoadAllTest([FromBody]string value)
        {
            return "";
        }
    }
}