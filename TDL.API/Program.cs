using TDL.Application;
using TDL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

string key = builder.Configuration.GetSection("JWT:Key").Value!;
string issuer = builder.Configuration.GetSection("JWT:Issuer").Value!;
string audience = builder.Configuration.GetSection("JWT:Audience").Value!;

// Add services to the container.

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

//to enable CORS in blazor web app
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
  options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                      policy.WithOrigins("https://localhost:7092",
                                            "http://localhost:5096")
                             .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
                             .AllowAnyHeader();
                    });
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//for CORS enable
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
