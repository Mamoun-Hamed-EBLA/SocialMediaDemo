using API.Services;
using Application.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace API;

public static class DependencyInjection
{
	public static IServiceCollection AddAPI(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddControllers();
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo { Title = "Social Media Demo", Version = "v1" });

			c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				Description = "JWT Authorization header using   the Bearer scheme.Example: \"Authorization: Bearer {token}\"",
				Name = "Authorization",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.ApiKey
			});

			c.AddSecurityRequirement(new OpenApiSecurityRequirement(){{
			new OpenApiSecurityScheme()
			{
				Reference = new OpenApiReference

				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] {}
		}
		});
		});
		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
			   .AddJwtBearer(options =>
			   {
				   options.RequireHttpsMetadata = false;
				   options.TokenValidationParameters = new TokenValidationParameters
				   {
					   ValidateIssuerSigningKey = true,
					   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]!)),
					   ValidateIssuer = false,
					   ValidateAudience = false,
					   ValidateLifetime = true,
					   ClockSkew = TimeSpan.Zero
				   };
			   });
		services.AddAuthorization();
		services.AddHttpContextAccessor();
		services.AddScoped<ICurrentUserService, CurrentUserService>();
		services.AddInfrastructure(configuration);
		services.AddMemoryCache();

		return services;
	}
}