using backend.Exceptions;
using backend.Extensions;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using backend.ViewModels;
using backend.ViewModels.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace backend.Services;

public class AccountService : IAccountService
{
    private readonly UserRepository _repository;
    private readonly PasswordService _passwordService;
    private readonly EmailService _emailService;
    private readonly CheckingEmailService _checkingEmailService;
    private readonly string _originAllowed;

    public AccountService(
        UserRepository repository,
        PasswordService passwordService,
        EmailService emailService,
        CheckingEmailService checkingEmailService,
        IOptions<OriginAllowedSettings> originAllowrd)
    {
        _repository = repository;
        _passwordService = passwordService;
        _emailService = emailService;
        _checkingEmailService = checkingEmailService;
        _originAllowed = originAllowrd.Value.Main;
    }

    public async Task<(int, ResultViewModel<AccountDetailsViewModel>)> CreateAccountAsync(
        EditorAccountViewModel model)
    {
        var role = await _repository.GetRoleByNameAsync("User");
        var newAccount = new User
        {
            Username = model.Username,
            Email = model.Email,
            Slug = GenerateSlug(model.Email, model.Slug),
            CreatedAt = DateTime.Now,
            Roles = [role]
        };

        try
        {
            var accountDetails = new AccountDetailsViewModel();
            var account = await _repository.InsertAsync(newAccount);
            accountDetails.CopyFrom(account);

            var verificationCode = await _checkingEmailService.GenerationCodeAsync(account.Email);
            _emailService.Send(
                account.Username,
                account.Email,
                "Código de Verificação",
                _emailService.HtmlForEmailVerification(account.Username, $"{_originAllowed}/validation-page?email={account.Email}&code={verificationCode}"));

            return (201, new ResultViewModel<AccountDetailsViewModel>(accountDetails));
        }
        catch (ConflictException ex)
        {
            return (409, new ResultViewModel<AccountDetailsViewModel>(ex.Message));
        }
        catch (FormatException ex)
        {
            return (400, new ResultViewModel<AccountDetailsViewModel>(ex.Message));
        }
        catch (DbUpdateException)
        {
            return (500, new ResultViewModel<AccountDetailsViewModel>("E-mail já está cadastrado!"));
        }
        catch (Exception)
        {
            return (500, new ResultViewModel<AccountDetailsViewModel>("Erro interno servidor!"));
        }
    }

    private string GenerateSlug(string email, string? providedSlug)
    {
        if (!string.IsNullOrWhiteSpace(providedSlug))
            return providedSlug;

        return string.Concat(email.TakeWhile(x => x != '@')) +
            $"{new Random(Guid.NewGuid().GetHashCode()).Next(10000, 100000)}";
    }

    public async Task<(int, ResultViewModel<string>)> ValidatingTheEmailCode(string email, int code)
    {
        var repository = await _repository.GetByEmailAsync(email);
        var (password, hash) = _passwordService.GenerateHash();

        if (repository == null)
            return (404, new ResultViewModel<string>(["Email não encontrado!"]));

        if (repository.IsEmailVerified)
            return (200, new ResultViewModel<string>(data: "Email já está verificado"));

        var (message, result) = await _checkingEmailService.ValidationAsync(email, code);
        if (result)
        {
            repository.IsEmailVerified = true;
            repository.Password = hash;
            await _repository.UpdateAsync(repository.Id, repository);
            _emailService.Send(
                repository.Username,
                repository.Email,
                "Senha de Acesso",
                _emailService.HtmlForEmail(repository.Username, password));
            return (200, new ResultViewModel<string>(data: message));
        }
        if (message.Contains("expirado"))
            return (410, new ResultViewModel<string>([message]));

        if (message.Contains("validação"))
            return (404, new ResultViewModel<string>([message]));

        return (422, new ResultViewModel<string>([message]));
    }

    public async Task<(int, ResultViewModel<string>)> ResendCodeAsync(string email)
    {
        var account = await _repository.GetByEmailAsync(email);
        var (verificationCode, result) = await _checkingEmailService.RegenerationCodeAsync(email);

        if (account is null)
            return (404, new ResultViewModel<string>(["E-mail não encontrado!"]));

        if (result)
        {
            _emailService.Send(
                account.Username,
                account.Email,
                "Código de Verificação",
                _emailService.HtmlForEmailVerification(account.Username, $"{_originAllowed}/validation-page?email={account.Email}&code={verificationCode}"));

            return (200, new ResultViewModel<string>(data: "Código enviado com sucesso!"));
        }
        return (404, new ResultViewModel<string>(["E-mail não encontrado!"]));
    }
}