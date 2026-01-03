using TwitterApp.Helpers.DIContainer;
using TwitterApp.Helpers.Extensions;

var builder = WebApplication.CreateBuilder(args);

var appSettings = builder.Configuration.GetSection("AppSettings");

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Custom extensions
builder.Services.AddPostgreSqlDbContext(appSettings)
                .AddIdentity()
                .AddAuthentication()
                .AddJwt(appSettings)
                .AddCors()
                .AddSwagger();

// Dependency injection for repositories and services
DIHelper.InjectDbRepositories(builder.Services);
DIHelper.InjectServices(builder.Services);

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CORSPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
