using DatingApp.api.Data;
using DatingApp.api.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connString = builder.Configuration.GetConnectionString("DefaultConnection");
string Key = builder.Configuration.GetSection("AppSettings:Token").Value;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>

(x => x.UseSqlServer(connString)
);
builder.Services.AddCors();

builder.Services.AddScoped<IAuthRepository, AuthRepository>();

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


app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

app.Run();
