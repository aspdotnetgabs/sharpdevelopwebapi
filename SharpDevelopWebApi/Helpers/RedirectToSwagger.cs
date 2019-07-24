using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace SharpDevelopWebApi.Helpers
{
    public class HomeController : ApiController
    {
        [Route("home")]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult Index()
        {
            return Redirect(Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/swagger");
        }
    }
}