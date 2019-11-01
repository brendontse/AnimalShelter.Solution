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

                List<string> animalsTable = _db.animals.Select(w => w.Name).ToList();
                foreach (String animal in array)
                {
                    if (!(animalsTable.Contains(animal)))
                    {
                        Random r = new Random();
                        double randomNumber = r.Next(1,8);
                        Animal newAnimal = new Animal{Name= wo, Rating= randomNumber, RatingCount= 0};
                        _db.Add(newAnimal);
                        _db.SaveChanges();
                    }
                }
                query = query.Where(w => array.Contains(w.Name));
            }
            // if (page != 0)
            // {
            //     int animalS_PER_PAGE = 30;
            //     int NUMBER_OF_animalS = _db.animals.ToList().Count;
            //     int MAX_PAGE = (int)Math.Ceiling((double)NUMBER_OF_animalS / (double)animalS_PER_PAGE);
            //     if (page <= MAX_PAGE)
            //     {
            //         int MIN_RANGE = ((page - 1) * animalS_PER_PAGE) + 1;
            //         int RANGE = animalS_PER_PAGE - 1;
            //         if (MIN_RANGE + RANGE > NUMBER_OF_animalS && MIN_RANGE < NUMBER_OF_animalS)
            //         {
            //             RANGE = (NUMBER_OF_animalS - MIN_RANGE) + 1;
            //         }
            //         int[] RANGE_ARRAY = Enumerable.Range(MIN_RANGE, RANGE).ToArray();
            //         query = query.Where(w => RANGE_ARRAY.Contains(w.animalId));
            //     }
            // }
            return query.ToList();
        }

        [HttpPost]
        public void Post([FromBody] animal animal)
        {

            Console.WriteLine("Post");
            List<string> animals = _db.animals.Select(w => w.Name).ToList();
            if (animals.Contains(animal.Name))
            {
                int animalId = _db.animals.FirstOrDefault(w => w.Name == animal.Name).animalId;
                animal.animalId = animalId;
                _db.Entry(animal).State = EntityState.Modified;
                _db.SaveChanges();
            }
            else
            {
                _db.animals.Add(animal);
                _db.SaveChanges();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<animal> Get(int id)
        {
            return _db.animals.FirstOrDefault(entry => entry.animalId == id);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] animal animal)
        {
            animal.animalId = id;
            _db.Entry(animal).State = EntityState.Modified;
            _db.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var animalToDelete = _db.animals.FirstOrDefault(entry => entry.animalId == id);
            _db.animals.Remove(animalToDelete);
            _db.SaveChanges();
        }

    }

}