using Authentication.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServicesApplication();
builder.Services.AddRepositoryApplication();
builder.Services.AddInfrastructure(builder.Configuration);
//Add authorization
builder.Services.AddAuthenticationApplication(builder.Configuration);
builder.Services.AddCustomAuthorizationPolicies();
builder.Services.AddCORSApplication(builder.Configuration, builder.Environment);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Apply the CORS policy
app.UseCors("AllowSpecificOrigin");

// Enables authentication
app.UseAuthentication();

// Enables authorization
app.UseAuthorization();

//app.MapGroup("/api/v1/").MapControllers();
app.MapControllers();

app.Run();
