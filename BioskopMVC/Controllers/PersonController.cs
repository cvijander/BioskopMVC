using BioskopMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BioskopMVC.Controllers
{
    public class PersonController : Controller
    {
        // lista osoba 
        private static List<Person> people = new List<Person>();


        //GET person 
        public IActionResult Index()
        {
            return View(people);
        }


        // GET Person / Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Person / Create  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("PersonId, FirstName, LastName, DateOfBirth,NationalityId")] Person person)
        {
            if (ModelState.IsValid)
            {
                // dodavanje nove osobe u listu
                people.Add(person);
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }



        // GET:  Person/Edit 
        public IActionResult Edit(int id)
        {
            var person = people.FirstOrDefault(p => p.PersonId == id);
            if(person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST:  Person/Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("PersonId, FirstName, LastName,DateOfBirth,Nationality")] Person person)
        {
            if (ModelState.IsValid)
            {
                //cupanje prema id 
                var existingPerson = people.FirstOrDefault(p => p.PersonId == id);

                // ako ne postoji 
                if (existingPerson == null)
                {
                    return NotFound();
                }

                // Azuriranje  podataka 
                existingPerson.FirstName = person.FirstName;
                existingPerson.LastName = person.LastName;
                existingPerson.DateOfBirth = person.DateOfBirth;
                existingPerson.NationalityId = person.NationalityId;

                return RedirectToAction(nameof(Index));
            }

            return View(person);
        }

        // GET  Person /Delete 
        public IActionResult Delete(int id)
        {
            var person = people.FirstOrDefault(p => p.PersonId == id);
            if(person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST Person / Delete 
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var person = people.FirstOrDefault(p => p.PersonId == id);

            // osoba je pronadjena i brisemo je 
            if(person != null)
            {
                people.Remove(person);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
