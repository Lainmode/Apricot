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
using System.Threading.Channels;

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

        public IActionResult Chat(int userId, int channelId)
        {
            TextChannel channel = db.TextChannels.Where(e => e.ID == channelId).First();

            ICollection<Chat> myChats = channel.Chats.Where(e => e.UserID == userId).ToList();
            ICollection<Chat> recipientChats = channel.Chats.Where(e => e.UserID != userId).ToList();
            ICollection<Chat>? allChats = myChats.Concat(recipientChats).OrderBy(e => e.Created).ToList();
            if (allChats == null)
            {
                allChats = new List<Chat>();
            }
            User user = db.Users.Where(e => e.ID == userId).FirstOrDefault();
            ICollection<User> recipients = channel.Users.Where(e => e.ID != userId).ToList();

            ViewBag.MyChats = myChats;
            ViewBag.RecipientChats = recipientChats;
            ViewBag.AllChats = allChats;

            ViewBag.User = user;
            ViewBag.Recipient = recipients;
            ViewBag.Channel = channel;

            return View();
        }

        public IActionResult SendMessage(int userId, int channelId, string text)
        {
            TextChannel channel = db.TextChannels.Where(e => e.ID == channelId).FirstOrDefault();
            Chat chat = new Chat()
            {
                Created = DateTime.Now,
                Text = text,
                UserID = userId,
                ChannelID = channel.ID,

            };

            channel.Chats.Add(chat);

            db.Chats.Add(chat);
            db.TextChannels.Update(channel);
            db.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult CheckNewMessages(int lastChatId, int userId, int channelId)
        {
            var chat = db.Chats.Where(e => e.ID == lastChatId).FirstOrDefault();

            var user = db.Users.Where(e => e.ID == userId).First();

            if (chat == null)
            {
                chat = new Chat();
                chat.Created = DateTime.Now.AddDays(-5);
            }

            chat.UserID = user.ID;

            TextChannel channel = db.TextChannels.Where(e => e.ID == channelId).First();

            ICollection<Chat> myChats = channel.Chats.Where(e => e.UserID == userId).ToList();
            ICollection<Chat> recipientChats = channel.Chats.Where(e => e.UserID != userId).ToList();
            ICollection<Chat>? allChats = myChats.Concat(recipientChats).OrderBy(e => e.Created).ToList();

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

            return Json(new { newMsg = true, messages = partialViewHtml, lastChatId = allChats.Last().ID }); ;
        }

        public IActionResult Space(int spaceId)
        {
            User user = db.Users.Include(e => e.Contacts).Include(e => e.SpaceUsers).ThenInclude(e => e.Space).First();
            Space space = db.Spaces.Where(e => e.ID == spaceId).FirstOrDefault();

            ICollection<Space> spaces = new List<Space>();
            foreach (var item in user.SpaceUsers)
            {
                spaces.Add(item.Space);
            }
            ViewBag.Users = space.SpaceUsers;
            ViewBag.Spaces = spaces;
            ViewBag.Space = space;
            ViewBag.ID = user.ID;
            ViewBag.Channel = space.TextChannel;

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