using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Messenger.Domain.Models;


namespace Messenger.Interface.Database
{
    public class ApplicationDbContext : IdentityDbContext<Models.User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> DomainUsers { get; set; }
        public DbSet<ChatParticipant> ChatParticipants { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages {get; set;}
    }
}