using PZ3_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PZ3_Project.Services
{
    public class AnimeXmlService
    {
        private readonly string _xmlFilePath;

        public AnimeXmlService(string xmlFilePath)
        {
            _xmlFilePath = xmlFilePath;
        }

        public List<Anime> ReadXmlData()
        {
            try
            {
                XDocument doc = XDocument.Load(_xmlFilePath);

                var animeList = (from anime in doc.Descendants("Anime")
                                 select new Anime
                                 {
                                     Id = Convert.ToInt32(anime.Element("Id").Value),
                                     Title = anime.Element("Title").Value,
                                     Genre = anime.Element("Genre").Value,
                                     Rating = Convert.ToInt32(anime.Element("Rating").Value),
                                     Description = anime.Element("Description")?.Value,
                                     ImageUrl = anime.Element("ImageUrl")?.Value
                                 }).ToList();

                return animeList;
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or throw as needed
                throw new Exception("Error reading XML data.", ex);
            }
        }

        public void UpdateAnime(Anime updatedAnime)
        {
            try
            {
                XDocument doc = XDocument.Load(_xmlFilePath);

                var animeElement = doc.Descendants("Anime")
                                      .Where(e => e.Element("Id").Value == updatedAnime.Id.ToString())
                                      .FirstOrDefault();

                if (animeElement != null)
                {
                    animeElement.Element("Title").Value = updatedAnime.Title;
                    animeElement.Element("Genre").Value = updatedAnime.Genre;
                    animeElement.Element("Rating").Value = updatedAnime.Rating.ToString();
                    animeElement.Element("Description").Value = updatedAnime.Description;
                    animeElement.Element("ImageUrl").Value = updatedAnime.ImageUrl;
                    doc.Save(_xmlFilePath);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or throw as needed
                throw new Exception("Error updating anime in XML data.", ex);
            }
        }
    }
}
