using DatingApp.api.Data;
using DatingApp.api.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Diagnostics;
using DatingApp.api.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connString = builder.Configuration.GetConnectionString("DefaultConnection");
string Key = builder.Configuration.GetSection("AppSettings:Token").Value;

builder.Services.AddControllers().AddJsonOptions(opt =>
                opt.JsonSerializerOptions.ReferenceHandler =
                     System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>
(x => x.UseSqlServer(connString)
);
builder.Services.AddAutoMapper(opt=> opt.AddProfile(new AutoMapperProfiles()));
builder.Services.AddCors();

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IDatingRepository, DatingRepository>();
builder.Services.AddTransient<Seed>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key)),
               ValidateIssuer = false,
               ValidateAudience = false
           };
       });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler(builder=>{
        builder.Run(async context=>{
            context.Response.StatusCode= (int)HttpStatusCode.InternalServerError;
            var error=context.Features.Get<IExceptionHandlerFeature>();
            if(error!=null){
                    await context.Response.WriteAsync(error.Error.Message) ;
            }
        });
    });
}

// app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
// use this to seed your usera
//app.Services.GetService<Seed>().SeedUsers();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

app.Run();
