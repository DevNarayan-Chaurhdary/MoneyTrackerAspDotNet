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
    public class PaymentApiController : ControllerBase
    {
         private MoneyApplicationDbContex contex = null;
        public PaymentApiController(MoneyApplicationDbContex con)
        {
            contex = con;
        }
        // GET: api/<PaymentApiController>
        [HttpGet]
        public IActionResult Get()
        {

            List<PaymentMode> payment = (from x in contex.PaymentMode.Where(a => a.IsActive == true)
                                         select x).ToList();
            return Ok(payment);
        }
        [HttpPost("search")]
        public IActionResult Search([FromBody] CatSearchDTO obj)
        {

            List<PaymentMode> category = (from x in contex.PaymentMode.Where(a => a.IsActive == true && a.PaymentsMode == obj.Title)
                                       select x).ToList();
            return Ok(category);
        }

        // GET api/<PaymentApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PaymentApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PaymentApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PaymentApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
