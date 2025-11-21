using Library.Infrastructure.Data;
using Library.Infrastructure.Repositories;
using Library.Infrastructure.Services;
using Library.Application.Services;
using Library.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Library.API.Middleware; 


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryConnection")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
