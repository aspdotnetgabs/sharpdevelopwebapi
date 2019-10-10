using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{
	public class MailController : ApiController
	{
		SDWebApiDbContext _db = new SDWebApiDbContext();
		
        [Route("api/mail/inbox/{username}")]
        [HttpGet]
        public IHttpActionResult GetInbox(string username)
        {        	
        	var mail = _db.Mails.Where(x => x.To == username).ToList();
        	return Ok(mail);
        }	

        [Route("api/mail/sent/{username}")]
        [HttpGet]
        public IHttpActionResult GetSent(string username)
        {        	
        	var mail = _db.Mails.Where(x=>x.From == username && x.Status == "sent").ToList();
        	return Ok(mail);
        }	  

        [Route("api/mail/draft/{username}")]
        [HttpGet]
        public IHttpActionResult GetDraft(string username)
        {        	
        	var mail = _db.Mails.Where(x=>x.From == username && x.Status == "draft").ToList();
        	return Ok(mail);
        }
        
        [Route("api/mail/trash/{username}")]
        [HttpGet]
        public IHttpActionResult GetTrash(string username)
        {        	
        	var mail = _db.Mails.Where(x=>x.From == username && x.Status == "trash").ToList();
        	return Ok(mail);
        }      

        [Route("api/mail/unsent/{username}")]
        [HttpGet]
        public IHttpActionResult GetUnsent(string username)
        {        	
        	var mail = _db.Mails.Where(x=>x.From == username && x.Status == "unsent").ToList();
        	return Ok(mail);
        }  

        [Route("api/mail/search/{username}")]
        [HttpGet]
        public IHttpActionResult SearchMail(string username, string keyword)
        {        	
        	var mail = _db.Mails
        		.Where(x => x.From == username && 
        		       (x.Subject.Contains(keyword) || x.Body.Contains(keyword) || x.Body.Contains(keyword) || x.From.Contains(keyword)))
        		.ToList();
        	
        	return Ok(mail);
        }	 
		
		[HttpGet]
        public IHttpActionResult Get(int Id)
        {
            var mail = _db.Mails.Find(Id);
            if (mail != null){            	
                return Ok(mail);
            }
            else
                return BadRequest("Mail not found");           
        }               
        
        [HttpPost]
        [Route("api/mail/savedraft")]
		public IHttpActionResult Compose(Mail mail)
		{
			mail.Status = "draft";
			_db.Mails.Add(mail);
			_db.SaveChanges();
			return Ok(mail);
		}
		
        [HttpPut]
        [Route("api/mail/sendmail/{MailId}")]
		public IHttpActionResult Send(int MailId)
		{
			var mail = _db.Mails.Find(MailId);
			var recipientExists = _db.Accounts.Where(x => x.UserName == mail.To).FirstOrDefault();
			                                         
			if(mail != null && recipientExists != null)
			{
				mail.Status = "sent";
				_db.Entry(mail).State = System.Data.Entity.EntityState.Modified;
				_db.SaveChanges();
				return Ok("Successfully sent.");
			}
			else
			{
				mail.Status = "unsent";
				_db.Entry(mail).State = System.Data.Entity.EntityState.Modified;
				_db.SaveChanges();			
				return BadRequest("Sending failed");			
			}
		}
		
		[HttpPut]
		[Route("api/mail/updatedraft")]
		public IHttpActionResult UpdateDraft(Mail updatedMail)			
		{
			var mail = _db.Mails.Where(x => x.Id == updatedMail.Id && x.Status == "draft").FirstOrDefault();
			if(mail != null)
			{
				mail.To = updatedMail.To;
				mail.Subject = updatedMail.Subject;
				mail.Body = updatedMail.Body;	
				mail.Attachment = mail.Attachment;
				_db.Entry(mail).State = System.Data.Entity.EntityState.Modified;
				_db.SaveChanges();
				return Ok("Changes successfully saved.");
			}
			else
				return BadRequest("Mail not found");
		}		
		
		[HttpDelete]
		[Route("api/mail/movetotrash/{MailId}")]
		public IHttpActionResult MoveToTrash(int MailId)			
		{
			var mail = _db.Mails.Find(MailId);
			if(mail != null)
			{
				mail.Status = "trash";
				_db.Entry(mail).State = System.Data.Entity.EntityState.Modified;
				_db.SaveChanges();
				return Ok("Successfully moved to trash");
			}
			else
				return BadRequest("Mail not found");
		}		
		
		[HttpDelete]
		[Route("api/mail/delete/{MailId}")]
		public IHttpActionResult Delete(int MailId)			
		{
			var mail = _db.Mails.Find(MailId);
			if(mail != null)
			{
				_db.Mails.Remove(mail);
				_db.SaveChanges();
				return Ok("Deleted successfully");
			}
			else
				return BadRequest("Mail not found");
		}
	}
}