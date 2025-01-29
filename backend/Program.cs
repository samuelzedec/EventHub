using backend.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;


internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		var connectionString = builder.Configuration.GetConnectionString("Default");

		ConfigureMvc(builder);
		DatabaseSettings(builder, connectionString);

		var app = builder.Build();
		app.Run();
	}

	private static void DatabaseSettings(
		WebApplicationBuilder builder,
		string? connectionString)
	{
		builder.Services.AddDbContext<EventHubDbContext>(
			options => options
				.UseSqlServer(connectionString) // Injetando a conexão do banco
				.EnableSensitiveDataLogging(false) // Desabilita dados sensíveis nos logs
				.LogTo(Console.WriteLine, LogLevel.Error)); // Exibindo somente erros no terminal		
	}

	private static void ConfigureMvc(WebApplicationBuilder builder)
	{
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
}