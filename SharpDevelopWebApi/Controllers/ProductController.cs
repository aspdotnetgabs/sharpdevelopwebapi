using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SharpDevelopWebApi.Models;

namespace SharpDevelopWebApi.Controllers
{
	public class ProductController : ApiController
	{
		SDWebApiDbContext _db = new SDWebApiDbContext();
		
		[HttpGet]
		public IHttpActionResult GetAll()
		{
			var products = _db.Products.ToList();
		
			foreach(var p in products)
			{
				p.Category = _db.Categories.Find(p.CategoryId) ?? new Category();
			}
			
			
			return Ok(products);
		}
		
		[HttpGet]
		public IHttpActionResult Get(int Id)
		{
			var product = _db.Products.Find(Id);
			if(product !=null)
			{
				product.Category = _db.Categories.Find(product.CategoryId) ?? new Category();
				return Ok(product);
			}				
			else
				return BadRequest("Product not found");
		}
		
		[HttpPost]
		public IHttpActionResult Create(Product newProduct)
		{
			_db.Products.Add(newProduct);
			_db.SaveChanges();
			return Ok(newProduct.Id);
		}
		
		[HttpPut]
		public IHttpActionResult Update(Product updatedProduct)
		{
			var product = _db.Products.Find(updatedProduct.Id);
			product.Name = updatedProduct.Name;
			product.Price = updatedProduct.Price;
			product.CategoryId = updatedProduct.CategoryId;
			product.Category = _db.Categories.Find(updatedProduct.CategoryId) ?? new Category();
			
			_db.Entry(product).State = System.Data.Entity.EntityState.Modified;
			_db.SaveChanges();
			
			return Ok(product);			
		}
		
		[HttpDelete]
		public IHttpActionResult Delete(int Id)
		{
			var product = _db.Products.Find(Id);
			if(product != null)
			{
				_db.Products.Remove(product);
				_db.SaveChanges();
				return Ok("Product successfully deleted.");
			}
			else
				return BadRequest("Product not found");
		}

		[HttpGet]
		[Route("api/Category")]
		public IHttpActionResult GetCategories()
		{
			var categories = _db.Categories.ToList();
			return Ok(categories);
		}
		
		[HttpPost]
		[Route("api/Category")]
		public IHttpActionResult CreateCategory(Category newCategory)
		{
			_db.Categories.Add(newCategory);
			_db.SaveChanges();
			return Ok(newCategory.Id);
		}
	}
}