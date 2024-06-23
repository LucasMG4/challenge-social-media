using G4.Infraestructure.DependencyInjection;
using G4.WebApplication.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddAutoMapperConfig();
builder.Services.AddInfrastructure(builder.Configuration);
builder.AddJWT();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");

app.Run();
