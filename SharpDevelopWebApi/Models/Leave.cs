/*
 * Created by SharpDevelop.
 * User: Elle
 * Date: 10/2/2019
 * Time: 9:59 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpDevelopWebApi.Models
{
	/// <summary>
	/// Description of Leave.
	/// </summary>
	public class Leave
	{
		public int Id { get; set; }
		
		public int EmployeeUserId { get; set; }
		[NotMapped]
		public Employee Employee { get; set; }
		
		public DateTime FilingDate { get; set; }
		public DateTime DateFrom { get; set; }
		public DateTime DateTo { get; set; }
		
		public int ReasonId { get; set; }
		[NotMapped]
		public Reason Reason { get; set; }
		
		public string SpecifiedReason { get; set; }
		public string Status { get; set; } // pending, approved, denied
		
		
	}
}
