using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FSPM_1.Db.Context;

public class Fsmp1DesignTimeFactory : IDesignTimeDbContextFactory<Fsmp1DbContext>

{
	public Fsmp1DbContext CreateDbContext(string[] args)
	{
		var cs = Environment.GetEnvironmentVariable("FSPM_Connection") 
		         ?? "Host=localhost;Port=5432;Database=fspm1db;Username=postgres;Password=postgres";

		var builder = new DbContextOptionsBuilder<Fsmp1DbContext>()
			.UseNpgsql(cs);

		return new Fsmp1DbContext(builder.Options);
	}
}