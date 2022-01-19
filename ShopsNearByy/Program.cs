using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ShopsNearByy.Models;
using ShopsNearByy.Services;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ShopsNearDatabaseSettings>(
                builder.Configuration.GetSection(nameof(ShopsNearDatabaseSettings)));

builder.Services.AddSingleton<IShopsNearDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<IShopsNearDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
        new MongoClient(builder.Configuration.GetValue<string>("ShopsNearDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IUserServices, UserServices>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

builder.Services.Configure<ShopsNearDatabaseSettings>(
builder.Configuration.GetSection("ShopNDatabase"));

builder.Services.AddSingleton<UserServices>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
