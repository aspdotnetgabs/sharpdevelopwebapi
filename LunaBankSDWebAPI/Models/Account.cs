using System;
namespace SharpDevelopWebApi.Models
{

	public class Account
	{	
		public int Id { get; set; }
		public DateTime? Birthdate { get; set; }
		public string Phone { get; set; }
		public string AccountNumber { get; set; }
		public string Lastname { get; set; }
		public string Firstname { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
	}
}
