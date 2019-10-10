/*
 * Created by SharpDevelop.
 * User: clyde
 * Date: 10/6/2019
 * Time: 1:03 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace SharpDevelopWebApi.Models
{
	
	public class Mail
	{
		public int Id { get; set; }
		public string To { get; set; }
		public string From { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public string Attachment { get; set; }
		public string Status { get; set; } // draft, sent, unsent, trash
		
	}
}
