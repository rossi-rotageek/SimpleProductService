using Microsoft.EntityFrameworkCore;
using Serilog;
using SimpleService.DataServices;
using SimpleService.ExtensionMethods;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Logging.AddSerilog(logger);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>
    (o => o.UseInMemoryDatabase("SimpleServiceDatabase"));
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();
app.FeedSampleData();
app.ConfigureMiddlewares();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();