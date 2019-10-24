using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;
using LiteDB;
using SharpDevelopWebApi.Helpers.JWT;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{
	/// <summary>
	/// Description of ValuesController.
	/// </summary>
	public class SampleController : ApiController
	{
		[HttpGet]
		[Route("api/sample/LiteDb")]
        public IHttpActionResult GetLiteDb()
		{
			// Open database (or create if doesn't exist)
			using(var db = new LiteDatabase(HostingEnvironment.MapPath("~/App_Data/MyData.db")))
			{
			    // Get customer collection
			    var col = db.GetCollection<Customer>("customers");					
			
			    // Use LINQ to query documents (with no index)
			    var results = col.FindAll();
			    
			    return Ok(results);
			}			
			
		}
        
		[HttpPost]
		[Route("api/sample/LiteDb")]
        public IHttpActionResult CreateLiteDb(Customer customer)
		{
			// Open database (or create if doesn't exist)
			using(var db = new LiteDatabase(HostingEnvironment.MapPath("~/App_Data/MyData.db")))
			{
			    // Get customer collection
			    var col = db.GetCollection<Customer>("customers");		
			
			    // Create unique index in Name field
			    col.EnsureIndex(x => x.LastName, true);
			
			    // Insert new customer document (Id will be auto-incremented)
			    var bson = col.Insert(customer);

			
			    // Use LINQ to query documents (with no index)
			    //var results = col.FindAll();
			    
			    return Ok(customer);
			}	
		}        

		
		[HttpGet]
		[Route("api/sample")]
        public IHttpActionResult Get(string url = "")
		{
        	 var data = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Templates/bootstrap.html"));
            
        	 var result = TuesPechkinPdf.ToPdf(data);
        	
			System.IO.File.WriteAllBytes(@"C:\Users\DRIVE_D\My Documents\_GIT_VCS\AspDotNetGabs\sharpdevelopwebapi\SharpDevelopWebApi\Templates\testpdf.pdf", result);
						
			return Ok(result.ToBase64String());
		}

        [HttpPost]
        [Route("api/sample")]
        public IHttpActionResult Post(Product product)
        {
            return Ok(product);
        }

        [HttpPost]
        [FileUpload.SwaggerForm()]
        [Route("api/sample/upload")]
        public IHttpActionResult UploadFile()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/UploadedFiles/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    docfiles.Add(System.IO.Path.GetFileName(filePath));
                }
                return Ok(new { docfiles });
            }

            return BadRequest();
        }
        
        [HttpPost]
        [FileUpload.SwaggerForm()]
        [Route("api/sample/uploadphoto")]
        public IHttpActionResult UploadImage()
        {
        	var postedFile = HttpContext.Current.Request.Files[0];
            var filePath = postedFile.SaveAsJpegFile();
            return Ok(filePath);
        }    


        [HttpPost]
        [Route("api/sample/sendmail")]
        public IHttpActionResult SendEmail(string EmailTo, string Subject, string Message)
        {
            var success = EmailService.SendEmail(EmailTo, Subject, Message);
            if (success)
                return Ok("Successfully sent.");
            else
                return BadRequest("Sending failed.");
        }
    }



}