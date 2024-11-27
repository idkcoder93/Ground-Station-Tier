////var builder = WebApplication.CreateBuilder(args);

////// Add services to the container.

////builder.Services.AddControllers();
////// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
////builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen();
////builder.Services.AddHttpClient();

////var app = builder.Build();

////// Configure the HTTP request pipeline.
////if (app.Environment.IsDevelopment())
////{
////    app.UseSwagger();
////    app.UseSwaggerUI();
////}

////app.UseHttpsRedirection();

////app.UseAuthorization();

////app.MapControllers();

////app.Run();


//using Microsoft.AspNetCore.Mvc;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllers().AddNewtonsoftJson(options =>
//{
//    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
//});

//// Add HttpClient
//builder.Services.AddHttpClient();

//// Add Authentication/Authorization
//builder.Services.AddAuthentication(); // Customize based on your auth mechanism.
//builder.Services.AddAuthorization();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();

//app.Run();


var builder = WebApplication.CreateBuilder(args);

// Add HttpClient to the DI container
builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger services.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();


// Enable Swagger middleware only in development mode.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty; // This sets Swagger UI at the app's root.

    });

}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
