using Microsoft.EntityFrameworkCore;
using Task.Api.Extensions;
using Task.Api.Middlewares;
using Task.Data.DbContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Custom Services
builder.Services.AddCustomService();

//Jwt Configuration
builder.Services.AddJwtService(builder.Configuration);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administration", p => p.RequireRole("Admin", "SuperAdmin"));
    options.AddPolicy("AdminMerchant", p => p.RequireRole("Admin", "Merchant"));
    options.AddPolicy("Worker", p => p.RequireRole("Driver", "Picker", "Packer"));
});

//Add DbContext
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection")));

//Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularOrigins",
    builder =>
    {
        builder.WithOrigins(
                            "http://localhost:4200",
                            "http://localhost:25342"
                            )
                            .AllowAnyHeader()
                            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowAngularOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
