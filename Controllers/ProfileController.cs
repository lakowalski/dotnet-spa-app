using DotnetApp.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DotnetApp.Controllers
{
    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        private static List<Profile> _profiles;
        static ProfileController()
        {
            _profiles = new List<Profile>{
                new Profile {
                    Id = 1,
                    Name = "marilyn",
                    FullName = "Marylin Monroe",
                    ThumbUrl = "https://secure.static.tumblr.com/8d9dd352d0869b6d6422a1e58eb392c7/uj2pgt4/Gtwnhm0z5/tumblr_static_8sgbrouj5x4w4gww4k8kos480_640_v2.jpg",
                    CoverUrl = "http://thiswallpaper.com/cdn/hdwallpapers/380/black%20and%20white%20marilyn%20monroe%20desktop%20background%20wallpaper.jpeg",
                    City = "Poznan, PL",
                    FlickrTags = "Marylin",
                    Description = "<p>Lorem ipsum dolor sit amet, qui cu eligendi expetenda. Quaeque sanctus iudicabit ei usu, laboramus consetetur mea ad. Ius at nobis dicam reprehendunt, illud commune democritum at usu, ancillae eleifend pertinacia has in. Alii alienum cum ad. Quaeque accusamus prodesset his at, eu simul soleat constituam mea, ocurreret torquatos ex mea.</p><blockquote>Cu vel dicunt euismod petentium, an dico graeci qui. Ne iudico nostrud percipitur mel, eum doming sensibus an. Id alia verterem sententiae cum, et dolor definitionem mea. Consetetur persequeris id pri, eu mei virtute legendos mediocritatem. Eum ne impedit tincidunt. Vel an mazim omnesque conceptam.</blockquote><p>Commodo iracundia constituto ei vix, at aeque saepe partem cum. Meis appareat sit in. Vel te probo nusquam vituperatoribus, ea vero appareat guber    gren usu. Eu his elit summo. Te vis falli dolor denique, facilisi repudiare dissentiet est ei.</p>"
                },
                new Profile {
                    Id = 2,
                    Name = "madonna",
                    FullName = "Madonna",
                    ThumbUrl = "http://vignette1.wikia.nocookie.net/twoja-twarz-brzmi-znajomo/images/7/7a/Screen-shot-2014-12-17-at-10-44-55-am.png/revision/latest?cb=20150702130142&path-prefix=pl",
                    CoverUrl = "http://wallpapercave.com/wp/WtjD3bt.jpg",
                    City = "Paris, FR",
                    FlickrTags = "Madonna,Singer",
                    Description = "<p>Lorem ipsum dolor sit amet, qui cu eligendi expetenda. Quaeque sanctus iudicabit ei usu, laboramus consetetur mea ad. Ius at nobis dicam reprehendunt, illud commune democritum at usu, ancillae eleifend pertinacia has in. Alii alienum cum ad. Quaeque accusamus prodesset his at, eu simul soleat constituam mea, ocurreret torquatos ex mea.</p><blockquote>Cu vel dicunt euismod petentium, an dico graeci qui. Ne iudico nostrud percipitur mel, eum doming sensibus an. Id alia verterem sententiae cum, et dolor definitionem mea. Consetetur persequeris id pri, eu mei virtute legendos mediocritatem. Eum ne impedit tincidunt. Vel an mazim omnesque conceptam.</blockquote><p>Commodo iracundia constituto ei vix, at aeque saepe partem cum. Meis appareat sit in. Vel te probo nusquam vituperatoribus, ea vero appareat gubergren usu. Eu his elit summo. Te vis falli dolor denique, facilisi repudiare dissentiet est ei.</p>"
                }
            };
        }

        [HttpGet]
        public IEnumerable<Profile> GetAll()
        {
            return _profiles.AsReadOnly();
        }

        [HttpGet("{name}", Name = "GetProfile")]
        public IActionResult GetByName(string name)
        {
            var item = _profiles.Find(n => n.Name == name);
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
            item.Id = (_profiles.Count + 1);
            _profiles.Add(item);
            return CreatedAtRoute("GetProfile", new { controller = "Profile", id = item.Id }, item);
        }

        [HttpDelete("{name}")]
        public void Delete(string name)
        {
            _profiles.RemoveAll(n => n.Name == name);
        }
    }
}