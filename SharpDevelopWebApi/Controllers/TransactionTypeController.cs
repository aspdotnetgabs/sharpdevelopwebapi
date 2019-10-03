using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{
	/// <summary>
	/// Description of TransactionTypeController.
	/// </summary>
	public class TransactionTypeController : ApiController
	{
	//	public int Id { get; set; }
	//	public string TransactionName { get; set; } //	(Fund Transfer, Pay Bills, Phone Reload) 

        SDWebApiDbContext _db = new SDWebApiDbContext();

        [HttpGet]
        [Route("api/GetAllTransactionTypes")]
        public IHttpActionResult GetAll(string keyword = "")
        {
            keyword = keyword.Trim();
            var transactiontypes = new List<TransactionType>();
            if (!string.IsNullOrEmpty(keyword))
            {
                transactiontypes = _db.TransactionTypes
                    .Where(x => x.TransactionName.Contains(keyword))
                    .ToList();
            }
            else
                transactiontypes = _db.TransactionTypes.ToList();

            return Ok(transactiontypes);
        }

        [HttpGet]
	//	public int Id { get; set; }
	//	public string TransactionName { get; set; } //	(Fund Transfer, Pay Bills, Phone Reload) 

        public IHttpActionResult Get(int Id)
        {
            var transactiontypes = _db.TransactionTypes.Find(Id);
            if (transactiontypes != null)
                return Ok(transactiontypes);
            else
                return BadRequest("Transaction type not found");
        }

        [HttpPost]
        public IHttpActionResult Create(TransactionType newtTransactionType)
        {
            _db.TransactionTypes.Add(newtTransactionType);
            _db.SaveChanges();
            return Ok(newtTransactionType);
        }


        [HttpPut]
        public IHttpActionResult Update(TransactionType updatedTransactionType)
        {
            var transactiontypes = _db.TransactionTypes.Find(updatedTransactionType.Id);
            if (transactiontypes != null)
            {
                transactiontypes.TransactionName = updatedTransactionType.TransactionName;


                _db.Entry(transactiontypes).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();

                return Ok(transactiontypes);
            }
            else
                return BadRequest("Transaction type not found");
        }

        
        [HttpDelete]
        public IHttpActionResult Delete(int Id)
        {
            var transactiontypes = _db.TransactionTypes.Find(Id);
            if (transactiontypes != null)
            {
                _db.TransactionTypes.Remove(transactiontypes);
                _db.SaveChanges();
                return BadRequest("Transaction type successfully deleted");
            }
            else
                return BadRequest("Transaction type not found");
        }
	}
}