using FalloutChat.Models;
using FalloutChat.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalloutChat.Services
{
    public class ChatHistoryService
    {
        private readonly Guid _userId;
        public ChatHistoryService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateChatHistory(ChatHistoryCreate model)
        {
            var entity =
                new ChatHistory()
                {
                    UserId = _userId,
                    BadResponse = model.BadResponse,
                    MessageSent = model.MessageSent,
                    ReceviedTimeUtc = model.ReceviedTimeUtc,
                    ResponseReceived = model.ResponseReceived,
                    SentTimeUtc = model.SentTimeUtc
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.ChatHistories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<ChatHistoryListItem> GetChatHistories()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .ChatHistories
                        .Select(
                            e =>
                                new ChatHistoryListItem
                                {
                                    UserId = e.UserId,
                                    BadResponse = e.BadResponse,
                                    MessageSent = e.MessageSent,
                                    ReceviedTimeUtc = e.ReceviedTimeUtc,
                                    ResponseReceived = e.ResponseReceived,
                                    SentTimeUtc = e.SentTimeUtc,
                                    Id = e.Id
                                }
                        );

                return query.ToArray();
            }
        }
        public ChatHistoryListItem GetChatHistoryById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .ChatHistories
                        .Single(e => e.Id == id);
                return
                    new ChatHistoryListItem
                    {
                        Id = entity.Id,
                        UserId = entity.UserId,
                        SentTimeUtc = entity.SentTimeUtc,
                        ResponseReceived = entity.ResponseReceived,
                        ReceviedTimeUtc = entity.ReceviedTimeUtc,
                        MessageSent = entity.MessageSent,
                        BadResponse = entity.BadResponse
                    };
            }
        }
        public bool UpdateChatHistory(ChatHistoryEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .ChatHistories
                        .Single(e => e.Id == model.Id);

                entity.BadResponse = model.BadResponse;
                entity.MessageSent = model.MessageSent;
                entity.ResponseReceived = model.ResponseReceived;
                entity.SentTimeUtc = model.SentTimeUtc;
                entity.ReceviedTimeUtc = model.ReceviedTimeUtc;                

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteChatHistory(int Id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .ChatHistories
                        .Single(e => e.Id == Id);

                ctx.ChatHistories.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
