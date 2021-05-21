using Fitverse.CalendarService.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitverse.CalendarService.Data
{
	public class CalendarContext : DbContext
	{
		public CalendarContext(DbContextOptions<CalendarContext> options) : base(options)
		{
		}

		public DbSet<ClassType> ClassTypes { get; set; }

		public DbSet<CalendarClass> Classes { get; set; }

		public DbSet<Timetable> Timetables { get; set; }
		
		public DbSet<Reservation> Reservations { get; set; }
		
		public DbSet<Member> Members { get; set; }
		
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Member>()
				.HasIndex(u => u.MemberId)
				.IsUnique();

			builder.Entity<ClassType>()
				.Property(x => x.IsDeleted)
				.HasDefaultValue(false);
		}
	}
}