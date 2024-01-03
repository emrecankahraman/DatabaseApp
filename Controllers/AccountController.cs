using DatabaseApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 

namespace DatabaseApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly Database1Context _context;

        public AccountController(Database1Context context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View(new Admin());
        }

        [HttpPost]
        public IActionResult Login(Admin user)
        {
            var userInDb = _context.Admins.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

            if (userInDb != null)
            {
                return RedirectToAction("Update", "Update");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(user);
            }
        }
    }
}
