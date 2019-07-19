using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{
	/// <summary>
	/// Description of ValuesController.
	/// </summary>
	public class ValuesController : ApiController
	{
		[HttpGet]
		[Route("api/values")]
        public IHttpActionResult Get()
		{
			var products = new List<Product>()
	        { 
	            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 }, 
	            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M }, 
	            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M } 
	        };
			
			return Ok(products);
		}

        [HttpPost]
        [FileUpload.SwaggerForm()]
        [Route("api/values")]
        public IHttpActionResult Post(Product product)
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    docfiles.Add(System.IO.Path.GetFileName(filePath));
                }
                return Ok(new { product, docfiles });
            }

            return BadRequest();
        }
	}
}