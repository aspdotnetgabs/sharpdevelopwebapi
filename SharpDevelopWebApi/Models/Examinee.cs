/*
 * Created by SharpDevelop.
 * User: clyde
 * Date: 9/22/2019
 * Time: 11:36 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpDevelopWebApi.Models
{
	public class Examinee : Person
	{
		[NotMapped]
		public int Score { get; set; }	
	}
}
