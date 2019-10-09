/*
 * Created by SharpDevelop.
 * User: HMW LENDING
 * Date: 29/09/2019
 * Time: 5:32 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpDevelopWebApi.Models
{
	/// <summary>
	/// Description of DoctorAvailableTimes.
	/// </summary>
	public class DoctorAvailableTimes
	{
		public int Id { get; set; }
		
		public int DoctorUserId { get; set; }
		[NotMapped]
		public Doctor Doctor {get; set;}
		
		public DateTime? Date { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
	}
}
