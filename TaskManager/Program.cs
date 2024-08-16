using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Repository.Interface;
using TaskManager.Repository.Services;
using FluentValidation;
using System.Globalization;
using TaskManager.Models;
using Microsoft.AspNetCore.Identity;
using FluentValidation.AspNetCore;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Repository
builder.Services.AddScoped<ITaskListRepository, TaskListRepository>();


builder.Services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("sqldata")),
                ServiceLifetime.Scoped);


//Add Fluent Validation
builder.Services.AddValidatorsFromAssemblyContaining<TaskListValidator>(); 
builder.Services.AddFluentValidationAutoValidation(); 
builder.Services.AddFluentValidationClientsideAdapters(); 

var app = builder.Build();


// EF Migration Service
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
