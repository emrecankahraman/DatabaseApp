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
    public class Upgradecontroller : Controller
    {
        private readonly Database1Context _context;
        private readonly ILogger<HomeController> _logger;

        public Upgradecontroller(Database1Context context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult UpgradeThesis()
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
            return View("UpgradeThesis", resultList);
        }



        public static IQueryable<Thesis> FiltreleTheses(IQueryable<Thesis> sonuclar, string yazar, string title, string universite, string enstitu, string dil, string danisman, string keyword, string topic)
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
           (string.IsNullOrEmpty(keyword) || (t.Keyword != null && t.Keyword.KeywordText != null && t.Keyword.KeywordText.Contains(keyword))) &&
            (string.IsNullOrEmpty(topic) || (t.Topic != null && t.Topic.TopicName != null && t.Topic.TopicName.Contains(topic)))


            );
                return sonuclar;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Enumerable.Empty<Thesis>().AsQueryable();
            }
        }

        [HttpPost]
        public IActionResult UpgradeThesis(int id, string selectedValue, string newValue)
        {
            var thesisToUpdate = _context.Theses
                .Include(t => t.University)
                .Include(t => t.Institute)
                .Include(t => t.Supervisor)
                            .ThenInclude(s => s.Person)
                .Include(t => t.CoSupervisor)
                .Include(t => t.Keyword)
                .Include(t => t.Topic)
                .FirstOrDefault(t => t.ThesisNumber == id);

            if (thesisToUpdate == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(selectedValue) && !string.IsNullOrEmpty(newValue))
            {
                switch (selectedValue)
                {
                    case "University":
                        var existingUniversity = _context.Universities.FirstOrDefault(u => u.Name == newValue);

                        if (existingUniversity == null)
                        {
                            existingUniversity = new University { Name = newValue };
                            _context.Universities.Add(existingUniversity);
                            _context.SaveChanges(); 

                            thesisToUpdate.UniversityId = existingUniversity.UniversityId;
                        }
                        else
                        {
                            _context.Entry(existingUniversity).State = EntityState.Detached;

                            thesisToUpdate.UniversityId = existingUniversity.UniversityId;
                        }
                        break;
                    case "Keyword":
                        var existingKeyword = _context.Keywords.FirstOrDefault(k => k.KeywordText == newValue);

                        if (existingKeyword == null)
                        {
                            existingKeyword = new Keyword { KeywordText = newValue };
                            _context.Keywords.Add(existingKeyword);
                            _context.SaveChanges();

                            thesisToUpdate.KeywordId = existingKeyword.KeywordId;
                        }
                        else
                        {
                            _context.Entry(existingKeyword).State = EntityState.Detached;
                            thesisToUpdate.KeywordId = existingKeyword.KeywordId;
                        }
                        break;
                    case "Topic":
                        var existingTopic = _context.SubjectTopics.FirstOrDefault(t => t.TopicName == newValue);

                        if (existingTopic == null)
                        {
                            existingTopic = new SubjectTopic { TopicName = newValue };
                            _context.SubjectTopics.Add(existingTopic);
                            _context.SaveChanges();

                            thesisToUpdate.TopicId = existingTopic.TopicId;
                        }
                        else
                        {
                            _context.Entry(existingTopic).State = EntityState.Detached;
                            thesisToUpdate.TopicId = existingTopic.TopicId;
                        }
                        break;
                    case "Institute":
                        var existingInstitute = _context.Institutes.FirstOrDefault(i => i.Name == newValue);

                        if (existingInstitute == null)
                        {
                            existingInstitute = new Institute { Name = newValue };
                            _context.Institutes.Add(existingInstitute);
                            _context.SaveChanges();

                            thesisToUpdate.InstituteId = existingInstitute.InstituteId;
                        }
                        else
                        {
                            _context.Entry(existingInstitute).State = EntityState.Detached;
                            thesisToUpdate.InstituteId = existingInstitute.InstituteId;
                        }
                        break;
                    case "Supervisor":
                        var existingSupervisor = _context.Supervisors
                            .Include(s => s.Person)
                            .FirstOrDefault(s => s.Person != null && s.Person.Name == newValue);

                        if (existingSupervisor == null)
                        {
                            var newPerson = new Person { Name = newValue };    
                            var newSupervisor = new Supervisor { Person = newPerson }; 
                            _context.Supervisors.Add(newSupervisor);
                            _context.SaveChanges();

                            thesisToUpdate.SupervisorId = newSupervisor.SupervisorId;
                        }
                        else
                        {
                            thesisToUpdate.SupervisorId = existingSupervisor.SupervisorId;
                        }
                        break;
                    case "CoSupervisor":
                        var existingCoSupervisor = _context.CoSupervisors
                            .Include(c => c.Person)
                            .FirstOrDefault(c => c.Person != null && c.Person.Name == newValue);

                        if (existingCoSupervisor == null)
                        {
                            var newPerson = new Person { Name = newValue };
                            var newCoSupervisor = new CoSupervisor { Person = newPerson };
                            _context.CoSupervisors.Add(newCoSupervisor);
                            _context.SaveChanges();

                            thesisToUpdate.CoSupervisorId = newCoSupervisor.CoSupervisorId;
                        }
                        else
                        {
                            thesisToUpdate.CoSupervisorId = existingCoSupervisor.CoSupervisorId;
                        }
                        break;
                   

                    default:
                        ModelState.AddModelError(string.Empty, "Geçersiz bir değer seçildi.");
                        return View("UpgradeThesis", thesisToUpdate);
                }

                try
                {
                    _context.SaveChanges();
                    return RedirectToAction("UpgradeThesis", "thesisToUpdate");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError(string.Empty, "Güncelleme işlemi sırasında bir hata oluştu: " + ex.Message);
                    return View("UpgradeThesis", thesisToUpdate);
                }
            }

            return View("UpgradeThesis", thesisToUpdate);
        }

    }
}




