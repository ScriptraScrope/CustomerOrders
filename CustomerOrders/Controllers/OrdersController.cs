using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerOrders.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrders.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult OrderForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Save(Order order)
        {
            return Json(order);
        }

    }
}