using backend.ViewModels;
using backend.ViewModels.Account;
namespace backend.Interfaces;

public interface IAccountService
{
    Task<(int, ResultViewModel<AccountDetailsViewModel>)> CreateAccountAsync(EditorAccountViewModel model);
}