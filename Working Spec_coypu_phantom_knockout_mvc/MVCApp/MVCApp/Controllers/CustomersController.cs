using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class CustomersController : Controller
    {
        //
        // GET: /Customers/

        public ActionResult Index()
        {
            var initialState = new[]
                {
                    new Customer {firstName="Andy",lastName="Andrews"},
                    new Customer {firstName="Bobby",lastName="Bones"}
                };
            return View(initialState);
        }

        //Post: /Customers/
        [HttpPost]
        public ActionResult Index( IEnumerable<Customer> customers)
        {
            return View(customers);
        }
    }
}
