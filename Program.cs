using BlogWithMyPrincess.Data.Context;
using BlogWithMyPrincess.Data.Repositories;
using BlogWithMyPrincess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BlogConnectionString")));

builder.Services.AddScoped<IBlogDbContext>(provider =>
    provider.GetRequiredService<BlogDbContext>());
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();


//Agregar CORS
builder.Services.AddCors((options =>
{
    options.AddPolicy("CorePolicy", p =>
    {
        p.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader();
    });
}));
builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("CorePolicy");
app.MapControllers();
app.Run();
