using CleanArchitectureTemplate.API.DependencyInjections;
using CleanArchitectureTemplate.API.Middlewares;
using CleanArchitectureTemplate.Application.DependencyInjections;

var builder = WebApplication.CreateBuilder(args);

// Add services.
builder.Services.ConfigureAPIServices(builder.Configuration);
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure middleware.
app.UseSwagger();
app.UseHttpsRedirection();
app.UseCors();

// Configure custom middlewares
app.UseMiddleware<ExceptionMiddleware>();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Initialize and run the app.
app.InitializeInfrastructure();
app.InitializeBackgroundJobs();

app.Run();

