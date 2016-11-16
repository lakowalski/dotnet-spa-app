using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DotnetApp.Models;

namespace DotnetApp.Controllers
{
    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        private readonly ProfileContext _context;

        public ProfileController(ProfileContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Profile> GetAll()
        {
            return _context.Profiles.ToList();
        }

        [HttpGet("{name}", Name = "GetProfile")]
        public IActionResult GetByName(string name)
        {
            var item = _context.Profiles.FirstOrDefault(o => o.Name == name);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Profile item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _context.Profiles.Add(item);
            _context.SaveChanges();
            return CreatedAtRoute("GetProfile", new { controller = "Profile", name = item.Name }, item);
        }

        [HttpDelete("{name}")]
        public void Delete(string name)
        {
            var item = _context.Profiles.FirstOrDefault(o => o.Name == name);
            _context.Profiles.Remove(item);
            _context.SaveChanges();
        }
    }
}