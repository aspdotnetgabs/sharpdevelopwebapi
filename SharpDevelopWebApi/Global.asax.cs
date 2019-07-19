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
using Swashbuckle.Application;

namespace SharpDevelopWebApi
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			var config = GlobalConfiguration.Configuration;
			
			var cors = new EnableCorsAttribute("*","*","*");
			config.EnableCors(cors);
			
			config.MapHttpAttributeRoutes();
	      	// Redirect root to Swagger UI
	        config.Routes.MapHttpRoute(
	            name: "Swagger UI",
	            routeTemplate: "",
	            defaults: null,
	            constraints: null,
	            handler: new RedirectHandler(SwaggerDocsConfig.DefaultRootUrlResolver, "swagger/ui/index"));
        
			config.Formatters.Remove(config.Formatters.XmlFormatter);		
			config.EnsureInitialized(); 
		}
	}
}
