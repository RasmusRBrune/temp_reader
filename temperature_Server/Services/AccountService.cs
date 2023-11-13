using temperature_Server.Data;
using temperature_Server.Repositories;

namespace temperature_Server.Services
{
    public class AccountService : BaseEntityService<Account, Guid>, IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository repository) : base(repository)
        {
            _accountRepository = repository;
        }

        public async Task<Account> FindByUserId(string? UserId)
        {
            return await _accountRepository.GetSingleAsync(e => e.UserId == UserId);
        }
    }
}
