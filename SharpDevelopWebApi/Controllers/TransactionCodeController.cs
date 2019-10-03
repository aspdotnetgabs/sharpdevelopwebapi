using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{

	public class TransactionCodeController : ApiController
	{

        SDWebApiDbContext _db = new SDWebApiDbContext();

        [HttpGet]
        [Route("api/GetAllTransactionCodeData")]
        public IHttpActionResult GetAll(string keyword = "")
        {
            keyword = keyword.Trim();
            var trcode = new List<TransactionCode>();
            if(!string.IsNullOrEmpty(keyword))
            {
                trcode = _db.TransactionCodes
                    .Where(x => x.Name.Contains(keyword))
                    .ToList();
            }
            else
                trcode = _db.TransactionCodes.ToList();

            return Ok(trcode);
        }

        [HttpGet]
        public IHttpActionResult Get(int TransactionTypeId)
        {
            var trcode = _db.TransactionCodes.Find(TransactionTypeId);
            if (trcode != null)
                return Ok(trcode);
            else
                return BadRequest("Transaction code not found");
        }


        [HttpPost]
        public IHttpActionResult Create(TransactionCode newTrCode)
        {
            _db.TransactionCodes.Add(newTrCode);
            _db.SaveChanges();
            return Ok(newTrCode);
        }


        [HttpDelete]
        public IHttpActionResult Delete(int Id)
        {
            var trcode = _db.TransactionCodes.Find(Id);
            if (trcode != null)
            {
                _db.TransactionCodes.Remove(trcode);
                _db.SaveChanges();
                return Ok("Transaction code successfully deleted");
            }
            else
                return BadRequest("Transaction code not found");
        }
	}
}