using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace SharpDevelopWebApi.Models
{
	public class Transaction
	{
		public int Id { get; set; }
		public string AccountNumber { get; set; }
		public int TransactionCodeId { get; set; }
		
		[NotMapped]
		public int TransactionCode { get; set; }
		public int Amount { get; set; }
		public DateTime? Date { get; set; }
	}
}
