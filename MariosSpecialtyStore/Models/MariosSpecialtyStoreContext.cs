using System;
using Microsoft.EntityFrameworkCore;

namespace MariosSpecialtyStore.Models
{
    public class MariosSpecialtyStoreContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseMySql(@"Server=localhost;Port=8889;database=mariosspecialtystore;uid=root;pwd=root;");
    }
}
