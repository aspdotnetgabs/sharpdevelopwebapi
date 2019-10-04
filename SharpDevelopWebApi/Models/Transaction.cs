using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace SharpDevelopWebApi.Models
{
	public class Transaction
	{
		public int Id { get; set; }
		public string AccountNumber { get; set; }

		public int TransactionTypeId { get; set; }		
		[NotMapped]
		public TransactionType TransactionType { get; set; }
		
		
		public string ToAccount { get; set; }
		public int Amount { get; set; }
		public DateTime? Date { get; set; }
		public string Notes {get;set;}
	}
}
