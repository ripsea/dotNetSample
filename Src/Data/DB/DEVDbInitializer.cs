using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DB
{
    internal class DEVDbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DEVDbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        internal void Seed()
        {
            modelBuilder.Entity<User>().HasData(
                new User() { Id= Guid.NewGuid(), 
                    Name = "iris", 
                    Password = "iris" });
        }
    }
}
