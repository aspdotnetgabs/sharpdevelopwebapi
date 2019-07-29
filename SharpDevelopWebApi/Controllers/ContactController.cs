using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{	
	public class ContactController : ApiController
	{	
		[HttpGet]
		public IHttpActionResult GetContact()
		{
			var _db = new SDWebApiDbContext();
			var contacts = _db.Contacts.ToList();
			return Ok(contacts);
		}			
		
		[HttpPost]
		public IHttpActionResult PostContact(Contact c)
		{
			var _db = new SDWebApiDbContext();
			_db.Contacts.Add(c);
			_db.SaveChanges();
			return Ok(c);
		}
		
		
	}
}