using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using csharp_login.Models;
using DbConnection;

namespace csharp_login.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbConnector _dbConnector;

        public HomeController(DbConnector connect)
        {
            _dbConnector = connect;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/process")]
        public IActionResult Process(string firstName, string lastName, string email, string password)
        {
            User newUser = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            if (TryValidateModel(newUser) == false)
            {
                ViewBag.ModelFields = ModelState.Values;
                return View();
            }

            else
            {
                string query = $"INSERT INTO csharp_users (first_name, last_name, email, password, created_at, updated_at) VALUES ('{firstName}', '{lastName}', '{email}', '{password}', NOW(), NOW())";
                DbConnector.Execute(query);
                return RedirectToAction("Success");
            }
        }
        [HttpGet]
        [Route("/success")]
        public IActionResult Success()

        {
            Console.WriteLine("entered Success");
            return View();
        }
    }
}