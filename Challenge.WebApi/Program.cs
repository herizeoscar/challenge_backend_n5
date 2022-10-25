using Challenge.Application;
using Challenge.Application.ExtensionsMethods;
using Challenge.Infrastructure;
using Challenge.Infrastructure.Context;
using Challenge.WebApi.Mappings;
using Confluent.Kafka;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Filters;
using System.Reflection;

try {
    var builder = WebApplication.CreateBuilder(args);
    Log.Logger = new LoggerConfiguration().Filter.ByExcluding(Matching.FromSource("Microsoft"))
                                                      .WriteTo.File(path: @$"C:\Challenge_Backend\challenge_backend_.log", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 90, retainedFileTimeLimit: System.TimeSpan.FromDays(90))
                                                      .CreateLogger();

    Log.Information("Starting application.");
    // Add services to the container.
    builder.Services.AddControllers().AddFluentValidation(options => {
        // Validate child properties and root collection elements
        options.ImplicitlyValidateChildProperties = true;
        options.ImplicitlyValidateRootCollectionElements = true;
        // Automatic registration of validators in assembly
        options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    });
    ;
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddMediatR(Assembly.Load(new AssemblyName("Challenge.Application")));
    builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
    builder.Services.AddPersistence(builder.Configuration);
    builder.Services.AddApplication(builder.Configuration);
    builder.Services.AddElasticsearch(builder.Configuration);
    builder.Services.AddSingleton<ProducerConfig>(new ProducerConfig() {
        BootstrapServers = "localhost:9092"
    });
    builder.Host.UseSerilog();
    builder.Services.AddHealthChecks();
    builder.Services.AddAutoMapper(typeof(MappingProfile));

    var app = builder.Build();
    app.MapHealthChecks("/health");

    // Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    try {
        app.Services.CreateScope().ServiceProvider
            .GetService<ApplicationDbContext>()!.Database
            .Migrate();
    } catch(Exception e) {
        Log.Fatal("Error migrating database: " + e.Message);
    }

    Log.Information("Running the application.");
    app.Run();
} catch(Exception e) {
    Log.Fatal($"Error starting application. {e.Message}");
}
