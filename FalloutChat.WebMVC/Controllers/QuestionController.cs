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
    public class QuestionController : Controller
    {
        private QuestionService CreateQuestionService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new QuestionService(userId);
            return service;
        }
        // GET: Question
        public ActionResult Index()
        {
            var Service = CreateQuestionService();
            var model = Service.GetQuestions();
            return View(model);
        }

        // GET: Question/Details/5
        public ActionResult Details(int id)
        {
            var svc = CreateQuestionService();
            var model = svc.GetQuestionById(id);

            return View(model);
        }

        // GET: Question/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Question/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var service = CreateQuestionService();
            if (service.CreateQuestion(model))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Question could not be saved.");
            return View(model);
        }

        // GET: Question/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, QuestionEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateQuestionService();
            if (service.UpdateQuestion(model))
            {
                TempData["SaveResult"] = "Your question was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your question could not be updated.");
            return View(model);
        }

        // POST: Question/Edit/5        
        public ActionResult Edit(int id)
        {
            var service = CreateQuestionService();
            var detail = service.GetQuestionById(id);
            var model =
                new QuestionEdit
                {
                    Id = detail.Id,
                    Answer = detail.Answer,
                    QuestionAdded = detail.QuestionAdded,
                    QuestionText = detail.QuestionText
                };
            return View(model);
        }

        // GET: Question/Delete/5
        public ActionResult Delete(int id)
        {
            var svc = CreateQuestionService();
            var model = svc.GetQuestionById(id);

            return View(model);
        }

        // POST: Question/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var service = CreateQuestionService();

            service.DeleteQuestion(id);

            TempData["SaveResult"] = "Your question was deleted";

            return RedirectToAction("Index");
        }
        [HttpGet]                
        public ActionResult UpVote(int id)
        {
            QuestionVoteCreate qv = new QuestionVoteCreate
            {
                GoodQuestion = true,
                QuestionId = id
            };

            var service = new QuestionVoteService(Guid.Parse(User.Identity.GetUserId()));
            service.CreateQuestionVote(qv);
            return RedirectToAction("Index");
        }
        [HttpGet]                
        public ActionResult DownVote(int id)
        {
            QuestionVoteCreate qv = new QuestionVoteCreate
            {
                GoodQuestion = false,
                QuestionId = id
            };

            var service = new QuestionVoteService(Guid.Parse(User.Identity.GetUserId()));
            service.CreateQuestionVote(qv);
            return RedirectToAction("Index");
        }
    }
}
