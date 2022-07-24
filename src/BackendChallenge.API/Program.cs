using BackendChallenge.API.Extensions;
using BackendChallenge.API.Middlewares;
using BackendChallenge.Core.Mapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(AutoMapperSetup));

//DI extensions
builder.Services.AddSwagger();
builder.Services.AddServiceInterfaces();
builder.Services.AddRepositoryInterfaces();
builder.Services.AddDbContext(builder.Configuration);
builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.ConfigureHttpClient(builder.Configuration);

var app = builder.Build();

//if (app.Environment.IsDevelopment()){}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<UserIdentifierMiddleware>();

app.MapControllers();

app.Run();