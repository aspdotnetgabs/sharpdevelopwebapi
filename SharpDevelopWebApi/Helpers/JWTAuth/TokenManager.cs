

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JWTAuth
{
	public static class TokenManager
	{
		// Change this with your own secret
	    public const string secret = "SEiL4IiTEq4EW155ySS1T25GtXo68VVUvSNbsUw8Vm53YI6rBao86Fpne5venhn";

		public static string createToken(string username, string[] roles = null, int expireMinutes = 20)
		{
		    //Set issued at date
		    DateTime issuedAt = DateTime.UtcNow;
		    //set the time when it expires
		    DateTime expires = DateTime.UtcNow.AddMinutes(expireMinutes);
		
		    var tokenHandler = new JwtSecurityTokenHandler();
		  
		    //create a identity and add claims to the user which we want to log in  
	        var claims = new List<Claim>();
	        claims.Add(new Claim(ClaimTypes.Name, username));
	        if(roles != null)
	        {
	        	foreach (var role in roles)
	            {
	            	claims.Add(new Claim(ClaimTypes.Role, role.Trim()));
	            }        	
	        }     	    
	        
		    var claimsIdentity = new ClaimsIdentity(claims);        
		
		    var now = DateTime.UtcNow;
		    var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secret));
		    var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey,Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);
		
		
		    //create the jwt
		    var token = (JwtSecurityToken) tokenHandler.CreateJwtSecurityToken(
		    	subject: claimsIdentity, 
		    	notBefore: issuedAt, 
		    	expires: expires, 
		    	signingCredentials: signingCredentials
		    );
		    
		    var tokenString = tokenHandler.WriteToken(token);
		
		    return tokenString;
		}	
	}

}