using System.Text;
using film_management_app.Server;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = "https://localhost:44414",
        ValidAudience = "https://localhost:44414",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("991b2b979968afd96afdf5d737cb27f1cd98922f7bed756c3cfa6fdec2f276aa0121d2e8aaaf49d1e4a316a7d3954a1af7e74a67abb8e859e9a8d2c8a56f9f4e")),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});
builder.Services.AddTransient((sp) => new FilmManagementDbContext());
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IDirectorRepository, DirectorRepository>();
builder.Services.AddTransient<IFilmRepository, FilmRepository>();
builder.Services.AddTransient<IGenreRepository, GenreRepository>();
builder.Services.AddTransient<IStarRepository, StarRepository>();
builder.Services.AddTransient<IActorFilmsService, ActorFilmsService>();
builder.Services.AddTransient<IActorInvitationService, ActorInvitationService>();
builder.Services.AddTransient<IDirectorFilmsService, DirectorFilmsService>();
builder.Services.AddTransient<IFeeNegotiationService, FeeNegotiationService>();
builder.Services.AddTransient<IUserManagementService, UserManagementService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(builder =>
{
    builder.MapControllers();
    builder.MapFallbackToFile("index.html");
});

app.Run();