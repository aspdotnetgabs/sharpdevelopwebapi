using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{
	/// <summary>
	/// Description of SongController.
	/// </summary>
	public class SongController : ApiController
	{
		SDWebApiDbContext _db = new SDWebApiDbContext();
		
		[HttpGet]
		public IHttpActionResult GetAll()
		{		
			List<Song> songs = _db.Songs.ToList();
			return Ok(songs);
		}
		
		[HttpGet] 
		public IHttpActionResult Get(int Id)
		{
			var song = _db.Songs.Find(Id);
			if(song != null)
				return Ok(song);
			else
				return BadRequest("Song not found");
			
		}
		
		[HttpPost]
		public IHttpActionResult Create([FromBody]Song song)
		{			
			_db.Songs.Add(song);
			_db.SaveChanges();
			return Ok(song.Id);
		}
		
		[HttpPut]
		public IHttpActionResult Update([FromBody]Song updatedSong)
		{
			var song = _db.Songs.Find(updatedSong.Id);
			if(song != null)
			{
				song.Artist = updatedSong.Artist;
				song.Title = updatedSong.Title;
				song.Genre = updatedSong.Genre;
				_db.Entry(song).State = EntityState.Modified;
				_db.SaveChanges();
				
				return Ok(song);			
			}
			else
			{
				return BadRequest("Song not found");
			}

		}
		
	
		[HttpDelete]
		public IHttpActionResult Delete(int Id)
		{
			var songToDelete = _db.Songs.Find(Id);
			if(songToDelete != null)
			{
				_db.Songs.Remove(songToDelete);
				_db.SaveChanges();
				return Ok("Successfully deleted");
			}
			else
			{
				return BadRequest("Song not found");
			}
		}
	}
}