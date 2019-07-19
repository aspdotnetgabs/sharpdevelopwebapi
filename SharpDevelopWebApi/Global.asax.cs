/*
 * Created by SharpDevelop.
 * User: Gabs
 * Date: 19/07/2019
 * Time: 2:03 am
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using Swashbuckle.Application;

namespace SharpDevelopWebApi
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			var config = GlobalConfiguration.Configuration;
			
			config.EnableCors(new EnableCorsAttribute("*","*","*"));		
			
			config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            // Redirect root to Swagger UI
            config.Routes.MapHttpRoute(
                name: "Swagger UI",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler(SwaggerDocsConfig.DefaultRootUrlResolver, "swagger"));

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.EnsureInitialized();
        }

        #region SessionInWebAPI
        // Avoid Session at all cost!!!
        // Use Session in Web Api in some special cases ONLY if you know what you're doing.
        public override void Init()
        {
            this.PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            bool IsWebApiRequest = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/api");
            if (IsWebApiRequest)
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }
        #endregion
    }
}
