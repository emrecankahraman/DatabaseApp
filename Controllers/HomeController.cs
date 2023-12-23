using DatabaseApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace DatabaseApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly Database1Context _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(Database1Context context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    

  
        
        [HttpPost]
        public IActionResult AraCokluKriterGore(string yazar, string title, string universite, string enstitu, string dil, string danisman, string keyword, string topic)
        {
            
            var sonuclar = _context.Theses
                .Include(t => t.Author)
                    .ThenInclude(c => c.Person)
                .Include(t => t.University)
                .Include(t => t.Institute)
                .Include(t => t.Supervisor)
                    .ThenInclude(c => c.Person)
                .Include(t => t.CoSupervisor)
                    .ThenInclude(c => c.Person)
                    .Include(t => t.Keyword)
                 .Include(t => t.Topic);

            var filteredResults = FiltreleTheses(sonuclar, yazar, title, universite, enstitu, dil, danisman, keyword, topic);

            var resultList = filteredResults.ToList();
            ViewBag.AramaMetni = "Çoklu Kriterlere Göre Arama";
            return View("Index", resultList);
        }



        public static IQueryable<Thesis> FiltreleTheses(IQueryable<Thesis> sonuclar, string yazar, string title, string universite, string enstitu, string dil, string danisman,string keyword, string topic)
        {
            if (sonuclar == null)
            {


                return Enumerable.Empty<Thesis>().AsQueryable();
            }

            try
            {

                sonuclar = sonuclar.Include(t => t.Author)
                   .ThenInclude(a => a.Person)
                    .Include(t => t.University)
                    .Include(t => t.Institute)
                    .Include(t => t.CoSupervisor)
                        .ThenInclude(c => c.Person)
                    .Include(t => t.Supervisor)
                        .ThenInclude(c => c.Person)
                    .Include(t => t.Keyword)
                     .Include(t => t.Topic);

                sonuclar = sonuclar.Where(t =>
            (string.IsNullOrEmpty(yazar) || (t.Author != null && t.Author.Person != null && t.Author.Person.Name != null && t.Author.Person.Name.Contains(yazar))) &&
            (string.IsNullOrEmpty(title) || (t.Title != null && t.Title.Contains(title))) &&
            (string.IsNullOrEmpty(universite) || (t.University != null && t.University.Name != null && t.University.Name.Contains(universite))) &&
            (string.IsNullOrEmpty(enstitu) || (t.Institute != null && t.Institute.Name != null && t.Institute.Name.Contains(enstitu))) &&
            (string.IsNullOrEmpty(dil) || (t.Language != null && t.Language.Contains(dil))) &&
            (string.IsNullOrEmpty(danisman) || (t.Supervisor != null && t.Supervisor.Person != null && t.Supervisor.Person.Name != null && t.Supervisor.Person.Name.Contains(danisman))) &&
           (string.IsNullOrEmpty(keyword) ||( t.Keyword != null && t.Keyword.KeywordText != null && t.Keyword.KeywordText.Contains(keyword))) &&
            (string.IsNullOrEmpty(topic) || (t.Topic!= null && t.Topic.TopicName != null && t.Topic.TopicName.Contains(topic))) 

               
            );
                return sonuclar;
            }
            catch (Exception ex)
            {
                // Hata ayrýntýlarýný logla veya debug modunda incele
                Console.WriteLine(ex.ToString());
                return Enumerable.Empty<Thesis>().AsQueryable();
            }
        }
    }
}




