using Microsoft.AspNetCore.Mvc;
using MyProject.Data_Access;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyProject.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private MoneyApplicationDbContex contex = null;
        public CategoryApiController(MoneyApplicationDbContex con)
        {
            contex = con;
        }
        // GET: api/<CategoryApiController>
        [HttpGet]
        public IActionResult Get()
        {

            List<Category> category = (from x in contex.Category.Where(a => a.IsActive == true)
                                       select x).ToList();
            return Ok(category);
        }

        [HttpPost("search")]
        public IActionResult Search([FromBody] CatSearchDTO obj)
        {

            List<Category> category = (from x in contex.Category.Where(a => a.IsActive == true && a.CategoryName == obj.Title)
                                       select x).ToList();
            return Ok(category);
        }

        // GET api/<CategoryApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CategoryApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CategoryApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoryApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
