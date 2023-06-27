using Publink.Rest.Extensions;
using Publink.Rest.Models;

var builder = WebApplication.CreateBuilder(args);

const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: myAllowSpecificOrigins,
	corsPolicyBuilder =>
	{
		corsPolicyBuilder.AllowAnyOrigin();
		corsPolicyBuilder.AllowAnyHeader();
		corsPolicyBuilder.AllowAnyMethod();
	});
});

builder.Services.AddControllers();

var jwt = new Jwt
{
	Secret = builder.Configuration.GetSection("JwtSecret").Value
};

builder.Services
	.AddAuth(jwt)
	.AddServices()
	.AddConfiguration(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(myAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
