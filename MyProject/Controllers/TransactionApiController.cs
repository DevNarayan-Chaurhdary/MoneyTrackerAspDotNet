using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.Data_Access;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionApiController : ControllerBase
    {
         private MoneyApplicationDbContex contex = null;
        public TransactionApiController(MoneyApplicationDbContex con)
        {
            contex = con;
        }
        // GET: api/<TransactionApiController>
        [HttpGet]
        public IActionResult Get()
        {

            List<TableData> data = (from t in contex.Transactions.Where(a=>a.IsActive == true)
                        join c in contex.Category
                        on t.Category.CategoryId equals c.CategoryId
                        join p in contex.PaymentMode
                        on t.Payment.PaymentModeId equals p.PaymentModeId
                        orderby t.TxnDate descending
                        select new TableData
                        {
                            TransactionId = t.TransactionId,
                            Category = c.CategoryName,
                            TDate = t.TxnDate,
                            PMode = p.PaymentsMode,
                            Description = t.Description,
                            Amount = t.Amount,
                            TransactionType = t.TransactionType
                        }).ToList();

            return Ok(data);
        }


        [HttpGet("header")]
        public IActionResult GetData()
        {
        
            HeaderDTO header = new HeaderDTO();
            var result= (from x in contex.Transactions.Where(e=>e.IsActive == true)
                                      group x.Amount by x.TransactionType into temp
                                      select new 
                                      {
                                          type=temp.Key, Total=temp.Sum()
                                      }).ToList();
          
            header.TotalExpense = result[0].Total;
            header.TotalIncome = result[1].Total;
            header.TotalTxn = contex.Transactions.Where(e=>e.IsActive==true).Count();

            return Ok(header);
        }

        [HttpGet("catList")]
        public IActionResult GetList()
        {
      
            var data= (from x in contex.Category.Where(a => a.IsActive == true) 
                                  select new { x.CategoryName }).ToList();
            CatList list = new CatList();
         
            return Ok(data);
        }


        [HttpPost("search")]
        public IActionResult Search( [FromBody] SearchDTO obj)
        {

            List<TableData> data;
            if (obj.Title == "Category")
            {
                data = (from t in contex.Transactions.Where(a=>a.IsActive == true)
                        join c in contex.Category.Where(a=>a.CategoryName == obj.Value)
                        on t.Category.CategoryId equals c.CategoryId
                        join p in contex.PaymentMode
                        on t.Payment.PaymentModeId equals p.PaymentModeId
                        orderby t.TxnDate descending
                        select new TableData
                        {
                            TransactionId = t.TransactionId,
                            Category = c.CategoryName,
                            TDate = t.TxnDate,
                            PMode = p.PaymentsMode,
                            Description = t.Description,
                            Amount = t.Amount,
                            TransactionType = t.TransactionType
                        }).ToList();

            }
            else
            {
                data = (from t in contex.Transactions.Where(a=>a.IsActive == true)
                        join c in contex.Category
                        on t.Category.CategoryId equals c.CategoryId
                        join p in contex.PaymentMode.Where(a=>a.PaymentsMode == obj.Value)
                        on t.Payment.PaymentModeId equals p.PaymentModeId
                        orderby t.TxnDate descending
                        select new TableData
                        {
                            TransactionId = t.TransactionId,
                            Category = c.CategoryName,
                            TDate = t.TxnDate,
                            PMode = p.PaymentsMode,
                            Description = t.Description,
                            Amount = t.Amount,
                            TransactionType = t.TransactionType
                        }).ToList();

            }
          

            return Ok(data);
        }


        //[HttpGet("secondaryData")]
        //public IActionResult get()
        //{

        //}

        // GET api/<TransactionApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TransactionApiController>
        [HttpPost]
        public IActionResult Post([FromBody] TransactionInputDTO obj)
        {
            var vcontex = new ValidationContext(obj, null, null);
            var result = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(obj, vcontex, result, true);
            if (result.Count == 0)
            {

                Category c = (from x in contex.Category
                              where x.CategoryName == obj.Category
                              select x).Single();

                PaymentMode m = (from y in contex.PaymentMode
                          where y.PaymentsMode == obj.PaymentMode
                          select y).Single();

                Transaction t = new Transaction();
                t.TxnDate = obj.TxnDate;
                t.Description = obj.Description;
                t.Amount = obj.Amount;
                t.TransactionType = obj.TransactionType;
                DateTime date = DateTime.Now;
                t.CreatedOn = date;
                t.IsActive = obj.IsActive;
                t.Category = c;
                t.Payment = m;
                contex.Transactions.Add(t);
                contex.SaveChanges();

                return Content("Successfully Transaction Inserted");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }

        // PUT api/<TransactionApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] TransactionInputDTO obj)
        {

            try
            {
 
                Transaction t = contex.Transactions.Where(x => x.TransactionId == id).Single<Transaction>();

                Category c = (from x in contex.Category
                              where x.CategoryName == obj.Category
                              select x).Single();

                PaymentMode m = (from y in contex.PaymentMode
                                 where y.PaymentsMode == obj.PaymentMode
                                 select y).Single();

                t.TxnDate = obj.TxnDate;
                t.Description = obj.Description;
                t.Amount = obj.Amount;
                t.TransactionType = obj.TransactionType;
                DateTime date = DateTime.Now;
                t.CreatedOn = date;
                t.IsActive = true;
                t.Category = c;
                t.Payment = m;
                contex.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // DELETE api/<TransactionApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

            try
            {
                Transaction t = contex.Transactions.Where(x => x.TransactionId == id).Single<Transaction>();
                t.IsActive = false;
                contex.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
