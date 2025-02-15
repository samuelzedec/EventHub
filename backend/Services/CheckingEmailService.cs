using backend.Exceptions;
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
			throw new ConflictException("E-mail já está em uso. Faça login para acessar!");

		await _repository.InsertAsync(new VerificationCode
		{
			Code = code,
			UserEmail = userEmail,
			Duration = DateTime.Now.AddHours(1)
		});

		return code;
	}

	public async Task<(string, bool)> Validation(string userEmail, int code)
	{
		var verificationByUserEmail = await _repository
			.GetVerificationCodeByUserEmailAsync(userEmail);
		
		if(verificationByUserEmail is null)
			return ("Email não encontrado para validação!", false);

		if (verificationByUserEmail.Code == code)
			return ("Email verificado", true);
		
		return ("Código Inválido", false);
	} 
}