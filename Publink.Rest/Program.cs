using Microsoft.EntityFrameworkCore;
using Publink.Rest.Interfaces;
using Publink.Rest.Models;
using Publink.Rest.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostService, PostService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	var connectionString = builder.Configuration.GetConnectionString("Publink");

	options.UseSqlServer(connectionString);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
