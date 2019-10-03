using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{

	public class AccountWalletController : ApiController
	{

        SDWebApiDbContext _db = new SDWebApiDbContext();

        [HttpGet]
        [Route("api/GetAllWalletData")]
        public IHttpActionResult GetAll(string keyword = "")
        {
            keyword = keyword.Trim();
            var userwallet = new List<UserWallet>();
            if(!string.IsNullOrEmpty(keyword))
            {
                userwallet = _db.UserWallets
                    .Where(x => x.Username.Contains(keyword))
                    .ToList();
            }
            else
                userwallet = _db.UserWallets.ToList();

            return Ok(userwallet);
        }

        [HttpGet]
        public IHttpActionResult Get(int Id)
        {
            var userwallet = _db.UserWallets.Find(Id);
            if (userwallet != null)
                return Ok(userwallet);
            else
                return BadRequest("Account not found");
        }

        [HttpPost]
        public IHttpActionResult Create(UserWallet newCustomer)
        {
            _db.UserWallets.Add(newCustomer);
            _db.SaveChanges();
            return Ok(newCustomer);
        }

        [HttpPut]
        public IHttpActionResult Update(UserWallet updatedCustomer)
        {
            var userwallet = _db.UserWallets.Find(updatedCustomer.Username);
            if (userwallet != null)
            {
                userwallet.Wallet = updatedCustomer.Wallet;

                _db.Entry(userwallet).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();

                return Ok(userwallet);
            }
            else
                return BadRequest("Account not found");
        }

        [HttpDelete]
        public IHttpActionResult Delete(int Id)
        {
            var userwallet = _db.UserWallets.Find(Id);
            if (userwallet != null)
            {
                _db.UserWallets.Remove(userwallet);
                _db.SaveChanges();
                return Ok("Account successfully deleted");
            }
            else
                return BadRequest("Account not found");
        }
     }
}

