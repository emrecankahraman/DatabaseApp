using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DatabaseApp.Models;

public class CreateController : Controller
{
    private readonly Database1Context _context;

    public CreateController(Database1Context context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(string supervisorName, string coSupervisorName,string AuthorName,string keywordText,string topicName, string universityName, string instituteName, Thesis thesis)
    {
        if (ModelState.IsValid)
        {
            var supervisor = _context.Supervisors.Include(s => s.Person)
                .FirstOrDefault(s => s.Person.Name == supervisorName);

            if (supervisor == null)
            {
                var newSupervisorPerson = new Person { Name = supervisorName };
                _context.People.Add(newSupervisorPerson);
                _context.SaveChanges();

                supervisor = new Supervisor { PersonId = newSupervisorPerson.PersonId };
                _context.Supervisors.Add(supervisor);
                _context.SaveChanges();
            }

            var coSupervisor = _context.CoSupervisors.Include(cs => cs.Person)
                .FirstOrDefault(cs => cs.Person.Name == coSupervisorName);

            if (coSupervisor == null)
            {
                var newCoSupervisorPerson = new Person { Name = coSupervisorName };
                _context.People.Add(newCoSupervisorPerson);
                _context.SaveChanges();

                coSupervisor = new CoSupervisor { PersonId = newCoSupervisorPerson.PersonId };
                _context.CoSupervisors.Add(coSupervisor);
                _context.SaveChanges();
            }


            var Author= _context.Authors.Include(s => s.Person)
                .FirstOrDefault(a => a.Name == AuthorName);

            if (Author == null)
            {
                var newAuthorPerson = new Person { Name = AuthorName };
                _context.People.Add(newAuthorPerson);
                _context.SaveChanges();
                var newAuthor = new Author
                {
                    Name = AuthorName,
                    PersonId = newAuthorPerson.PersonId
                };

                _context.Authors.Add(newAuthor);
                _context.SaveChanges();
                Author = newAuthor;

            }
            else
            {
                if (Author.Person == null)
                {
                    var newAuthorPerson = new Person { Name = AuthorName };
                    _context.People.Add(newAuthorPerson);
                    _context.SaveChanges();

                    Author.PersonId = newAuthorPerson.PersonId;
                    Author.Person = newAuthorPerson;
                }
                else
                {
                    Author.Person.Name = AuthorName; 
                }

                Author.Name = AuthorName; 
                _context.SaveChanges();
            }
            var keyword = _context.Keywords.FirstOrDefault(k => k.KeywordText == keywordText);
            if (keyword == null)
            {
                var newKeyword = new Keyword { KeywordText = keywordText };
                _context.Keywords.Add(newKeyword);
                _context.SaveChanges();
                keyword = newKeyword;
            }

            var topic = _context.SubjectTopics.FirstOrDefault(t => t.TopicName == topicName);
            if (topic == null)
            {
                var newTopic = new SubjectTopic { TopicName = topicName };
                _context.SubjectTopics.Add(newTopic);
                _context.SaveChanges();
                topic = newTopic;
            }

            var university = _context.Universities.FirstOrDefault(u => u.Name == universityName);
            if (university == null)
            {
                var newUniversity = new University { Name = universityName };
                _context.Universities.Add(newUniversity);
                _context.SaveChanges();
                university = newUniversity;
            }

            var institute = _context.Institutes.FirstOrDefault(i => i.Name == instituteName);
            if (institute == null)
            {
                var newInstitute = new Institute { Name = instituteName, UniversityId = university.UniversityId };
                _context.Institutes.Add(newInstitute);
                _context.SaveChanges();
                institute = newInstitute;
            }

            thesis.SupervisorId = supervisor.SupervisorId;
            thesis.CoSupervisorId = coSupervisor.CoSupervisorId;
            thesis.AuthorId = Author.AuthorId;
            thesis.KeywordId = keyword.KeywordId;
            thesis.TopicId = topic.TopicId;
            thesis.UniversityId = university.UniversityId;
            thesis.InstituteId = institute.InstituteId;

            _context.Theses.Add(thesis);
            _context.SaveChanges();

            return RedirectToAction("Update", "Update"); 
        }

        return View(thesis); 
    }
}
