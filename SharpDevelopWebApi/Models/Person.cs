/*
 * Created by SharpDevelop.
 * User: Gabs
 * Date: 31/07/2019
 * Time: 3:06 pm
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace SharpDevelopWebApi.Models
{
	/// <summary>
	/// Description of Person.
	/// </summary>
	public class Person
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime? BirthDate { get; set; }
		public string Gender { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
	}
}
