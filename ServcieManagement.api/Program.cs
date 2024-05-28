
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;
using ServiceManagement.EFCore;
using ServiceManagement.EFCore.Repositories;
using ServiceManagement.WebAPI.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDBContext>(options =>
options.UseSqlServer(
    builder.Configuration.GetConnectionString("ServiceDBConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IBranchesRepository, BranchRepository>();
builder.Services.AddScoped<IInventroryRepository, InventoryRepository>();
builder.Services.AddScoped<IMachinesRepository, MachinesRepository>();
builder.Services.AddScoped<ITechniciansRepository, TechniciansRepository>();
builder.Services.AddScoped<IServiceRequestsRepository, ServiceRequestsRepository>();
builder.Services.AddScoped<IVendorRepository, VendorRepository>();
builder.Services.AddScoped<IDropdowns, Dropdowns>();
builder.Services.AddScoped<IservicePartsRepository, ServicePartsRepository>();
builder.Services.AddScoped<IRequestSequenceRepository, RequestSequenceRepository>();

builder.Services.AddAutoMapper(typeof(Maps));
builder.Services.AddCors(option =>
{
    option.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        });
});
builder.Services.AddIdentity<ApplicationUser,ApplicationRole>().AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

//SeedData.Seed();
app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
