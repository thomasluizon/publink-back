using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Publink.Rest.Context;
using Publink.Rest.Interfaces.Repository;
using Publink.Rest.Interfaces.Services;
using Publink.Rest.Models;
using Publink.Rest.Repository;
using Publink.Rest.Services;

namespace Publink.Rest.Extensions
{
	public static class Services
	{
		public static IServiceCollection AddAuth(this IServiceCollection services, Jwt jwt)
		{
			var key = Encoding.ASCII.GetBytes(jwt.Secret);

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					// Temp
					ValidateIssuer = false,
					// Temp
					ValidateAudience = false,
					IssuerSigningKey = new SymmetricSecurityKey(key)
				};
			});

			return services;
		}

		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen(c =>
			{
				var jwtSecurityScheme = new OpenApiSecurityScheme
				{
					Description = "Enter token",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = JwtBearerDefaults.AuthenticationScheme,
					BearerFormat = "JWT",

					Reference = new OpenApiReference
					{
						Id = JwtBearerDefaults.AuthenticationScheme,
						Type = ReferenceType.SecurityScheme
					}
				};

				c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						jwtSecurityScheme, Array.Empty<string>()
					}
				});
			});

			services.AddSingleton<DapperContext>();

			services.AddScoped<IPostRepository, PostRepository>();
			services.AddScoped<IPostService, PostService>();

			services.AddSingleton<IAuthRepository, AuthRepository>();
			//services.AddScoped<IAuthRepository, AuthRepository>();
			services.AddScoped<IAuthService, AuthService>();

			services.AddScoped<ITokenService, TokenService>();

			return services;
		}

		public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<Jwt>(configuration.GetSection("Jwt"));

			return services;
		}
	}
}
