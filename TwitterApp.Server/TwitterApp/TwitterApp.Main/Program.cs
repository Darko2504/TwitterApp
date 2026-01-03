using TwitterApp.Helpers.DIContainer;
using TwitterApp.Helpers.Extensions;
using TwitterApp.Mappers;

var builder = WebApplication.CreateBuilder(args);

var appSettings = builder.Configuration.GetSection("AppSettings");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);

builder.Services.AddPostgreSqlDbContext(builder.Configuration)
                .AddIdentity()
                .AddAuthentication()
                .AddJwt(builder.Configuration)
                .AddCors()
                .AddSwagger();

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
