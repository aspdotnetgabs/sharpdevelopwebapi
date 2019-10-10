/*
 * Created by SharpDevelop.
 * User: clyde
 * Date: 9/27/2019
 * Time: 7:43 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace SharpDevelopWebApi.Models
{
	public class Exam
	{
	 	public int Id { get; set; }
	 	public int QuestionId { get; set; }
	 	public string Question { get; set; }
	 	public int ExamineeUserId { get; set; }
	 	public string Examinee { get; set; }
	 	public string ExamineeAnswer { get; set; }
	}
}
