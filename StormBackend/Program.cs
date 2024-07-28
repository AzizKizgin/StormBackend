using StormBackend.Extensions;
using StormBackend.SignalR;


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
builder.Services.ConfigureServiceManager();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSignalRCore();
builder.Services.AddSignalR();

   builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    });
var app = builder.Build();
app.UseCors("AllowAll");
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
app.MapHub<ChatHub>("/chatHub");
app.Run();