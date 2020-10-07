using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;

namespace SharpDevelopWebApi.Controllers
{
	/// <summary>
	/// Description of SongController.
	/// </summary>
	public class SongController : ApiController
	{
		[Route("api/song/get1song")]
		[HttpGet]
		public IHttpActionResult GetSong()
		{
			return Ok(mySong);
		}
		
		[Route("api/song")]
		[HttpGet]
		public IHttpActionResult GetSongs()
		{
			var songs = new List<Song>();
			
			var song1 = new Song();
			song1.Id = 1;
			song1.Title = "Show Me The Way To Heart";
			song1.Artist = "Scott Grimes";
			song1.Genre = "Ballad";
			songs.Add(song1);
			
			var mySong = new Song();
			mySong.Id = 2;
			mySong.Title = "Hello My Love";
			mySong.Artist = "Westlife";
			mySong.Genre = "Pop";		
			songs.Add(mySong);
			
			var song2 = new Song
			{
				Id = 3,
				Title = "Back For Good",
				Artist = "Take That",
				Genre = "Pop"
			};
			songs.Add(song2);
				
			return Ok(songs);
		}
		
	}
}