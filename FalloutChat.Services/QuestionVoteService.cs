using FalloutChat.Data;
using FalloutChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalloutChat.Services
{
    public class QuestionVoteService
    {
        private readonly Guid _userId;
        public QuestionVoteService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateQuestionVote(QuestionVoteCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                int count =
                ctx
                    .QuestionVotes
                    .Count(e => (e.QuestionId == model.QuestionId) && (e.UserId == _userId));

                if (count > 0)
                {
                    var ent =
                    ctx
                        .QuestionVotes
                        .Single(e => (e.QuestionId == model.QuestionId) && (e.UserId == _userId));
                    if (ent != null)
                    {
                        DeleteQuestionVote(ent.Id);
                    }
                }

                var entity =
                    new QuestionVote()
                    {
                        QuestionId = model.QuestionId,
                        GoodQuestion = model.GoodQuestion,
                        UserId = _userId
                    };

                ctx.QuestionVotes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<QuestionVoteListItem> GetQuestionVotesByUserId()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .QuestionVotes
                        .Where(e => e.UserId == _userId)
                        .Select(
                            e =>
                                new QuestionVoteListItem
                                {
                                    UserId = e.UserId,
                                    Id = e.Id,
                                    GoodQuestion = e.GoodQuestion,
                                    QuestionId = e.QuestionId,
                                    Question =
                                        ctx.Questions.Single(f => f.Id == e.QuestionId)
                                }
                        );

                return query.ToArray();
            }
        }
        public QuestionVoteListItem GetQuestionVoteById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .QuestionVotes
                        .Single(e => e.Id == id);
                return
                    new QuestionVoteListItem
                    {
                        Id = entity.Id,
                        QuestionId = entity.QuestionId,
                        Question =
                            ctx.Questions.Single(f => f.Id == entity.QuestionId),
                        GoodQuestion = entity.GoodQuestion
                    };
            }
        }
        public IEnumerable<QuestionVoteListItem> GetQuestionVotesByQuestionId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .QuestionVotes
                        .Where(e => e.QuestionId == id)
                        .Select(
                            f => new QuestionVoteListItem()
                            {
                                Id = f.Id,
                                QuestionId = f.QuestionId,
                                Question =
                                    ctx.Questions.Single(g => g.Id == f.QuestionId),
                                GoodQuestion = f.GoodQuestion
                            }
                        );
                return entity.ToArray();
            }
        }
        public bool UpdateQuestionVote(QuestionVoteEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .QuestionVotes
                        .Single(e => e.Id == model.Id);

                entity.GoodQuestion = model.GoodQuestion;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteQuestionVote(int Id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .QuestionVotes
                        .Single(e => e.Id == Id);

                ctx.QuestionVotes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
