using Microsoft.AspNetCore.Mvc;
using Messenger.Interface.Database;
using Messenger.Application.MessageHandlers;
using Messenger.Domain.Models;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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
            return View();
        }
        
        public IActionResult ChatCreation()
        {
            return View();
        }
        
        public IActionResult ChatJoining()
        {
            return View();
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
        public async Task<IActionResult> CreateNewChat(string name, ChatType type)
        {
            var dbChat = dbContext.Chats.FirstOrDefault(chat => chat.Name == name);
            if (dbChat != null) return RedirectToAction("ChatCreation");
            var user = dbContext.DomainUsers.FirstOrDefault(user => user.Name == User.Identity.Name);
            var chat = new Chat
            {
                Name = name,
                Type = type
            };
            if (user != null)
            {
                var chatUser = new ChatParticipant { Participant = user, Role = ChatRole.Admin };
                chat.Participants.Add(chatUser);
                dbContext.Chats.Add(chat);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Chat", new {chatId = chat.Id});
            }
            return RedirectToAction("ChatCreation");
        }
        
        [HttpPost]
        public async Task<IActionResult> JoinChat(string chatName)
        {
            var user = dbContext.DomainUsers.FirstOrDefault(user => user.Name == User.Identity.Name);
            var chat = dbContext.Chats.FirstOrDefault(chat => chat.Name == chatName);
            if (chat != null && user != null)
            {
                var chatUser = new ChatParticipant { Participant = user, Role = ChatRole.Guest };
                chat.Participants.Add(chatUser);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Chat", new { chatId = chat.Id });
            }
            return RedirectToAction("ChatJoining");
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(int chatId, string content)
        {
            var user = dbContext.DomainUsers.FirstOrDefault(user => user.Name == User.Identity.Name);
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