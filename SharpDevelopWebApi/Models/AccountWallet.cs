using System;
namespace SharpDevelopWebApi.Models
{

	public class AccountWallet
	{
		public int Id { get; set; } // Class entity must have and Id Primary Key
		
		public float Wallet { get; set; }
		public string Username { get; set; }
	}

}
