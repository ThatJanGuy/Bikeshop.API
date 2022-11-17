﻿using Bikeshop.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bikeshop.API.DbContexts
{
    public class BikeshopContext : DbContext
    {
        public DbSet<Bike> Bikes { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

    }
}