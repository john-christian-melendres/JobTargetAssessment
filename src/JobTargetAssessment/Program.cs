using JobTargetAssessment.Domain;
using JobTargetAssessment.Application;
using JobTargetAssessment.Persistence;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
    builder.Services.AddSingleton<JsonDbContext>();
    builder.Services.AddSingleton<IUserRepository, UserRepository>();
    builder.Services.AddSingleton<IUserService, UserService>();
}

var app = builder.Build();
{
    app.UseCors("AllowReactApp");
    app.MapControllers();
    app.UseStaticFiles();
    app.MapFallbackToFile("index.html");
}

app.Run();