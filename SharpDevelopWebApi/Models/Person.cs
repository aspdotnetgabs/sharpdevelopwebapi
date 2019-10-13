/*
 * Created by SharpDevelop.
 * User: clyde
 * Date: 9/27/2019
 * Time: 7:35 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace SharpDevelopWebApi.Models
{
	public class Person
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string Lastname { get; set; }
		public string Firstname { get; set; }
		public string Gender { get; set; }
	}
}
