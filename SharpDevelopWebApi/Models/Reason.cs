/*
 * Created by SharpDevelop.
 * User: Elle
 * Date: 10/2/2019
 * Time: 10:12 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace SharpDevelopWebApi.Models
{
	/// <summary>
	/// Description of Reason.
	/// </summary>
	public class Reason
	{
		public int Id { get; set; }
		public string ReasonText { get; set; }
		public string Description { get; set; }
	}
}
