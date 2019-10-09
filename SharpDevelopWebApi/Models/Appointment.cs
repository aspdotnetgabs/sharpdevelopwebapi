/*
 * Created by SharpDevelop.
 * User: HMW LENDING
 * Date: 29/09/2019
 * Time: 5:31 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpDevelopWebApi.Models
{
	/// <summary>
	/// Description of Appointment.
	/// </summary>
	public class Appointment
	{
		public int Id { get; set; }
		
		public int PatientUserId { get; set; }
		[NotMapped]
		public Patient Patient { get; set; } 	
		
		public int DoctorUserId { get; set; }
		[NotMapped]
		public Doctor Doctor { get; set; }
		
		public int DoctorAvailbleTimeId { get; set; }
		[NotMapped]
		public DoctorAvailableTimes DoctorAvailableTimes { get; set; }
		
		public string Reason { get; set; }
		public string Status { get; set; }
	}
}
