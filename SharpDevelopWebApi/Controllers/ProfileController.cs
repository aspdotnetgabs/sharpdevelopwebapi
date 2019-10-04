using SharpDevelopWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SharpDevelopWebApi.Controllers
{
    public class ProfileController : ApiController
    {
        

        SDWebApiDbContext _db = new SDWebApiDbContext();

        [HttpGet]
        [Route("AccountProfileControllerGetAll")]
        public IHttpActionResult GetAll(string keyword = "")
        {
            keyword = keyword.Trim();
            var accounts = new List<Account>();
            if (!string.IsNullOrEmpty(keyword))
            {
                accounts = _db.Accounts
                    .Where(x => x.Lastname.Contains(keyword) || x.Firstname.Contains(keyword))
                    .ToList();
            }
            else
                accounts = _db.Accounts.ToList();

            return Ok(accounts);
        }
        
		[HttpGet]
        [Route("api/getAccountProfile/{Id}")]
        public IHttpActionResult Get(int Id)
        {
        	var accounts = _db.Accounts.Find(Id);
            if (accounts != null)
                return Ok(accounts);
            else
                return BadRequest("Account profile not found");
        }        

        [HttpGet]
        [Route("api/getAccountProfileByUserId")]
        public IHttpActionResult GetByUserId(int userId)
        {
        	var accounts = _db.Accounts.Where(x => x.UserId == userId).FirstOrDefault();
            if (accounts != null)
                return Ok(accounts);
            else
                return BadRequest("Account profile not found");
        }

        [HttpPost]
        [Route("api/NewAccountProfile")]

        public IHttpActionResult Create(Account newAccount)
        {
            _db.Accounts.Add(newAccount);
            _db.SaveChanges();
            return Ok(newAccount);
        }


        [HttpPut]
        [Route("api/UpdateAccountProfile")]
        public IHttpActionResult Update(Account updatedAccountProfile)
        {
            var account = _db.Accounts.Find(updatedAccountProfile.Id);
            if (account != null)
            {
                account.Birthdate = updatedAccountProfile.Birthdate;
                account.Phone = updatedAccountProfile.Phone;
                account.AccountNumber = updatedAccountProfile.AccountNumber;
                account.Lastname = updatedAccountProfile.Lastname;
                account.Firstname = updatedAccountProfile.Firstname;
                account.Address = updatedAccountProfile.Address;
                account.Email = updatedAccountProfile.Email;

                _db.Entry(account).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();

                return Ok(account);
            }
            else
                return BadRequest("Account profile not found");
        }
        //public int Id { get; set; }
        //public DateTime? Birthdate { get; set; }

        //public int Phone { get; set; }
        //public string AccountNumber { get; set; }
        //public string Lastname { get; set; }
        //public string Firstname { get; set; }
        //public string Address { get; set; }
        //public string Email { get; set; }
        [HttpDelete]
        [Route("api/DeleteAccountProfile/{Id}")]

        public IHttpActionResult Delete(int Id)
        {
            var account = _db.Accounts.Find(Id);
            if (account != null)
            {
                _db.Accounts.Remove(account);
                _db.SaveChanges();
                return Ok("Account profile successfully deleted");
            }
            else
                return BadRequest("Account profile not found");
        }
    }
}
