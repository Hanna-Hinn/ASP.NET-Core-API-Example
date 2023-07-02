using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using API_Project.Entities;
using API_Project.ViewModels;

namespace API_Project.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly DatabaseContext _context;
        public CustomerController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public List<Customers> GetAll()
        {
            var customers = _context.Customers.ToList();
            return customers;
        }

        [HttpGet("GetById")]
        public IActionResult GetByID(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
                return new JsonResult(customer);
            return NotFound();
        }

        [HttpPost("AddCustomer")]
        public IActionResult Add(Customers customer)
        {
            try
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("error while adding customer: " + e);
            }
        }

        [HttpDelete("DeleteCustomer")]
        public IActionResult Delete(int id)
        {
            try
            {
                var customer = _context.Customers.Find(id);
                if (customer != null)
                {
                    _context.Customers.Remove(customer);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound("Customer not found");
            }
            catch (Exception e)
            {
                return BadRequest("Error while deleting customer: " + e);
            }
        }

        [HttpPost("UpdateCustomer")]
        public IActionResult Update(Customers cust)
        {
            try
            {
                var customer = _context.Customers.Find(cust.Id);
                if (customer != null)
                {
                    _context.Customers.Update(cust);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound("Customer not found");
            }
            catch (Exception e)
            {
                return BadRequest("Error while deleting customer: " + e);
            }
        }

        [HttpGet("CustomGetAll")]
        public List<CustomerVM> CustomGetAll()
        {
            var customers = _context.Customers.ToList();
            List<CustomerVM> customerlist = new List<CustomerVM>();
            foreach(var item in customers){
                var vm = new CustomerVM()
                {
                    Name = item.Name,
                    Id = item.Id
                };
                customerlist.Add(vm);
            }
            return customerlist;
        }



    }
}
