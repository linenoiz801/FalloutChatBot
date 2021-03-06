using FalloutChat.Data;
using FalloutChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalloutChat.Services
{
    public class QuestionService
    {
        private readonly Guid _userId;
        private int GetVoteCount(int id, bool UpVote)
        {
            using (var ctx = new ApplicationDbContext())
            {
                return
                    ctx
                        .QuestionVotes
                        .Where(e => (e.QuestionId == id && e.GoodQuestion == UpVote))
                        .Count();
            }
        }
        public QuestionService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateQuestion(QuestionCreate model)
        {
            var entity =
                new Question()
                {
                    UserId = _userId,
                    QuestionText = model.QuestionText,
                    Answer = model.Answer,
                    QuestionAdded = false
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Questions.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<QuestionListItem> GetQuestions()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Questions
                        .AsEnumerable()
                        .Select(
                            e =>
                                new QuestionListItem
                                {
                                    UserId = e.UserId,
                                    Id = e.Id,
                                    QuestionText = e.QuestionText,
                                    QuestionAdded = e.QuestionAdded,
                                    Answer = e.Answer,
                                    DownVoteCount = GetVoteCount(e.Id, false),
                                    UpVoteCount = GetVoteCount(e.Id, true)
                                }
                        );

                return query.ToArray();
            }
        }
        public QuestionListItem GetQuestionById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Questions
                        .Single(e => e.Id == id);
                return
                    new QuestionListItem
                    {
                        Id = entity.Id,
                        UserId = entity.UserId,
                        Answer = entity.Answer,
                        QuestionAdded = entity.QuestionAdded,
                        QuestionText = entity.QuestionText,
                        DownVoteCount = GetVoteCount(entity.Id, false),
                        UpVoteCount = GetVoteCount(entity.Id, true)
                    };
            }
        }
        public bool UpdateQuestion(QuestionEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Questions
                        .Single(e => e.Id == model.Id);

                entity.Answer = model.Answer;
                entity.QuestionAdded = model.QuestionAdded;
                entity.QuestionText = model.QuestionText;                

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteQuestion(int Id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Questions
                        .Single(e => e.Id == Id);

                ctx.Questions.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
