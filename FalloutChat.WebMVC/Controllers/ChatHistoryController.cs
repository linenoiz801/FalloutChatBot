using FalloutChat.Models;
using FalloutChat.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FalloutChat.WebMVC.Controllers
{
    public class ChatHistoryController : Controller
    {
        private ChatHistoryService CreateChatHistoryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ChatHistoryService(userId);
            return service;
        }
        // GET: ChatHistory
        public ActionResult Index()
        {
            var Service = CreateChatHistoryService();
            var model = Service.GetChatHistories();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChatHistoryCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var service = CreateChatHistoryService();
            if(service.CreateChatHistory(model))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Chat history could not be saved.");
            return View(model);
        }
    }
}