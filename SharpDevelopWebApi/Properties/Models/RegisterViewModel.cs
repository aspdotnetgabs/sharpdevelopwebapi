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
	public string Lastname { get; set; }
	public string Firstname { get; set; }
	public string Gender { get; set; }
	
	
}