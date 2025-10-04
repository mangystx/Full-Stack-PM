using FSPM_1.Db.Context;
using FSPM_1.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cs = builder.Configuration.GetConnectionString("Default")
         ?? throw new InvalidOperationException("Missing ConnectionStrings:Default");
builder.Services.AddDbContext<Fsmp1DbContext>(opt => opt.UseNpgsql(cs));

builder.Services.AddHttpClient<YouTubeService>();

builder.Services.AddDirectoryBrowser();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<Fsmp1DbContext>();
	await db.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();

app.Run();