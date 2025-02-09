using backend.Exceptions;
using backend.Extensions;
using backend.Models;
using backend.Repositories;
using backend.ViewModels;
using backend.ViewModels.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace backend.Services;

public class AccountService
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
        var hash = _passwordService.GenerateHash();
        var slug = string.IsNullOrWhiteSpace(model.Slug)
            ? string.Concat(model.Email.TakeWhile(x => x != '@')) + $"{new Random(Guid.NewGuid().GetHashCode()).Next(10000, 100000)}"
            : model.Slug;

        var newAccount = new User
        {
            Username = model.Username,
            Email = model.Email,
            Slug = slug,
            Password = hash,
            Roles = [role]
        };

        try
        {
            var accountDetails = new AccountDetailsViewModel();
            var account = await _repository.InsertAsync(newAccount);
            accountDetails.CopyFrom(account);

            var verificationCode = await _checkingEmailService.GenerationCodeAsync(account.Email);
            var sendEmail = _emailService.Send(
                account.Username,
                account.Email,
                "Código de Verificação",
                _emailService.HtmlForEmailVerification(account.Username, verificationCode, _originAllowed));

            return (201, new ResultViewModel<AccountDetailsViewModel>(accountDetails));
        }
        catch (DbUpdateException ex) when (ex.Message.Contains("UNIQUE KEY"))
        {
            return (400, new ResultViewModel<AccountDetailsViewModel>("E-mail já está cadastrado!"));
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
            return (500, new ResultViewModel<AccountDetailsViewModel>("Erro ao inserir dados no banco de dados!"));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return (500, new ResultViewModel<AccountDetailsViewModel>("Erro interno servidor!"));
        }
    }
}