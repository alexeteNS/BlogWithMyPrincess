using System.Text;
using BlogWithMyPrincess.Data.Context;
using BlogWithMyPrincess.Data.Repositories;
using BlogWithMyPrincess.Services;
using BlogWithMyPrincess.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BlogConnectionString")));

builder.Services.AddScoped<IBlogDbContext>(provider =>
    provider.GetRequiredService<BlogDbContext>());
builder.Services.AddScoped<ICommentsServices, CommentServices>();
builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IPostRepository, PostsRepository>();
builder.Services.AddScoped<IPostService, PostService>();
//Agregar CORS

var key = Encoding.ASCII.GetBytes("EstaEsUnaClaveMuySeguraYSegura123456!!");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey =  true,
        IssuerSigningKey =  new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = "MiBlogAPI",   
        ValidAudience = "MiBlogClient",
        ClockSkew =  TimeSpan.Zero,
    };
});

builder.Services.AddAuthorization();
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

app.UseRouting();
app.UseCors("CorePolicy");
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
