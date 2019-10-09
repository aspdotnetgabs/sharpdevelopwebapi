using SharpDevelopWebApi.Models;
public class Patient: Person
{
	public int UserId { get; set; }
	public string Email { get; set; }
}