using System.Linq;
using System.Security.Claims;
using Messenger.Interface.Database;
using Messenger.Interface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Interface.ViewComponents
{
    public class RoomViewComponent : ViewComponent
    {
        private ApplicationDbContext dbContext;

        public RoomViewComponent(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IViewComponentResult Invoke()
        {
            var user = dbContext.DomainUsers.FirstOrDefault(user => user.Name == User.Identity.Name);
            var chats = dbContext
                .Chats
                .Where(chat => 
                    chat.Participants
                        .Select(chatUser => chatUser.Participant)
                        .Contains(user)
                    );
            return View(chats);
        }
    }
}