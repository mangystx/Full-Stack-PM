using FSPM_1.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace FSPM_1.Db.Context;

public class Fsmp1DbContext(DbContextOptions<Fsmp1DbContext> options) : DbContext(options)
{
	public DbSet<Video> Videos => Set<Video>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Video>()
			.HasIndex(v => v.VideoId)
			.IsUnique();
	}
}