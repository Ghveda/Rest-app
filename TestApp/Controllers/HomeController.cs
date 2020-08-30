using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestApp.DB;
using TestApp.DB.models;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PostgreDB _postgreDB;


        public HomeController(ILogger<HomeController> logger, PostgreDB postgreDB)
        {
            _logger = logger;
            _postgreDB = postgreDB;
        }
        public IActionResult Main()
        {
            return View();
        }
        public IActionResult DeletePerson(int id)
        {
            bool deleted = false;
            try
            {
                var x = _postgreDB.Persons.FirstOrDefault(i => i.Id == id);
                if (x != null)
                {
                    _postgreDB.Persons.Remove(x);
                    _postgreDB.SaveChanges();
                    deleted = true;

                }
            }
            catch (Exception)
            {
                deleted = false;
            }

            return Json(new { status = deleted });
        }

        public IActionResult SearchPerson()
        {
            return View();
        }
        public IActionResult EditPerson(int id)

        {

            try
            {
                var x = _postgreDB.Persons.FirstOrDefault(i => i.Id == id);

                return Json(new { Person = x });
            }
            catch (Exception)
            {

                return Json(new { });
            }

        }


        public IActionResult Index()
        {
            var personList = _postgreDB.Persons.Include(i => i.Gender).Include(i => i.Country).ToList();

            List<PersonViewModel> persons = personList.Select(i => new PersonViewModel
            {
                Name = i.Name,
                Surname = i.Surname,
                Birthdate = i.Birthdate,
                Pin = i.Pin,
                Mobile = i.Mobile,
                Website=i.Website,
                CountryString=i.Country?.Value,
                Email = i.Email,
                GenderId = i.GenderId,
                CountryId=i.CountryId,
                Id = i.Id,
                GenderString = i.Gender?.Value
            }).ToList();
            return View(persons);
        }
        [HttpPost]
        public IActionResult CreatPerson(PersonViewModel model)
        {
            if (ModelState.IsValid)
            {


                Person person = new Person
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Pin = model.Pin,
                    Mobile = model.Mobile,
                    Email = model.Email,
                    Website = model.Website,
                    CountryId = model.CountryId,
                    GenderId = model.GenderId,
                    Birthdate = model.Birthdate,


                };

                _postgreDB.Persons.Add(person);
                _postgreDB.SaveChanges();
            }
            return RedirectToAction("index");
        }
        public IActionResult UpdatePerson(PersonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var person = _postgreDB.Persons.FirstOrDefault(i => i.Id == model.Id);

                person.Name = model.Name;
                person.Surname = model.Surname;
                person.Pin = model.Pin;
                person.Mobile = model.Mobile;
                person.Email = model.Email;
                person.Website = model.Website;
                person.CountryId = model.CountryId;
                person.GenderId = model.GenderId;
                person.Birthdate = model.Birthdate;
                _postgreDB.SaveChanges();
            }
            return RedirectToAction("index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        public IActionResult OrderChange(string name, string search )
        {
            if(search==null)
            {
                search = "";
            }
            
            List<PersonViewModel> persons = _postgreDB.Persons.Include(i => i.Gender).Include(i => i.Country).Where(i => i.Name.Contains(search) || i.Surname.Contains(search) || i.Pin.Contains(search) || i.Mobile.Contains(search) || i.Email.Contains(search) || i.Website.Contains(search) || i.Gender.Value.Contains(search) || i.Country.Value.Contains(search)).Select(i => new PersonViewModel
            {
                Id = i.Id,
                Birthdate = i.Birthdate,
                Name = i.Name,
                Surname = i.Surname,
                Pin = i.Pin,
                Mobile = i.Mobile,
                Email = i.Email,
                Website = i.Website,
                CountryString = i.Country.Value,
                GenderString = i.Gender.Value
            }).ToList();


            switch (name)
            {
                case "nameAsc":
                   persons= persons.OrderBy(i => i.Name).ToList();
                    break;

                case "nameDesc":
                    persons=persons.OrderByDescending(i => i.Name).ToList();
                    break;

                case "surnameAsc":
                   persons= persons.OrderBy(i => i.Surname).ToList();
                    break;
                case "surnameDesc":
                    persons=persons.OrderByDescending(i => i.Surname).ToList();
                    break;
                
            }
            return PartialView("~/Views/Home/personTablePartial.cshtml", persons);
        }
    }
}
