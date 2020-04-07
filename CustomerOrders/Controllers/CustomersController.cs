using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerOrders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrders.Controllers
{
    public class CustomersController : Controller
    {
        private Database _dbContext;
        public CustomersController()
        {
            _dbContext = new Database();
        }
        public IActionResult Index()
        {
            return View(model: _dbContext.Customers.ToList());
        }
        public IActionResult Delete(int id)
        {
            var customer = _dbContext.Customers.Find(id);
            if (customer == null) 
           {
               return NotFound();
           }
            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();
            // Find method works only with the primary key. 
            return RedirectToAction("Index");
        }
        public IActionResult CreateOrder(int id)
        {
            return View("OrderForm");
        }
        public IActionResult Orders(int id)
        {
            var customer = _dbContext.Customers.Include(navigationPropertyPath: c => c.Orders).SingleOrDefault();
            if (customer == null)
            {
                return NotFound();
            }
            return View("Index",customer.Orders);
        }
        [HttpPost]
        public IActionResult SaveOrder(Order order, int customerId)
        {
            var customer = _dbContext.Customers.Find(customerId);
            if (customer == null) 
            {
                return NotFound();
            }
            order.Customer = customer;
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            
            var customer = _dbContext.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View("CustomerForm",customer);
        }
        public IActionResult CustomerForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(Customer customer)
        {
            if (customer.Id == null)
            {
                // Saving changes ot database
                _dbContext.Customers.Add(customer);
            }
            else 
            {
                var customerInDb = _dbContext.Customers.Find(customer.Id);
                customerInDb.Id = customer.Id;
                customerInDb.Email = customer.Email;
            }
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}