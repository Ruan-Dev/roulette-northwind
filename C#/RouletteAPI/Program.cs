using Microsoft.EntityFrameworkCore;
using Roulette.API.Services;
using Roulette.API.Data;
using Roulette.API.Middleware;
using Roulette.API.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddDbContext<IDbContext, ApplicationDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRouletteService, RouletteService>();
builder.Services.AddScoped<IPayoutService, PayoutService>();
builder.Services.AddScoped<ISpinService, SpinService>();
builder.Services.AddScoped<IBetService, BetService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RouletteApi v1 - Derivco Assessment"));
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();
    dbContext.Database.Migrate();
}

app.Run();
