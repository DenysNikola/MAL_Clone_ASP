using Microsoft.AspNetCore.Mvc;
using PZ3_Project.Models;
using PZ3_Project.Services;
using System.Xml.Linq;

namespace PZ3_Project.Controllers
{
    public class AnimeController : Controller
    {

        private readonly AnimeXmlService _animeXmlService;

        public AnimeController(AnimeXmlService animeXmlService)
        {
            _animeXmlService = animeXmlService;
        }
        public ActionResult Index()
        {
            // Read XML data and pass it to the view
            var animeList = _animeXmlService.ReadXmlData();
            return View(animeList);
        }


        public IActionResult AnimeDetails(int id)
        {
            // Retrieve the selected anime by Id from your service
            var selectedAnime = _animeXmlService.ReadXmlData().FirstOrDefault(a => a.Id == id);

            if (selectedAnime == null)
            {
                return NotFound(); // Handle the case where the anime is not found
            }

            return View(selectedAnime);
        }

        [HttpPost]
        public IActionResult Edit(Anime updatedAnime)
        {
            // Update the anime details in the XML file
            _animeXmlService.UpdateAnime(updatedAnime);

            // Redirect to the details page after editing
            return RedirectToAction("AnimeDetails", new { id = updatedAnime.Id });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Retrieve the selected anime by Id from your service
            var selectedAnime = _animeXmlService.ReadXmlData().FirstOrDefault(a => a.Id == id);

            if (selectedAnime == null)
            {
                return NotFound(); // Handle the case where the anime is not found
            }

            return View(selectedAnime);
        }
    }
}
