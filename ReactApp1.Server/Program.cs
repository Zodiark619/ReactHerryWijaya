using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Validations.Rules;
using ReactApp1.Server.Data;
using ReactApp1.Server.Models.Project2Exercise;
using Scalar.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddOpenApi();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.DocumentFilter<BearerSecuritySchemeDocumentFilter>();
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});
var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true

    };
});
var app = builder.Build();
//app.UseCors(x=>x.AllowAnyOrigin()
//.AllowAnyMethod()
//.AllowAnyHeader()
//.WithExposedHeaders("*"));
app.UseCors("AllowAll");
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
     app.UseSwagger();
    app.UseSwaggerUI();
    app.MapScalarApiReference(options =>
    {
         options.OpenApiRoutePattern = "/swagger/{documentName}/swagger.json";

    });
}
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();

//npm install react-redux
//npm install @reduxjs/toolkit
//npm install react-router-dom
//npm install bootstrap@5.3.8
//npm install bootstrap-icons
//npm install react-toastify
//npm install sweetalert2
//npm install jwt-decode










//builder.Services.AddOpenApi(options =>
//{
//    options.addDocumentTransformer<BearerSecuritySchemeTransformer>();
//})
//internal sealed class BearerSecuritySchemeTransformer(Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider authenticationSchemeProvider):IOpenApiDocumentTransformer
//{
//    public async Task TransformAsync(OpenApiDocument document,OpenApiDocumentTransformerContext context,CancellationToken cancellationToken)
//    {
//        var authenticationSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();
//        if (authenticationSchemes.Any(authScheme => authScheme.Name == JwtBearerDefaults.AuthenticationScheme))
//        {
//            var requirement=new Dictionary<string, OpenApiSecurityScheme>
//            {
//                [JwtBearerDefaults.AuthenticationScheme]=new OpenApiSecurityScheme
//                {
//                    Type=SecuritySchemeType.Http,
//                    Scheme="Bearer",
//                    In=ParameterLocation.Header,
//                    BearerFormat="JWT"
//                }
//            };
//            document.Components ??= new OpenApiComponents();
//            document.Components.SecuritySchemes=requirement;
//        }
//        document.Info = new()
//        {
//            Title = "mangofusion_api",
//            Version = "v1",
//            Description = "simple example",

//        };
//    }
//}
internal sealed class BearerSecuritySchemeDocumentFilter : IDocumentFilter
{
    private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;

    public BearerSecuritySchemeDocumentFilter(IAuthenticationSchemeProvider authenticationSchemeProvider)
    {
        _authenticationSchemeProvider = authenticationSchemeProvider;
    }

    public void Apply(OpenApiDocument document, DocumentFilterContext context)
    {
        var authenticationSchemes = _authenticationSchemeProvider
            .GetAllSchemesAsync()
            .GetAwaiter()
            .GetResult();

        if (authenticationSchemes.Any(s => s.Name == JwtBearerDefaults.AuthenticationScheme))
        {
            document.Components ??= new OpenApiComponents();

            document.Components.SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>
            {
                [JwtBearerDefaults.AuthenticationScheme] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // ?? lowercase in Swagger
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization"
                }
            };

            document.SecurityRequirements = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            };
        }

        document.Info = new OpenApiInfo
        {
            Title = "mangofusion_api",
            Version = "v1",
            Description = "simple example"
        };
    }
}