/*
 * Created by SharpDevelop.
 * User: Elle
 * Date: 10/2/2019
 * Time: 9:56 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpDevelopWebApi.Models
{
	/// <summary>
	/// Description of Employee.
	/// </summary>
	public class Employee
	{
		public int Id { get; set; }
		
		public int UserId { get; set; }
		
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public int DepartmentId { get; set; }
		[NotMapped]
		public string Department { get; set; }
		public string Position { get; set; }
		
	}
}
