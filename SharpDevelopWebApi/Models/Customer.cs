using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharpDevelopWebApi.Models
{
	public class Customer : Person
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PhotoUrl { get; set; }
    }
}