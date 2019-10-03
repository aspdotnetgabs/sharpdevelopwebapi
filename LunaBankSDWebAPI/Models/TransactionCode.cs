using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace SharpDevelopWebApi.Models
{
	public class TransactionCode
	{
	public int Id { get; set; }
	public int TransactionTypeId { get; set; }
	
	[NotMapped]
	public int TransactionType { get; set; }
	public int AccountBillNumber { get; set; }
    public string Name { get; set; } //	(Mock Deposit, To Person Account, MasterCard, Visa, Smart, Globe, PLDT, Parasat, Cepalco)
	}
}
