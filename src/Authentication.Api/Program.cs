using Authentication.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServicesApplication();
builder.Services.AddRepositoryApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapGroup("/api/v1/").MapControllers();
app.MapControllers();

app.Run();
