using Application;
using Application.Interfaces;
using ChallengeX.Infra.Data.Identity;
using ChallengeX.Infra.Data.Identity.Models;
using Infrastructure.Persistence;
using Infrastructure.Settings;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
	public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddScoped<JWTSettings>();
		services.Configure<JWTSettings>(configuration.GetSection("Token"));

		services.AddScoped<IDateTime, CurrentDate>();
		services.AddScoped<ITokenManager, TokenManager>();
		services.AddScoped<IIdentityService, IdentityService>();
		services.AddScoped<IDbContext, ApplicationDbContext>();



		services.AddApplication(configuration);
		services.AddDbContext<ApplicationDbContext>(options =>
				  options.UseSqlServer(
					 configuration.GetConnectionString("DefaultConnection"),
					  b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


		services.AddIdentityCore<ApplicationUser>(
			options =>
			{
				options.User.RequireUniqueEmail = false;
				options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
			})
			.AddRoles<IdentityRole>()
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddUserManager<UserManager<ApplicationUser>>();

	}
}