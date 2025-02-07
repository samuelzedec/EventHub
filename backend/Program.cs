using System.Text.Json.Serialization;
using backend.Controllers;
using backend.Data;
using backend.Repositories;
using backend.Services;
using Microsoft.EntityFrameworkCore;
namespace backend;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		LoadConfiguration(builder);
		ConfigureMvc(builder);
		DatabaseSettings(builder);
		ConfigureServices(builder);

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.MapControllers();
		app.Run("https://localhost:8000");
	}

	public static void LoadConfiguration(WebApplicationBuilder builder)
	{
		// O configure faz com que seja possível usar essa configuração como injeção de dependência
		builder.Services.Configure<JwtSettings>(
			builder.Configuration.GetSection("JwtSettings"));
		
		builder.Services.Configure<SmtpSettings>(
			builder.Configuration.GetSection("Smtp"));
	}

	private static void DatabaseSettings(WebApplicationBuilder builder)
	{
		Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection"));
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
	}

	private static void ConfigureServices(WebApplicationBuilder builder)
	{
		builder.Services.AddScoped<UserRepository>();
		builder.Services.AddScoped<AccountService>();
		builder.Services.AddTransient<TokenService>();
		builder.Services.AddTransient<EmailService>();
		builder.Services.AddTransient<PasswordService>();
	}
}