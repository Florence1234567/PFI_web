using ChatManager.Models;
using Microsoft.Ajax.Utilities;
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
            var users = new List<User>();

            var currentId = OnlineUsers.GetSessionUser().Id;

            var friendships = DB.Friendships.ToList().Where(m => m.IdUser1 == currentId
            || m.IdUser2 == currentId);

            foreach (var friendship in friendships)
            {
                if(friendship.IdUser1 != currentId)
                {
                    users.Add(DB.Users.Get(friendship.IdUser1));
                }
                else
                {
                    users.Add(DB.Users.Get(friendship.IdUser2));
                }
            }

            var chatMessages = DB.ChatMessages.ToList().Where(message =>
            message.Sender == 0).ToList();

            var tuple = new Tuple<List<User>, List<ChatMessage>>(users, chatMessages);


            return View(tuple);
        }

        [HttpGet]
        public ActionResult GetMessagesViewBag(int id)
        {
            var currentId = OnlineUsers.GetSessionUser().Id;

            var chatMessages = DB.ChatMessages.ToList().Where(message =>
            message.Sender == currentId && message.Receiver == id ||
            message.Sender == id && message.Receiver == currentId).ToList();

            var users = DB.Users.ToList();

            var tuple = new Tuple<List<User>, List<ChatMessage>>(users, chatMessages);

            return PartialView("Index", tuple);
        }


        [HttpGet]
        public ActionResult GetMessages(int id)
        {
            var currentId = OnlineUsers.GetSessionUser().Id;

            var chatMessages = DB.ChatMessages.ToList().Where(message => 
            message.Sender == currentId && message.Receiver == id ||
            message.Sender == id && message.Receiver == currentId).ToList();

            return Json(chatMessages, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SendMessage(int id, string message)
        {
            DB.ChatMessages.Create(new ChatMessage(OnlineUsers.GetSessionUser().Id, id, message));

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditMessage(int id, string message)
        {
            ChatMessage chatMessage = DB.ChatMessages.Get(id);
            chatMessage.Message = message;

            //DB.ChatMessages.Update(chatMessage);

            return RedirectToAction("Index");
        }

        public ActionResult DeleteMessage()
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteMessage(int id)
        {
            DB.ChatMessages.Delete(id);

            return RedirectToAction("Index");
        }
    }
}