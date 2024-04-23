using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Xml.Linq;
using WebAtonTest.Data;
using WebAtonTest.Managers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Подключать свою Базу Данных тут \|/
builder.Services.AddDbContext<WebAtonTestDbContext>(opt =>
    opt.UseNpgsql("Host = localhost; Port = 5432; Database = aton; Username = postgres; Password = 123;")); 

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000"); // add the allowed origins  
                          policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});
builder.Services.AddScoped<IUsersManager, UsersManager>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();

