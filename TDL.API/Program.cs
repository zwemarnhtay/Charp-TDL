using TDL.Application;
using TDL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

string key = builder.Configuration.GetSection("JWT:Key").Value!;
string issuer = builder.Configuration.GetSection("JWT:Issuer").Value!;
string audience = builder.Configuration.GetSection("JWT:Audience").Value!;

// Add services to the container.

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

//builder.Services.AddAuthentication(x =>
//{
//  x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//  x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//  x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(x =>
//{
//  x.TokenValidationParameters = new TokenValidationParameters
//  {
//    ValidateIssuer = true,
//    ValidateAudience = true,
//    ValidateLifetime = true,
//    ValidateIssuerSigningKey = true,
//    ValidIssuer = issuer,
//    ValidAudience = audience,
//    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
//  };
//});

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
