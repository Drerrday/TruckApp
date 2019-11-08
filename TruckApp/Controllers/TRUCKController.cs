using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckApp.Models;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace TruckApp.Controllers
{
    public class TRUCKController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var repo = new TRUCKRepository();
            var TRUCKS = repo.GetAllTRUCKS();
            return View(TRUCKS);
        }
        public IActionResult ViewTRUCK(int id)
        {
            var repo = new TRUCKRepository();
            var truck = repo.GetTruck(id);
            if (repo == null)
            {
                return View("TruckNotFound");
            }
            return View(truck);
        }
    }
}