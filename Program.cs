using Hangfire;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ProductAPI.VSA.Common;
using ProductAPI.VSA.Data;
using ProductAPI.VSA.Extensions;
using ProductAPI.VSA.Features.Products.Repository.Implementation;
using ProductAPI.VSA.Features.Products.Repository.Interface;
using ProductAPI.VSA.Health;
using ProductAPI.VSA.Settings;
using protoBuffer;
using System.Net;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddOData(opt =>
{
    opt.Select().Filter().OrderBy();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.WebHost.UseUrls("http://localhost:7102");



//Adding dependencies to the Application Pipeline
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("defaultConnectionString");

    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnectionString"));
});
builder.AddApplicationServices();


builder.Services.AddHangfire(config =>
{
    config.UseSimpleAssemblyNameTypeSerializer()
             .UseRecommendedSerializerSettings()
             .UseSqlServerStorage(builder.Configuration.GetConnectionString("defaultConnectionString"));
});

builder.Services.AddHangfireServer();

// Here to register all configurations so, you can call them in each class using IOptions pattern
//builder.Services.Configure<TenantSettings>(builder.Configuration.GetSection(nameof(TenantSettings)));
//// Try to initilize options
//TenantSettings options = new();
//// Try to bind tenant instance to configuration values
//builder.Configuration.GetSection(nameof(TenantSettings)).Bind(options);

builder.Services.Configure<PexelSettings>(builder.Configuration.GetSection(nameof(PexelSettings)));

//var defaultProvider = options.Defaults.DbProvider;

//if (defaultProvider.ToLower() == "mssql")
//{
//    builder.Services.AddDbContext<AppDbContext>(d => d.UseSqlServer());
//}

//foreach (var tenant in options.Tenants)
//{
//    //Check connection string for each Tenant
//    var connectionString = tenant.ConnectionString ?? options.Defaults.ConnectionString;
//    //
//    using var scope = builder.Services.BuildServiceProvider().CreateScope();
//    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//    dbContext.Database.SetConnectionString(connectionString);

//    if (dbContext.Database.GetPendingMigrations().Any())
//    {
//        dbContext.Database.Migrate();
//    }
//}



builder.Services.AddHealthChecks()
    .AddCheck<DataBaseHealthCheck>("Database")
    .AddCheck<GitHubHealthCheck>("GitHub");

builder.Services
    .AddHttpClient("Pricing", client =>
    {
        client.BaseAddress = new Uri(builder.Configuration["ApiUrls:Pricing"]);
    })
    .AddPolicyHandler(PolicyConfig.GetRetryPolicy())
    .AddPolicyHandler(PolicyConfig.GetCircuitBreakerPolicy());

builder.Services.AddGrpcClient<Greeter.GreeterClient>(o =>
{
    o.Address = new Uri("http://localhost:5208");   // gRPC Server address
});
//builder.Services.AddODataQueryFilter();

// Code below if you want to determine the ppath when access denied get rendering.
//builder.Services.ConfigureApplicationCookie(opt =>
//{
//   opt.AccessDeniedPath = 
//});


// To enable cross origin resource sharing with adding some security for backend.
builder.Services.AddCors(options =>
{
    options.AddPolicy("ProductionPolicy", policy =>
    {
        policy.WithOrigins(
          "http://localhost:4200/"
        )
        .WithMethods("GET", "POST", "PUT", "DELETE")
        .WithHeaders("Content-Type")
        .AllowCredentials()
        .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapHealthChecks("/_health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseCors("ProductionPolicy");


app.UseAuthorization();

app.UseHangfireDashboard();

app.MapControllers();

app.Run();
