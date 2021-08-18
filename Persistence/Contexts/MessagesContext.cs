using MessagesApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagesApi.Persistence.Contexts
{
    public class MessagesContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public MessagesContext(DbContextOptions<MessagesContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Message>().ToTable("Messages");
            builder.Entity<Message>().HasKey(mi => mi.Id);
            builder.Entity<Message>().Property(mi => mi.Text).IsRequired();

            builder.Entity<Message>().HasData
            (
                new Message { Id = 1, Text = "First message" },
                new Message { Id = 2, Text = "Second message" }
            );
        }
    }
}
