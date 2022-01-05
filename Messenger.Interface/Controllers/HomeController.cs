using Microsoft.AspNetCore.Mvc;
using Messenger.Interface.Database;
using Messenger.Application.MessageHandlers;
using Messenger.Domain.Models;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Messenger.Interface.Controllers
{
    [Authorize]
    public class HomeController: Controller
    {
        private IEnumerable<IMessagesHandler> handlers;
        private ApplicationDbContext dbContext { get; }
        public HomeController(ApplicationDbContext dbContext, IEnumerable<IMessagesHandler> handlers)
        {
            this.dbContext = dbContext;
            this.handlers = handlers;
        }
        public IActionResult Index()
        {
            var chats = dbContext.Chats;
            return View(chats);
        }

        [HttpGet("id")]
        public IActionResult Chat(int chatId)
        {
            var chat = dbContext.Chats
                .Include(chat => chat.Messages)
                .ThenInclude(msg => msg.Sender)
                .FirstOrDefault(chat => chat.Id == chatId);
            return View(chat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewChat(string name)
        {
            var chat = new Chat
            {
                Name = name,
                Type = Domain.Models.Chat.DialogType.MultiUserChat
            };
            dbContext.Chats.Add(chat);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(int chatId, string content)
        {
            var user = dbContext.Users.FirstOrDefault(user => user.Name == User.Identity.Name);
            var message = new Message
            {
                Content=content, 
                Sender=user,
                Date=DateTime.Now, 
                ChatId=chatId
            };
            var chat = dbContext.Chats
                .Include(chat => chat.Messages)
                .ThenInclude(msg => msg.Sender)
                .FirstOrDefault(chat => chat.Id == chatId);
            if (chat != null)
            {
                foreach (var handler in handlers)
                    message = handler.HandleMessage(chat.Messages, message);
                if (message != null)
                {
                    chat.Messages.Add(message);
                    await dbContext.SaveChangesAsync();
                }
            }
            return RedirectToAction("Chat", new {chatId});
        }
    }
}