using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MedicineProject.Data;
using MedicineProject.DataAccess.Repository;
using MedicineProject.DataAccess.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<MedicineProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MedicineProjectContext") ?? throw new InvalidOperationException("Connection string 'MedicineProjectContext' not found.")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.Run();
