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
using System.Web.Mvc;
using System.Web.Routing;

namespace SharpDevelopWebApi
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			GlobalConfiguration.Configuration.MapHttpAttributeRoutes();

			GlobalConfiguration.Configuration
				.Routes.MapHttpRoute("Default", "{controller}/{id}", new { id = RouteParameter.Optional });
//			GlobalConfiguration.Configuration.Routes
//				.MapHttpRoute(
//			        name: "Default",
//			        routeTemplate: "api/{controller}/{action}/{id}",
//			        defaults: new { controller = "Values", action = "Get", id = RouteParameter.Optional }
//			    );
			
			GlobalConfiguration.Configuration
				.Formatters.JsonFormatter.MediaTypeMappings
				.Add(new System.Net.Http.Formatting.RequestHeaderMapping("Accept", "text/html", StringComparison.InvariantCultureIgnoreCase, true, "application/json"));
			
			GlobalConfiguration.Configuration.EnsureInitialized(); 
		}
	}
}
