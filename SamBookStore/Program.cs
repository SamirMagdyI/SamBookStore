using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SamBookStore.Models;
using SamBookStore.Models.Domain;
using SamBookStore.Repositories.Abstract;
using SamBookStore.Repositories.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<DatabaseContext>();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Genre}/{action=Add}/{id?}");

app.Run();
