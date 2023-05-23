using ChatManager.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace ChatManager.Controllers
{
    public class ChatController : Controller
    {
        [OnlineUsers.UserAccess]
        public ActionResult Index()
        {
            //var users = DB.Users.ToList().Where(user => user.Status == 3)
            //                           .OrderBy(user => user.Status)
            //                           .ToList();
            var users = DB.Users.ToList();
            ViewBag.Messages = null;

            return View(users);
        }

        [HttpGet]
        public ActionResult GetMessagesViewBag(int id)
        {
            var currentId = OnlineUsers.GetSessionUser().Id;

            var chatMessages = DB.ChatMessages.ToList().Where(message =>
            message.Sender == currentId && message.Receiver == id ||
            message.Sender == id && message.Receiver == currentId).ToList();

            ViewBag.Messages = chatMessages;

            return Json(chatMessages, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetMessages(int id)
        {
            var currentId = OnlineUsers.GetSessionUser().Id;

            var chatMessages = DB.ChatMessages.ToList().Where(message => 
            message.Sender == currentId && message.Receiver == id ||
            message.Sender == id && message.Receiver == currentId).ToList();

            ViewBag.Messages = chatMessages;

            return Json(chatMessages, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SendMessage(int id, string message)
        {
            DB.ChatMessages.Create(new ChatMessage(OnlineUsers.GetSessionUser().Id, id, message));

            return RedirectToAction("Index");
        }
    }
}