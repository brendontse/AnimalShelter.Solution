using System;
using System.Collections.Generic;
using System.Linq;
using AnimalShelter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimalShelter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private AnimalShelterContext _db;

        public AnimalsController(AnimalShelterContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Animal>> Get(int animalId, string type, string name, int age, string animalList)
        {
            var query = _db.Animals.AsQueryable();
            if (animalId != 0)
            {
                query = query.Where(w => w.AnimalId == animalId);
            }
            if (type != null)
            {
                query = query.Where(w => w.Type == type);
            }
            if (name != null)
            {
                query = query.Where(w => w.Name == name);
            }
            if (age != 0)
            {
                query = query.Where(w => w.Age == age);
            }
            if (animalList != null)
            {
                animalList = animalList.ToLower();
                string[] array = animalList.Split(",");
                List<string> animalsTable = _db.Animals.Select(w => w.Name).ToList();
 
                query = query.Where(w => array.Contains(w.Name));
            }
            return query.ToList();
        }

        [HttpPost]
        public void Post([FromBody] Animal animal)
        {

            Console.WriteLine("Post");
            List<string> animals = _db.Animals.Select(w => w.Name).ToList();
            if (animals.Contains(animal.Name))
            {
                int animalId = _db.Animals.FirstOrDefault(w => w.Name == animal.Name).AnimalId;
                animal.AnimalId = animalId;
                _db.Entry(animal).State = EntityState.Modified;
                _db.SaveChanges();
            }
            else
            {
                _db.Animals.Add(animal);
                _db.SaveChanges();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Animal> Get(int id)
        {
            return _db.Animals.FirstOrDefault(entry => entry.AnimalId == id);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Animal animal)
        {
            animal.AnimalId = id;
            _db.Entry(animal).State = EntityState.Modified;
            _db.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var animalToDelete = _db.Animals.FirstOrDefault(entry => entry.AnimalId == id);
            _db.Animals.Remove(animalToDelete);
            _db.SaveChanges();
        }

    }

}