global using passports.Models;
using passports.Services.PassportService;
using PassportsAPI.EfCore;
using Microsoft.EntityFrameworkCore;
using Quartz;
using PassportsAPI.Quartz;
using PassportsAPI.Services.PassportUpdateService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PassportsDb")));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IPassportService, PassportService>();
builder.Services.AddScoped<IPassportUpdateService,PassportUpdateService>();

builder.Services.AddQuartz(options =>
{
options.UseMicrosoftDependencyInjectionJobFactory();
    var setting = builder.Configuration.GetSection("Schedule");
    var jobKey = new JobKey("ImportJob1");
options.AddJob<ImportJob>(opts => opts.WithIdentity(jobKey));
    options.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("ImportJob1-trigger")
        .WithCronSchedule(setting["ImportPassportsJob"]));

});

builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    db.Database.Migrate();
}

app.Run();
