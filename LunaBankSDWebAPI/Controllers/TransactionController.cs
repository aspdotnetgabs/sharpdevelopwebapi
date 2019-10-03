using SharpDevelopWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace SharpDevelopWebApi.Controllers
{
	/// <summary>
	/// Description of TransactionController.
	/// </summary>
	public class TransactionController : ApiController
    {
        //		public int Id { get; set; }
//		public string AccountNumber { get; set; }
//		public int TransactionCodeId { get; set; }
//		
//		[NotMapped]
//		public int TransactionCode { get; set; }
//		public int Amount { get; set; }
//		public DateTime? Date { get; set; }
        SDWebApiDbContext _db = new SDWebApiDbContext();

        [HttpGet]
        [Route("api/GetAllTransactionData")]

        public IHttpActionResult GetAll(string keyword = "")
        {
            keyword = keyword.Trim();
            var transactions = new List<Transaction>();
            if (!string.IsNullOrEmpty(keyword))
            {
                transactions = _db.Transactions
                    .Where(x => x.AccountNumber.Contains(keyword))
                    .ToList();
            }
            else
                transactions = _db.Transactions.ToList();

            return Ok(transactions);
        }

        [HttpGet]
        public IHttpActionResult Get(int Id)
        {
            var customer = _db.Customers.Find(Id);
            if (customer != null)
                return Ok(customer);
            else
                return BadRequest("Customer not found");
        }

        [HttpPost]
        public IHttpActionResult Create(Customer newCustomer)
        {
            _db.Customers.Add(newCustomer);
            _db.SaveChanges();
            return Ok(newCustomer);
        }

        [HttpPut]
        public IHttpActionResult Update(Customer updatedCustomer)
        {
            var customer = _db.Customers.Find(updatedCustomer.Id);
            if (customer != null)
            {
                customer.LastName = updatedCustomer.LastName;
                customer.FirstName = updatedCustomer.FirstName;
                customer.Email = updatedCustomer.Email;
                customer.Phone = updatedCustomer.Phone;

                _db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();

                return Ok(customer);
            }
            else
                return BadRequest("Customer not found");
        }

        [HttpDelete]
        public IHttpActionResult Delete(int Id)
        {
            var customer = _db.Customers.Find(Id);
            if (customer != null)
            {
                _db.Customers.Remove(customer);
                _db.SaveChanges();
                return Ok("Customer successfully deleted");
            }
            else
                return BadRequest("Customer not found");
        }
    }
}