/*
 * Created by SharpDevelop.
 * User: clyde
 * Date: 9/22/2019
 * Time: 9:00 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace SharpDevelopWebApi.Models
{
	public class Question : Choice
	{
		public string QuestionText { get; set; }
		public string CorrectAnswer { get; set; }
	}
}
