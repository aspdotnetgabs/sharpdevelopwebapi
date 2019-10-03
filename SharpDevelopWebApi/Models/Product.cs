using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using Newtonsoft.Json;

namespace SharpDevelopWebApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int CategoryId {get; set;}
        [NotMapped]
        public Category Category {get; set;}
        
        public decimal Price { get; set; }
        
        [NotMapped]
        public string Photo { get; set; }        
        [JsonIgnore]
        public byte[] PhotoData { get; set; }
    }
}
