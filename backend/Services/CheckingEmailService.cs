using backend.Models;
using backend.Repositories;

namespace backend.Services;
public class CheckingEmailService
{
	private readonly VerificationCodeRepository _repository;
	public CheckingEmailService(VerificationCodeRepository repository)
		=> _repository = repository;

	public async Task<int> GenerationCodeAsync(string userEmail)
	{
		var verificationByUserEmail = await _repository
			.GetVerificationCodeByUserEmailAsync(userEmail);

		var code = new Random(Guid.NewGuid().GetHashCode()).Next(100000, 1000000);

		if (verificationByUserEmail is not null)
		{
			verificationByUserEmail.Code = code;
			return code;
		}

		await _repository.InsertAsync(new VerificationCode
		{
			Code = code,
			UserEmail = userEmail,
			Duration = DateTime.Now.AddHours(1)
		});

		return code;
	}

	public async Task<(int, bool)> RegenerationCodeAsync(string userEmail)
	{
		var verificationByUserEmail = await _repository
			.GetVerificationCodeByUserEmailAsync(userEmail);
		
		var code = new Random(Guid.NewGuid().GetHashCode()).Next(100000, 1000000);

		if (verificationByUserEmail is null)
			return (0, false);

		verificationByUserEmail.Code = code;
		await _repository.UpdateByUserEmailAsync(userEmail, verificationByUserEmail);
		return (code, true);
	}

	public async Task<(string, bool)> ValidationAsync(string userEmail, int code)
	{
		var verificationByUserEmail = await _repository
			.GetVerificationCodeByUserEmailAsync(userEmail);

		if (verificationByUserEmail is null)
			return ("E-mail não encontrado para validação!", false);

		if (verificationByUserEmail.Code != code)
			return ("Código inválido!", false);

		if (DateTime.Now > verificationByUserEmail.Duration)
			return ("Código de verificação expirado!", false);

		return ("E-mail verificado com sucesso... Faça o login!", true);
	} 
}