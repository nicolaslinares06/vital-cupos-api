using API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.Helpers;
using Repository.Persistence.IRepository;
using Repository.Persistence.Repository;
using System.Security.Cryptography;
using static Repository.Helpers.Models.ModelsAppSettings;

var builder= WebApplication.CreateBuilder(args);

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

// Add services to the container.

builder.Services.Configure<EstadosCuposSettings>(builder.Configuration.GetSection("EstadosCupos"));
builder.Services.AddTransient<IActaVisitasCortesRepository, ActaVisitasCortesRepository>();
builder.Services.AddTransient<ICoordinatorAssignRequestAnalystsGpnRepository, CoordinatorAssignRequestAnalystsGpnRepository>();
builder.Services.AddTransient<IReportesEmpresasMarcaje, ReportesEmpresasMarcajesRepository>();
builder.Services.AddTransient<IReportesPrecintosRepository, ReportesPrecintosRepository>();
builder.Services.AddTransient<IGenericsMethodsHelper, GenericsMethodsHelper>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

///////////////////////////////////////////////////
var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);

///////////////////////////////////////////////////
// Crea el manager de JWT
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
        IssuerSigningKey = new ECDsaSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddSingleton<JwtAuthenticationManager>(new JwtAuthenticationManager(key));

//VARIABLES DE ENTORNO CONEXION DB
string SERVER = Environment.GetEnvironmentVariable("SERVER") ?? "";
string PORT = Environment.GetEnvironmentVariable("PORT") ?? "";
string DATABASE = Environment.GetEnvironmentVariable("DATABASE") ?? ""; 
string USERNAME = Environment.GetEnvironmentVariable("USERNAME") ?? ""; 
string PASSWORD = Environment.GetEnvironmentVariable("PASSWORD") ?? ""; 

if (string.IsNullOrEmpty(SERVER) || string.IsNullOrEmpty(PORT) || string.IsNullOrEmpty(DATABASE) || string.IsNullOrEmpty(USERNAME) || string.IsNullOrEmpty(PASSWORD))
{
    builder.Services.AddDbContext<DBContext>(options => {
        options.UseSqlServer(builder.Configuration.GetConnectionString("MinAmbienteDB"));
    });

}
else
{
    builder.Services.AddDbContext<DBContext>(options => {
        options.UseSqlServer($"Data Source={SERVER},{PORT};Initial Catalog={DATABASE};Persist Security Info=True;User ID={USERNAME};Password={PASSWORD};MultipleActiveResultSets=true");
    });
}

builder.Services.AddCors(op => {
    op.AddPolicy("CordPolicy", p =>
    {
        p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<DBContext>();
}

// Agrega el middleware de manejo de excepciones aquí.
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("CordPolicy");

app.MapControllers();

app.Run();