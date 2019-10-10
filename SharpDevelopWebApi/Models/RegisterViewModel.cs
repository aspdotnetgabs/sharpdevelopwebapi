using System.ComponentModel.DataAnnotations;


// DO NOT MAPPED in DbContext
public class RegisterViewModel
{
	[Required]
	public string UserName { get; set; }
	[Required]
	public string Password { get; set; }
	public string Role { get; set; }
	
	// Add you Registration fields here...
	[Required]
	public string LastName { get; set; }
	[Required]
	public string FirstName { get; set; }
	
	[Required]
	public int DepartmentId { get; set; }
	
	public string Position { get; set; }
}