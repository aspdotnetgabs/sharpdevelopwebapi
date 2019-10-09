using SharpDevelopWebApi.Models;
public class Doctor : Person
{
	public int UserId { get; set; }
	public string Specialization { get; set; }
	public string Email { get; set; }
}