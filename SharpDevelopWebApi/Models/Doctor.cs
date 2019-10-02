using SharpDevelopWebApi.Models;
public class Doctor : Person
{
	public string Specialization { get; set; }
	public int UserId { get; set; }
}