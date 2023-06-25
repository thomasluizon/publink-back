using Publink.Rest.Extensions;
using Publink.Rest.Models;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: MyAllowSpecificOrigins,
	builder =>
	{
		builder.AllowAnyOrigin();
		builder.AllowAnyHeader();
		builder.AllowAnyMethod();
	});
});

builder.Services.AddControllers();

var jwt = new Jwt();
builder.Configuration.GetSection("Jwt").Bind(jwt);

builder.Services
	.AddAuth(jwt)
	.AddServices()
	.AddConfiguration(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
