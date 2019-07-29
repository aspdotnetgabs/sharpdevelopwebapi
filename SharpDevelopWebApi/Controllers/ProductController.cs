using SharpDevelopWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SharpDevelopWebApi.Controllers
{
    [RoutePrefix("api")]
    public class ProductController : ApiController
    {
        [HttpGet]
        [Route("product")]
        public IHttpActionResult GetAll()
        {
            return Ok();
        }

        [HttpGet]
        [Route("product/{Id}")]
        public IHttpActionResult Get(int Id)
        {
            return Ok();
        }

        [HttpPost]
        [Route("product")]
        public IHttpActionResult Create(Product p)
        {
            return Ok();
        }

        [HttpPut]
        [Route("product")]
        public IHttpActionResult Update(Product p)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("product/{Id}")]
        public IHttpActionResult Delete(int Id)
        {
            return Ok();
        }
    }
}
