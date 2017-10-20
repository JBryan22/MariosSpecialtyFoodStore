using Microsoft.EntityFrameworkCore;
using MariosSpecialtyStore.Models;

namespace MariosSpecialtyStore.Models
{
	public class TestDbContext : MariosSpecialtyStoreContext
	{
		public override DbSet<Product> Products { get; set; }
		public override DbSet<Review> Reviews { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			options.UseMySql(@"Server=localhost;Port=8889;database=mariosspecialtystore_test;uid=root;pwd=root;");
		}
	}
}