using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

	    // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            List<Dish> AllUsers = dbContext.Dishes.ToList();
            // System.Console.WriteLine(AllUsers[0].Chef);
            return View(AllUsers);
        }

        [Route("new")]
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [Route("newdish")]
        [HttpPost]
        public RedirectToActionResult NewDish(Dish dish)
        {
            dbContext.Add(dish);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult View(int id)
        {
            Dish dish = dbContext.Dishes.FirstOrDefault(i => i.Id == id);
            // List<Dish> dish = dbContext.Dishes
            //     .Where(i => i.Id == id)
            //     .ToList();
            return View(dish);
        }

        [Route("delete/{id}")]
        [HttpGet]
        public RedirectToActionResult Delete(int id)
        {
            Dish dish = dbContext.Dishes.SingleOrDefault(i => i.Id == id);
            dbContext.Dishes.Remove(dish);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("update/{id}")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            Dish dish = dbContext.Dishes.FirstOrDefault(i => i.Id == id);
            return View(dish);
        }

        [Route("edit/{id}")]
        [HttpPost]
        public RedirectToActionResult Edit(Dish dish, int id)
        {
            Dish thisdish = dbContext.Dishes.FirstOrDefault(i => i.Id == id);
            thisdish.Name = dish.Name;
            thisdish.Chef = dish.Chef;
            thisdish.Description = dish.Description;
            thisdish.Tastiness = dish.Tastiness;
            thisdish.Calories = dish.Calories;
            dbContext.SaveChanges();
            return RedirectToAction("View", new {thisdish.Id});
        }
    }
}
