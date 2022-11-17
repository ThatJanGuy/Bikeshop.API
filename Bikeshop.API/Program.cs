var builder = WebApplication.CreateBuilder(args);

// Services

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseRouting();
    app.UseAuthorization();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
