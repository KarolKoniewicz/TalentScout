using Byte.TalentScout.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Byte.TalentScout.API.Middleware;
using Byte.TalentScout.Domain.Entities;
using Byte.TalentScout.Application.Services;
using Byte.TalentScout.Infrastructure.Repositories;
using Byte.TalentScout.Domain.Abstractions.Services;
using Byte.TalentScout.Domain.Abstractions.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddCookie(IdentityConstants.ApplicationScheme)
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddDbContext<TalentScoutIdentityDbContext>(options =>
    options.UseInMemoryDatabase("TalentScoutInMemoryDatabase"));

builder.Services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<TalentScoutIdentityDbContext>()
                .AddApiEndpoints();

builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<IDegreeRepository, DegreeRepository>();
builder.Services.AddScoped<IDegreeService, DegreeService>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}


app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapControllers();
app.UseRouting();
app.Run();


app.Run();
