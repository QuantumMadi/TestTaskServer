using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestTask.DataAccess;
using TestTask.Models;

namespace TestTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PersonContext _context;
        public HomeController(ILogger<HomeController> logger, PersonContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetSplitString(string stringToSplit)
        {
            if (stringToSplit != null && stringToSplit.Length > 0)
            {
                var str = stringToSplit.Split().ToList();
                foreach (var s in str)
                {
                    s.Trim();
                }
                return Ok(str);
            }

            return StatusCode(200, "The string is empty");
        }

        [HttpGet]
        public async Task<IActionResult> GetPeople()
        {
            var people = await _context.People.ToListAsync();
            string[] peopleString = new string[people.Count];
            foreach (var person in people)
            {
                peopleString.Append($"{person.Name} {person.FamilyName} {person.FamilyName} ({person.IIN}) - {person.BirthDate}");
            }

            return Ok(peopleString);
        }

        [HttpPost]
        public async Task<IActionResult> SavePerson(Person person)
        {
            try
            {
                var check = _context.People.FirstOrDefault(it => it.IIN == person.IIN);
                if (check != null)
                    return StatusCode(200, "The person is already exists");

                person.Id = Guid.NewGuid();
                await _context.People.AddAsync(person);
                return Ok("The person has been saved");
            }
            catch
            {
                return StatusCode(500, "Internal Server error");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
