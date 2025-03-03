
using Microsoft.AspNetCore.Mvc;
using CrazyMusiciansAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace CrazyMusiciansAPI.Controllers
{
    [Route("api/musicians")] // API controller olduğunu belirtir.
    [ApiController]
    public class MusiciansController : ControllerBase
    {
       
        private static List<Musician> musicians = new List<Musician>
        {
            new Musician { Id = 1, Name = "Ahmet Çalgı", Profession = "Famous Player", FunFeature = "Always plays wrong notes but very fun" },
            new Musician { Id = 2, Name = "Zeynep Melodi", Profession = "Popular Melody Writer", FunFeature = "Songs are often misunderstood but very popular" },
            new Musician { Id = 3, Name = "Cemil Akor", Profession = "Crazy Guitarist", FunFeature = "Frequently changes chords but surprisingly talented" }
        };

        // GET: api/musicians (Tüm müzisyenleri getirir / Retrieve all musicians)
        [HttpGet]
        public ActionResult<IEnumerable<Musician>> GetAll()
        {
            return Ok(musicians);
        }

        // GET: api/musicians/{id} (Belirli bir müzisyeni getirir / Retrieve a single musician by ID)
        [HttpGet("{id}")]
        public ActionResult<Musician> GetById(int id)
        {
            var musician = musicians.FirstOrDefault(m => m.Id == id);
            if (musician == null) return NotFound();
            return Ok(musician);
        }

        // GET: api/musicians/search?name=Ahmet (İsimle arama yapar / Search by name using [FromQuery])
        [HttpGet("search")]
        public ActionResult<IEnumerable<Musician>> SearchByName([FromQuery] string name)
        {
            var result = musicians.Where(m => m.Name.ToLower().Contains(name.ToLower())).ToList();
            return Ok(result);
        }

        // POST: api/musicians (Yeni müzisyen ekler / Create a new musician)
        [HttpPost]
        public ActionResult<Musician> Create(Musician musician)
        {
            musician.Id = musicians.Max(m => m.Id) + 1;
            musicians.Add(musician);
            return CreatedAtAction(nameof(GetById), new { id = musician.Id }, musician);
        }

        // PUT: api/musicians/{id} (Mevcut müzisyeni tamamen günceller / Update an existing musician)
        [HttpPut("{id}")]
        public ActionResult Update(int id, Musician updatedMusician)
        {
            var musician = musicians.FirstOrDefault(m => m.Id == id);
            if (musician == null) return NotFound();

            musician.Name = updatedMusician.Name;
            musician.Profession = updatedMusician.Profession;
            musician.FunFeature = updatedMusician.FunFeature;
            return NoContent();
        }

        // PATCH: api/musicians/{id} (Müzisyenin belirli bir alanını günceller / Partially update a musician)
        [HttpPatch("{id}")]
        public ActionResult PartialUpdate(int id, [FromBody] string funFeature)
        {
            var musician = musicians.FirstOrDefault(m => m.Id == id);
            if (musician == null) return NotFound();
            musician.FunFeature = funFeature;
            return NoContent();
        }

        // DELETE: api/musicians/{id} (Belirtilen müzisyeni siler / Delete a musician)
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var musician = musicians.FirstOrDefault(m => m.Id == id);
            if (musician == null) return NotFound();
            musicians.Remove(musician);
            return NoContent();
        }
    }
}
