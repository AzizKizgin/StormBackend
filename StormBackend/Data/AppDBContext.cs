using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StormBackend.Models;

namespace StormBackend.Data
{
    public class AppDBContext: IdentityDbContext<User>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<GroupMembership> GroupMemberships { get; set; }
        public DbSet<EmojiReaction> EmojiReactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Chats)
                .WithOne(c => c.User1)
                .HasForeignKey(c => c.User1Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Contacts)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.GroupMemberships)
                .WithOne(gm => gm.User)
                .HasForeignKey(gm => gm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Chat>()
                .HasMany(c => c.Messages)
                .WithOne(m => m.Chat)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contact>()
                .HasOne(c => c.User)
                .WithMany(u => u.Contacts)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GroupMembership>()
                .HasOne(gm => gm.User)
                .WithMany(u => u.GroupMemberships)
                .HasForeignKey(gm => gm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupMembership>()
                .HasOne(gm => gm.Group)
                .WithMany(g => g.Members)
                .HasForeignKey(gm => gm.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Group)
                .WithMany(g => g.Messages)
                .HasForeignKey(m => m.GroupId)
                .OnDelete(DeleteBehavior.Restrict);   

            modelBuilder.Entity<Message>()
                .HasMany(m => m.Reactions)
                .WithOne(r => r.Message)
                .HasForeignKey(r => r.MessageId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete reactions if message is deleted

            modelBuilder.Entity<EmojiReaction>()
                .HasOne(r => r.Message)
                .WithMany(m => m.Reactions)
                .HasForeignKey(r => r.MessageId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete reactions if message is deleted
             
        }
    }
}