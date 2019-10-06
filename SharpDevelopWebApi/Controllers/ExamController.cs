using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{
	public class ExamController : ApiController
	{
		SDWebApiDbContext _db = new SDWebApiDbContext();
		
		[HttpGet]
        public IHttpActionResult Get(int Id)
        {
            var exam = _db.Exams.Find(Id);
            if (exam != null){
            	
                return Ok(exam);
            }
            else
                return BadRequest("Exam not found!");
            
        }
        
        [Route("api/exam/all")]
        [HttpGet]
        public IHttpActionResult GetAll(){
        	
        	var exam = _db.Exams.ToList();
        	return Ok(exam);
        }
        
        [HttpPost]
        public IHttpActionResult Create(Exam exam)
        {      	
        	_db.Exams.Add(exam);
			_db.SaveChanges();
			return Ok(exam);      	
        }
        
        [HttpPut]
        public IHttpActionResult Update(Exam updatedExam)
        {
        	var exam = _db.Exams.Find(updatedExam.Id);
        	if(exam != null){
        		
        		exam.QuestionId = updatedExam.QuestionId;
        		exam.Question = updatedExam.Question;
        		exam.ExamineeId = updatedExam.ExamineeId;
        		exam.Examinee = updatedExam.Examinee;
        		exam.ExamineeAnswer = updatedExam.ExamineeAnswer;
        		
        		_db.Entry(exam).State = System.Data.Entity.EntityState.Modified;
				_db.SaveChanges();
				return Ok(exam);
        	}
        	else{
        		
        		return BadRequest("Exam not found!");
        	}
        	
        }
        
        [HttpDelete]
        public IHttpActionResult Delete(int Id){
        	
        	var exam = _db.Exams.Find(Id);
        	if(exam != null)
			{
				_db.Exams.Remove(exam);
				_db.SaveChanges();
				return Ok("Deleted successfully");
			}
			else
				return BadRequest("Exam not found!");
        }
	}
}