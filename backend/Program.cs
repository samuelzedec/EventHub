using System.Text;
using System.Text.Json.Serialization;
using backend.Data;
using backend.Repositories;
using backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
namespace backend;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		LoadConfiguration(builder);
		ConfigureAuthentication(builder);
		ConfigureMvc(builder);
		ConfigureDatabase(builder);
		ConfigureServices(builder);

		var app = builder.Build();
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}
		app.UseHttpsRedirection();
		app.UseCors("SharingPolicy");
		app.UseAuthentication();
		app.UseAuthorization();
		app.MapControllers();
		app.Run();
	}

	public static void LoadConfiguration(WebApplicationBuilder builder)
	{
		builder
			.Services
			.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"))
			.Configure<SmtpSettings>(builder.Configuration.GetSection("Smtp"))
			.Configure<OriginAllowedSettings>(builder.Configuration.GetSection("OriginAllowed"));
	}

	private static void ConfigureDatabase(WebApplicationBuilder builder)
	{
		builder.Services.AddDbContext<EventHubDbContext>(
			options => options
				.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")) // Injetando a conexão do banco
				.EnableSensitiveDataLogging(false) // Desabilita dados sensíveis nos logs
				.LogTo(Console.WriteLine, LogLevel.Error)); // Exibindo somente erros no terminal		
	}

	private static void ConfigureMvc(WebApplicationBuilder builder)
	{
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		builder.Services
			.AddControllers()
			.ConfigureApiBehaviorOptions(options =>
				options.SuppressModelStateInvalidFilter = true)
			.AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
				options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
			});
		builder.Services
			.AddCors(options =>
			{
				options.AddPolicy("SharingPolicy", policy =>
				{
					policy.WithOrigins(builder.Configuration.GetValue<string>("OriginAllowed:Main")!) // Define as origens permitidas a partir da configuração
					.AllowAnyHeader() // Permite qualquer cabeçalho na requisição
					.AllowAnyMethod(); // Permite qualquer método HTTP (GET, POST, PUT, DELETE, etc.)
				});
			});
	}

	private static void ConfigureAuthentication(WebApplicationBuilder builder)
	{
		var jwtSettings = builder
			.Configuration
			.GetSection("JwtSettings")
			.Get<JwtSettings>()!;

		builder.Services
			.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
					ValidateIssuer = false,
					ValidateAudience = false
				};

				options.Events = new JwtBearerEvents
				{
					OnAuthenticationFailed = context =>
					{
						Console.WriteLine($"Token inválido: {context.Exception.Message}");
						return Task.CompletedTask;
						// Aqui estamos dizendo que o evento da falha de autenticação está concluída e pode prosseguir
					}
				};
			});
	}

	private static void ConfigureServices(WebApplicationBuilder builder)
	{
		builder.Services.AddScoped<UserRepository>();
		builder.Services.AddScoped<VerificationCodeRepository>();
		builder.Services.AddScoped<AccountService>();
		builder.Services.AddTransient<TokenService>();
		builder.Services.AddTransient<EmailService>();
		builder.Services.AddTransient<CheckingEmailService>();
		builder.Services.AddTransient<PasswordService>();
	}
}