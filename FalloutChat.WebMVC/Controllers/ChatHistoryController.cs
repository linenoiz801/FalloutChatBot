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
        public ActionResult Create()
        {
            return View();
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
        public ActionResult Details(int id)
        {
            var svc = CreateChatHistoryService();
            var model = svc.GetChatHistoryById(id);

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateChatHistoryService();
            var detail = service.GetChatHistoryById(id);
            var model =
                new ChatHistoryEdit
                {
                    Id = detail.Id,
                    BadResponse = detail.BadResponse,
                    MessageSent = detail.MessageSent,
                    ReceviedTimeUtc = detail.ReceviedTimeUtc,
                    ResponseReceived = detail.ResponseReceived,
                    SentTimeUtc = detail.SentTimeUtc                    
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ChatHistoryEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateChatHistoryService();
            if (service.UpdateChatHistory(model))
            {
                TempData["SaveResult"] = "Your vote was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your vote could not be updated.");
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            var svc = CreateChatHistoryService();
            var model = svc.GetChatHistoryById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateChatHistoryService();

            service.DeleteChatHistory(id);

            TempData["SaveResult"] = "Your comment was deleted";

            return RedirectToAction("Index");
        }
    }
}