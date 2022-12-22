using Apricot.Database;
using Apricot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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

        public IActionResult Main(int userId)
        {
            User user = db.Users.Where(e => e.ID == userId).Include(e => e.Contacts).Include(e => e.SpaceUsers).ThenInclude(e => e.Space).First();
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
            ICollection<Chat>? allChats = myChats.Concat(recipientChats).OrderBy(e => e.Created).ToList();
            if(allChats == null)
            {
                allChats = new List<Chat>();
            }
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

        [HttpPost]
        public IActionResult CheckNewMessages(int lastChatId, int userId, int recipientId)
        {
            var chat = db.Chats.Where(e => e.ChadID == lastChatId).FirstOrDefault();

            var user = db.Users.Where(e => e.ID == userId).First();
            var recipient = db.Users.Where(e => e.ID == recipientId).First();

            if(chat == null)
            {
                chat = new Chat();
                chat.Created = DateTime.Now.AddDays(-5);
            }

            chat.UserID = user.ID;
            chat.UserID2 = recipient.ID;

            ICollection<Chat> myChats = db.Chats.Where(e => e.UserID == chat.UserID && e.UserID2 == chat.UserID2 && e.Created > chat.Created).ToList();
            ICollection<Chat> recipientChats = db.Chats.Where(e => e.UserID == chat.UserID2 && e.UserID2 == chat.UserID && e.Created > chat.Created).ToList();
            ICollection<Chat> allChats = myChats.Concat(recipientChats).OrderBy(e => e.Created).ToList();

            if (allChats.Count <= 0)
            {
                return Json(new { newMsg = false });
            }



            ChatToken chatToken = new ChatToken()
            {
                User = user,
                Chats = allChats
            };

            var partialViewHtml = Common.RenderViewAsync<ChatToken>(this, "ChatBubble", chatToken, true).Result;

            return Json(new { newMsg = true, messages = partialViewHtml, lastChatId = allChats.Last().ChadID }); ;
        }

        public IActionResult Space(int spaceId)
        {
            User user = db.Users.Include(e => e.Contacts).Include(e => e.SpaceUsers).ThenInclude(e => e.Space).First();
            Space space = db.Spaces.Where(e => e.ID == spaceId).FirstOrDefault();
            ICollection<User> contacts = db.Users.Where(e => e.Contacts.Where(e => e.UserID == user.ID).Count() > 0).ToList();
            ICollection<Space> spaces = new List<Space>();
            foreach (var item in user.SpaceUsers)
            {
                spaces.Add(item.Space);
            }
            ViewBag.Contacts = contacts;
            ViewBag.Spaces = spaces;
            ViewBag.Space = space;
            ViewBag.ID = user.ID;


            return View(user);
        }

        public IActionResult ChatBubble()
        {

            return View(db.Chats.ToList());
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

    public class ChatToken
    {
        public User User { get; set; }
        public ICollection<Chat> Chats { get; set; }
    }
}