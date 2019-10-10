/*
 * Created by SharpDevelop.
 * User: clyde
 * Date: 10/6/2019
 * Time: 1:02 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace SharpDevelopWebApi.Models
{
	
	public class Account
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Lastname { get; set; }
		public string Firstname { get; set; }
		public string Gender { get; set; }
		public DateTime? BirthDate { get; set; }
		public string RecoveryEmail { get; set; }
	}
}
