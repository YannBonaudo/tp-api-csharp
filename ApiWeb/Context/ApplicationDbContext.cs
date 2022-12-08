﻿using Database;
using Microsoft.EntityFrameworkCore;

namespace ApiWeb.Context
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {
        }

        public DbSet<User> Users{ get; set; }

        public DbSet<Pet> Pets{ get; set; }

    }
}
