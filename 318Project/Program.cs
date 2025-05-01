using _318Project.Models;
using _318Project.Services;           // ensures LoanService & FineService are in scope
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryConnectionString")));

// register your LoanService so ReturnModel can resolve it:
builder.Services.AddScoped<LoanService>();
// register the FineService for managing fines:
builder.Services.AddScoped<FineService>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
