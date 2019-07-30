using SharpDevelopWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SharpDevelopWebApi.Controllers
{
    public class CustomerController : ApiController
    {
        SDWebApiDbContext _db = new SDWebApiDbContext();

        [HttpGet]
        public IHttpActionResult GetAll(string keyword = "")
        {
            keyword = keyword.Trim();
            var customers = new List<Customer>();
            if(!string.IsNullOrEmpty(keyword))
            {
                customers = _db.Customers
                    .Where(x => x.LastName.Contains(keyword) || x.FirstName.Contains(keyword))
                    .ToList();
            }
            else
                customers = _db.Customers.ToList();

            return Ok(customers);
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
