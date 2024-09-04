using System.Security.Claims;
using Byte.TalentScout.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Byte.TalentScout.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddCookie(IdentityConstants.ApplicationScheme)
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddDbContext<TalentScoutIdentityDbContext>(options =>
    options.UseInMemoryDatabase("TalentScoutInMemoryDatabase"));

builder.Services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<TalentScoutIdentityDbContext>()
                .AddApiEndpoints();

builder.Services.AddControllers();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.MapIdentityApi<ApplicationUser>();


app.Run();
