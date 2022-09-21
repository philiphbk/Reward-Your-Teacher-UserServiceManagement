using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RYTUserManagementService.API;
using RYTUserManagementService.Common.Utilities;
using RYTUserManagementService.Core.ServiceImplementations;
using RYTUserManagementService.Core.ServiceInterfaces;
using RYTUserManagementService.Domain;
using RYTUserManagementService.Domain.RepoImplementations;
using RYTUserManagementService.Domain.RepoInterfaces;
using RYTUserManagementService.Dto;
using RYTUserManagementService.Models;
using Serilog;
using Serilog.Events;



var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

var services = builder.Services;

// Add services to the container.
// Add Serilog configuration
builder.Host.UseSerilog();

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(path: "c:\\RYTLUserManagementsService\\logs\\log-.txt",
        outputTemplate:
        "{Timestamp: yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:1j}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: LogEventLevel.Information
    ).CreateLogger();
try
{
    Log.Information("Application is starting");
    CreateHostBuilder(args).Build().Run();

}
catch (Exception e)
{
    Log.Fatal(e, "Application failed to start");
}
finally
{
    Log.CloseAndFlush();
}

static IHostBuilder CreateHostBuilder(string[] args) =>
Host.CreateDefaultBuilder(args)
        .UseSerilog()
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.Start();

        });



builder.Services.AddControllers();
builder.Services.AddDbContext<UserManagementDbContext>(options =>
{
//options.UseSqlServer(builder.Configuration.GetConnectionString("Path"));
options.UseNpgsql(builder.Configuration.GetConnectionString("Path"));
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


// Addition of the service extension class
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();


builder.Services.AddScoped<ISchoolServices, SchoolServices>();
builder.Services.AddScoped<IStudentServices, StudentServices>();
builder.Services.AddScoped<ITeacherServices, TeacherServices>();
builder.Services.AddScoped<AuthManager>();


builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddIdentity<ApiUser, IdentityRole>().AddEntityFrameworkStores<UserManagementDbContext>().AddDefaultTokenProviders();
builder.Services.AddTransient<IEmailSender, MailJetEmailSender>();

//builder.Services.AddIdentityCore<IdentityUser>(opt =>
//{
//    opt.User.RequireUniqueEmail = true;
//    opt.Password.RequiredUniqueChars = 2;
//    opt.Password.RequireUppercase = true;
//    opt.Password.RequiredLength = 8;
//}).AddEntityFrameworkStores<UserManagementDbContext>()
//.AddDefaultTokenProviders();
//.AddTokenProvider(default, typeof(ApiUserTokenProvider<ApiUser>));

builder.Services.AddAuthentication();



//AutoMapper for the DTO's
builder.Services.AddAutoMapper(typeof(Mappings));


// ADD JWT AUTHENTICATION
builder.Services.ConfigureJWT(config);


builder.Services.AddCors(o => {
    o.AddPolicy("AllowAll", builder => 
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();





builder.Services.AddControllers().AddNewtonsoftJson(op => 
    op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);




    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "RYTLP-UserManagement", Version = "v1"});

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @" JWT Authorization header using bearer scheme, 
                               enter 'Bearer' forllow by Space and enter your token ",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference(){
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "0auth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }

        });
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
});

app.UseHttpsRedirection();

app.UseCors("AllowAll");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
