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
        public IActionResult SearchByMultipleCriteria(string author, string title, string university, string institute, string language, string supervisor, string keyword, string topic)
        {
            var results = _context.Theses
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

            var filteredResults = filterThesis(results, author, title, university, institute, language, supervisor, keyword, topic);

            var resultList = filteredResults.ToList();
            ViewBag.Search = "Search by Multiple Criteria";
            return View("Index", resultList);
        }



        public static IQueryable<Thesis> filterThesis(IQueryable<Thesis> results, string author, string title, string university, string institute, string language, string supervisor, string keyword, string topic)
        {
            if (results == null)
            {


                return Enumerable.Empty<Thesis>().AsQueryable();
            }

            try
            {
                results = results.Where(t =>
            (string.IsNullOrEmpty(author) || (t.Author.Person.Name.Contains(author))) &&
            (string.IsNullOrEmpty(title) || (t.Title.Contains(title))) &&
            (string.IsNullOrEmpty(university) || (t.University.Name.Contains(university))) &&
            (string.IsNullOrEmpty(institute) || (t.Institute.Name.Contains(institute))) &&
            (string.IsNullOrEmpty(language) || (t.Language.Contains(language))) &&
            (string.IsNullOrEmpty(supervisor) || (t.Supervisor.Person.Name.Contains(supervisor))) &&
            (string.IsNullOrEmpty(keyword) ||( t.Keyword.KeywordText.Contains(keyword))) &&
            (string.IsNullOrEmpty(topic) || ( t.Topic.TopicName.Contains(topic))) 

               
            );
                return results;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Enumerable.Empty<Thesis>().AsQueryable();
            }
        }
    }
}




