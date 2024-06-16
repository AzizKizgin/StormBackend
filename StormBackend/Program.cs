using StormBackend.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.ConfigureSwaggerGen();
builder.Services.ConfigureControllers();
builder.Services.ConfigureServices();
builder.Services.ConfigureRepositories();
builder.Services.ConfigureRepositoryManager();

// builder.Services.ConfigureServiceManager();
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