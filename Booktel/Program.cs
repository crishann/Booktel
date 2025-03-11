using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Booktel.Data;
using Booktel.Areas.Identity.Data;

namespace Booktel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("BooktelContextConnection")
                ?? throw new InvalidOperationException("Connection string 'BooktelContextConnection' not found.");

            // Add the database context
            builder.Services.AddDbContext<BooktelContext>(options =>
                options.UseSqlServer(connectionString));

            // Add Identity with BooktelUser
            builder.Services.AddDefaultIdentity<BooktelUser>(options =>
                options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<BooktelContext>();

            // Add services to the container
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapRazorPages();
            app.Run();
        }
    }
}
