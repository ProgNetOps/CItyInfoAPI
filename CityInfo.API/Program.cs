using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using Serilog;


//Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/cityinfo.txt", rollingInterval:RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

//Manually configuring logging
/*builder.Logging.ClearProviders(); //Remove the default logger
builder.Logging.AddConsole(); //Adding console logger*/


//Tell asp.net core to use Serilog instead of the in-built logger
builder.Host.UseSerilog();


// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add service to check file types
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

//Use preprocessor directives to select a service
#if DEBUG
builder.Services.AddTransient<IMailService,LocalMailService>();
#else
builder.Services.AddTransient<IMailService,CloudMailService>();
#endif

//Add a singleton service so we don't need to use the static "Current" property to get an instance of the class
builder.Services.AddSingleton<CitiesDataStore>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

//app.MapControllers();
app.UseEndpoints(endpoints => { 
    endpoints.MapControllers(); 
});

app.Run();
