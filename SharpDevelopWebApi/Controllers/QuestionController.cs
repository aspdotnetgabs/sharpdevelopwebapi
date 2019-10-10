using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{
	public class QuestionController : ApiController
	{
		SDWebApiDbContext _db = new SDWebApiDbContext();
		
		[HttpGet]
        public IHttpActionResult Get(int Id)
        {
            var questions = _db.Questions.Find(Id);
            if (questions != null){
            	
                return Ok(questions);
            }
            else
                return BadRequest("Question not found");
            
        }
        
        [Route("api/question/allwithoutanswer")]
        [HttpGet]
        public IHttpActionResult GetAllWithoutAnswer()        
        {     
			var rnd = new Random(); // https://stackoverflow.com/a/43414937/1281209     
			
			var questions = _db.Questions.ToList()
        		.OrderBy(x => rnd.Next()).ToList();
        	
        	foreach (var quest in questions) 
        	{
        		quest.CorrectAnswer = ""; // Hide the correct answer
        	}
        	return Ok(questions);
        }
        
        [Route("api/question/all")]
        [HttpGet]
        public IHttpActionResult GetAll(){
        	
        	var questions = _db.Questions.ToList();
        	return Ok(questions);
        }
        
        
        [HttpPost]
		public IHttpActionResult Create(Question question)
		{
			_db.Questions.Add(question);
			_db.SaveChanges();
			return Ok(question);
		}
		
		[HttpPut]
		public IHttpActionResult Update(Question updatedQues)
		{
			var ques = _db.Questions.Find(updatedQues.Id);
			if(ques != null)
			{
				ques.QuestionText = updatedQues.QuestionText;
				ques.CorrectAnswer = updatedQues.CorrectAnswer;
				ques.ChoiceA = updatedQues.ChoiceA;
				ques.ChoiceB = updatedQues.ChoiceB;
				ques.ChoiceC = updatedQues.ChoiceC;
				
				_db.Entry(ques).State = System.Data.Entity.EntityState.Modified;
				_db.SaveChanges();
				return Ok(ques);
			}
			else
				return BadRequest("Question not found");
		}
		
		[HttpDelete]
		public IHttpActionResult Delete(int Id)			
		{
			var question = _db.Questions.Find(Id);
			if(question != null)
			{
				_db.Questions.Remove(question);
				_db.SaveChanges();
				return Ok("Deleted successfully");
			}
			else
				return BadRequest("Question not found");
		}
	}
}