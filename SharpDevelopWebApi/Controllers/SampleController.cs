using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;
using Dapper;
using SharpDevelopWebApi.Helpers.JWT;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{
	/// <summary>
	/// Description of ValuesController.
	/// </summary>
	public class SampleController : ApiController
	{					
        [HttpPost]
        [Route("api/sample")]
        public IHttpActionResult Post(Product product)
        {
            return Ok(product);
        }
        
        [AllowAnonymous]
        [Route("api/sample/getcontacts")]
        public IHttpActionResult GetContacts()
        {
        	// @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + AppDomain.CurrentDomain.GetData("DataDirectory") + @"\MyAccessDb.mdb";
        	string mdb = ConfigurationManager.ConnectionStrings["MyAccessDb"].ConnectionString; // @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + AppDomain.CurrentDomain.GetData("DataDirectory") + @"\MyAccessDb.mdb";
        	var contact = new 
        	{
        		FullName = "Hewbhurt Gabon",
        		Email = "gabs@gmail.com",
        		BirthDate = "1981/09/17"
        	};

            using (var conn = new OleDbConnection(mdb))
            {
                //        		conn.Execute( "INSERT INTO contacts(FullName, Email, BirthDate) "
                //	            	+ "VALUES (@FullName, @Email, @BirthDAte)", contact);
                var contactList = conn.Query("Select Id, FullName, Email, BirthDate from contacts").ToList();
                return Ok(contactList);
            }
            
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