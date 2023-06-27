using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Services;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CommandsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CommandsContext") ?? throw new InvalidOperationException("Connection string 'CommandsContext' not found.")));

builder.Services.AddScoped<IEmployee, EmployeeService>();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IRole, RoleService>();
builder.Services.AddScoped<ITableRestaurant, TableService>();
builder.Services.AddScoped<ICommands, CommandService>();
builder.Services.AddScoped<IDetailsCommand, DetailsCommandService>();
builder.Services.AddScoped<IDish, DishService>();
builder.Services.AddScoped<ICategoryDish, CategoryDishService>();
builder.Services.AddScoped<IPayMethod, PaymethodService>();
builder.Services.AddScoped<ICash, CashService>();
builder.Services.AddScoped<IEstablishment, EstablishmentService>();
builder.Services.AddScoped<IVoucher, VoucherServices>();
builder.Services.AddScoped<ICustomer, CustomerService>();
builder.Services.AddScoped<IVoucherType, VoucherTypeServices>();
builder.Services.AddScoped<IVoucherDetail, VoucherDetailServices>();
builder.Services.AddScoped<IReport, ReportService>();
builder.Services.AddScoped<IEmail, EmailService>();

// Add services to the container.
builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
         options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter())
    )
    .AddJsonOptions(options =>
    {
        // Que ignore la referencias circulares
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

        // Incluir identaci�n
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// A�adimos informaci�n a Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API - Project Backend",
        Version = "v1",
        Description = "API de Sistema de Comandas"
    });

    // Agregar la configuraci�n de autenticaci�n JWT en Swagger
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Ingrese el token JWT obtenido al iniciar sesi�n.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    // A�adimos la definici�n de seguridad
    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        { securityScheme, new[] { "Bearer" } }
    };

    c.AddSecurityRequirement(securityRequirement);
});

// Se añade las políticas de CORS para que solo acepte las peticiones del origen del frontend
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
    policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin()
              .WithExposedHeaders("Content-Disposition");
    }
));

// Se a�ade una autenticaci�n con el esquema 'Bearer'
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(config =>
    {
        // Configuraci�n para validar el token
        config.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JWTSettings:Issuer"], //Se extrae de appsettings.json
            ValidAudience = builder.Configuration["JWTSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:SecretKey"])),
        };
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<CommandsContext>();

    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
