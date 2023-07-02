using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using API_Project.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Project.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {

        private readonly DatabaseContext _context;
        public OrdersController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public List<Orders> GetAll()
        {
            var orders = _context.Orders.Include(c => c.Customer).ToList();
            return orders;
        }

        [HttpGet("GetById")]
        public IActionResult GetByID(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
                return new JsonResult(order);
            return NotFound();
        }

        [HttpPost("AddOrder")]
        public IActionResult Add(Orders order)
        {
            try
             {
                _context.Orders.Add(order);
               _context.SaveChanges();
               return Ok();
             }
           catch (Exception e)
             {
              return BadRequest("error while adding Order: " + e);
           }
           // try
           // {
          //      var customer = _context.Customers.Find(customerId);
          //      if(customer != null)
          //      {
          //          var order = new Orders(title,details,price,issueDate,customerId);
          //          order.Customer = customer;
         //           _context.Orders.Add(order);
         //           _context.SaveChanges();
         //           return Ok();
        //        }
         //       return NotFound("Customer ID not Found");
         //   }
         //   catch(Exception e)
          //  {
         //       return BadRequest("error while adding Order: " + e);
          //  }
        }

        [HttpDelete("DeleteOrder")]
        public IActionResult Delete(int id)
        {
            try
            {
                var order = _context.Orders.Find(id);
                if (order != null)
                {
                    _context.Orders.Remove(order);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound("Order not found");
            }
            catch (Exception e)
            {
                return BadRequest("Error while deleting Order: " + e);
            }
        }

        [HttpPost("UpdateOrder")]
        public IActionResult Update(Orders ord)
        {
            try
            {
                var order = _context.Orders.Find(ord.Id);
                if (order != null)
                {
                    _context.Orders.Update(ord);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound("Order not found");
            }
            catch (Exception e)
            {
                return BadRequest("Error while deleting Order: " + e);
            }
        }

    }
}
