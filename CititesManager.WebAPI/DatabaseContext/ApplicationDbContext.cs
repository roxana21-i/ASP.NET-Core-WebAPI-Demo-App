using CititesManager.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CititesManager.WebAPI.DatabaseContext
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public ApplicationDbContext() { }

		public virtual DbSet<City> Cities { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<City>().HasData(new City() 
			{ CityID = Guid.Parse("C13411F2-7D11-45C5-B0E3-1387C8175133"), CityName = "Timisoara" });
			modelBuilder.Entity<City>().HasData(new City()
			{ CityID = Guid.Parse("1D667409-5E0E-4533-80BB-F547F92B85B9"), CityName = "Cluj-Napoca" });
		}
	}
}
