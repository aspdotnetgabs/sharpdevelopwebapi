using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

public class ApiAuthorize : System.Web.Http.AuthorizeAttribute
{
    public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
    {
        if (HttpContext.Current.Session["currentUser"] != null)
        {
            HttpContext.Current.Response.AddHeader("AuthenticationStatus", "Authorized");
            //actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK);
        }
        else
        {
            HttpContext.Current.Response.AddHeader("AuthenticationStatus", "Unauthorized");
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        }
    }
}
