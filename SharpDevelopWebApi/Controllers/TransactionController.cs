using SharpDevelopWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace SharpDevelopWebApi.Controllers
{
	public class TransactionController : ApiController
    {
        SDWebApiDbContext _db = new SDWebApiDbContext();

        [HttpPost]
        public IHttpActionResult CreateTransaction(Transaction trans) 
        {        
        	trans.Date = DateTime.Now;
        	_db.Transactions.Add(trans);
        	_db.SaveChanges();
        	return Ok(trans.Id);
        }
       	
    }
}