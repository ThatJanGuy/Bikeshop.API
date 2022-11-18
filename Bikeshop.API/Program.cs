using Bikeshop.API.DbContexts;
using Bikeshop.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<BikeshopContext>(
    dbContextOptions => dbContextOptions.UseSqlServer(
    builder.Configuration["ConnectionStrings:BikeshopDBConnectionString"]));
builder.Services.AddScoped<IBikeshopRepository, BikeshopRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
