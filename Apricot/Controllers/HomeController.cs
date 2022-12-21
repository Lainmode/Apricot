using Apricot.Database;
using Apricot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace Apricot.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApricotContext db = new ApricotContext();
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            db.ChangeTracker.LazyLoadingEnabled = false;
            base.OnActionExecuting(context);
        }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.CurrentPage = CurrentPage.Home;
            return View();
        }

        public IActionResult SignUp()
        {
            ViewBag.CurrentPage = CurrentPage.SignUp;
            return View();
        }

        public IActionResult Login()
        {
            ViewBag.CurrentPage = CurrentPage.Login;
            return View();
        }

        public IActionResult Main()
        {
            User user = db.Users.Include(e => e.Contacts).Include(e => e.SpaceUsers).ThenInclude(e => e.Space).First();
            ICollection<User> contacts = db.Users.Where(e => e.Contacts.Where(e => e.UserID == user.ID).Count() > 0).ToList();
            ICollection<Space> spaces = new List<Space>();
            foreach (var item in user.SpaceUsers)
            {
                spaces.Add(item.Space);
            }
            ViewBag.Contacts = contacts;
            ViewBag.Spaces = spaces;
            ViewBag.ID = user.ID;
            return View(user);
        }

        public IActionResult Chat(int userId, int recipientUserId)
        {
            ICollection<Chat> myChats = db.Chats.Where(e => e.UserID == userId && e.UserID2 == recipientUserId).ToList();
            ICollection<Chat> recipientChats = db.Chats.Where(e => e.UserID == recipientUserId && e.UserID2 == userId).ToList();
            ICollection<Chat> allChats = myChats.Concat(recipientChats).OrderBy(e=>e.Created).ToList();
            User user = db.Users.Where(e => e.ID == userId).FirstOrDefault();
            User recipient = db.Users.Where(e => e.ID == recipientUserId).FirstOrDefault();

            ViewBag.MyChats = myChats;
            ViewBag.RecipientChats = recipientChats;
            ViewBag.AllChats = allChats;

            ViewBag.User = user;
            ViewBag.Recipient = recipient;

            return View();
        }

        public IActionResult SendMessage(int userId, int recipientId, string text)
        {
            Chat chat = new Chat()
            {
                Created = DateTime.Now,
                Text = text,
                UserID = userId,
                UserID2 = recipientId,

            };
            db.Chats.Add(chat);
            db.SaveChanges();

            return Json(new { sucess = true });
        }

        public IActionResult Space(int spaceId)
        {
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.CurrentPage = CurrentPage.About;
            return View();
        }
        public IActionResult ContactSupport()
        {
            ViewBag.CurrentPage = CurrentPage.ContactSupport;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}