using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProjectSem3.IService;
using ProjectSem3.Models;
using ProjectSem3.Models.Dao;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSession();

builder.Services.AddDbContext<IofaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<ISubmissionService, SubmissionDao>();
builder.Services.AddScoped<IArtworkService, ArtworkDao>();
builder.Services.AddScoped<IAccountStudentService, AccountStudentDao>();
builder.Services.AddScoped<ICompetitionService, CompetitionDao>();

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

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}");

app.Run();
