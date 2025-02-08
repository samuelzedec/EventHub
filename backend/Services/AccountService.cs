using backend.Extensions;
using backend.Models;
using backend.Repositories;
using backend.ViewModels;
using backend.ViewModels.Account;
using Microsoft.EntityFrameworkCore;
namespace backend.Services;

public class AccountService
{
    private readonly UserRepository _repository;
    private readonly PasswordService _passwordService;
    private readonly EmailService _emailService;

    public AccountService(
        UserRepository repository,
        PasswordService passwordService,
        EmailService emailService)
    {
        _repository = repository;
        _passwordService = passwordService;
        _emailService = emailService;
    }

    public async Task<(int, ResultViewModel<AccountDetailsViewModel>)> CreateAccountAsync(
        EditorAccountViewModel model)
    {
        var role = await _repository.GetRoleByNameAsync("User");        
        var (passwrod, hash) = _passwordService.GenerateHash();
        var slug = string.IsNullOrWhiteSpace(model.Slug)
            ? string.Concat(model.Email.TakeWhile(x => x != '@'))
            : model.Slug;

        var newAccount = new User
        {
            Username = model.Username,
            Email = model.Email,
            Slug = slug,
            Password = hash,
            CreatedAt = DateTime.Now,
            Roles = [role]
        };

        try
        {
            var accountDetails = new AccountDetailsViewModel();
            var account = await _repository.InsertAsync(newAccount);
            accountDetails.CopyFrom(account);

            var sendEmail = _emailService.Send(
                accountDetails.Username,
                accountDetails.Email,
                "Bem-vindo ao EventHub",
                _emailService.HtmlForEmail(accountDetails.Username, passwrod));

            if (sendEmail is false)
                throw new FormatException("O endereço de e-mail informado é inválido.");

            return (201, new ResultViewModel<AccountDetailsViewModel>(accountDetails));
        }
        catch (DbUpdateException ex) when (ex.Message.Contains("UNIQUE KEY"))
        {
            return (400, new ResultViewModel<AccountDetailsViewModel>("E-mail já está cadastrado!"));
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine(ex.Message);
            return (500, new ResultViewModel<AccountDetailsViewModel>("Erro ao inserir dados no banco de dados!"));
        }
        catch (FormatException ex)
        {
            return (400, new ResultViewModel<AccountDetailsViewModel>(ex.Message));
        }
        catch (Exception)
        {
            return (500, new ResultViewModel<AccountDetailsViewModel>("Erro interno servidor!"));
        }
    }
}