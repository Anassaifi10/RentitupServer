using RentalApp.Application.Common.Interfaces;
using RentalApp.Domain.Constants;
using RentalApp.Infrastructure.Data;
using RentalApp.Infrastructure.Data.Interceptors;
using RentalApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RentalApp.Application.Common.Models;
using RentalApp.Infrastructure.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("RentalAppDb");
        Guard.Against.Null(connectionString, message: "Connection string 'RentalAppDb' not found.");
        builder.Services.Configure<CloudinarySetting>(builder.Configuration.GetSection("CloudinarySettings"));
        builder.Services.Configure<SendGridSettings>(builder.Configuration.GetSection("SendGrid"));

        builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        builder.Services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        builder.Services.AddTransient<IImageServices, ImageServices>();
        builder.Services.AddTransient<IItemServices, ItemServices>();
        builder.Services.AddTransient<IItemRequestService, ItemRequestServices>();
        builder.Services.AddTransient<IEmailSenderServices, SendGridEmailServices>();
        builder.Services.AddTransient<IUserAccountServices, UserAccountServices>();

        builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString);
        });


        builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        builder.Services.AddScoped<ApplicationDbContextInitialiser>();

        builder.Services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme, op =>
            {
                op.Events = new AspNetCore.Authentication.BearerToken.BearerTokenEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.HttpContext.Request.Query["access_token"];
                        var path = context.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/Rentituphub"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        builder.Services.AddAuthorizationBuilder();

        builder.Services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();

        builder.Services.AddSingleton(TimeProvider.System);
        builder.Services.AddTransient<IIdentityService, IdentityService>();

        builder.Services.AddAuthorization(options =>
            options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));

        builder.Services.AddCors(option =>
        {
            option.AddPolicy("CORS", pol =>
            {
                pol.WithOrigins(["http://localhost:5173"]).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            });
        });

        builder.Services.AddSignalR();
    }
}
