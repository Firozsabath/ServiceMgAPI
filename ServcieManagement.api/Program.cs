
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;
using ServiceManagement.EFCore;
using ServiceManagement.EFCore.Repositories;
using ServiceManagement.WebAPI;
using ServiceManagement.WebAPI.Mappings;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDBContext>(options =>
options.UseSqlServer(
    builder.Configuration.GetConnectionString("ServiceDBConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

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
builder.Services.AddScoped<ITechniciansNotesRespository, TechniciansNotesRespository>();
builder.Services.AddScoped<IRequestTypesRepository, RequestTypesRepository>();
builder.Services.AddScoped<IRequestPrioritiesRepository, RequestPrioritiesRepository>();
builder.Services.AddScoped<IEmailConfig, EmailConfig>();
builder.Services.AddScoped<IServiceRequestAttachmentsRepository, ServiceRequestAttachmentsRepository>();
builder.Services.AddScoped<IQuotationRepository, QuotationRepository>();


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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                        ValidAudience = builder.Configuration["JWT:ValidAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                    };
                });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add support to logging with SERILOG
//builder.Host.UseSerilog((context, configuration) =>
//    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

//app.UseSerilogRequestLogging();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

//SeedData.Seed();
app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
