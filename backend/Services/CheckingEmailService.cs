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
		var verificationByUserEmail = await _repository.GetVerificationCodeByUserEmailAsync(userEmail);
		var code = new Random(Guid.NewGuid().GetHashCode()).Next(100000, 1000000);

		if (verificationByUserEmail is not null)
			throw new ConflictException("E-mail já estáe em uso, porém, não verificado!");

		await _repository.InsertAsync(new VerificationCode
		{
			Code = code,
			UserEmail = userEmail,
		});

		return code;
	} 
}