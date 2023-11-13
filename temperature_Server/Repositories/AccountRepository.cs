using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using temperature_Server.Data;
using temperature_Server.Data.Context;
using temperature_Server.Extensions;

namespace temperature_Server.Repositories
{
    public class AccountRepository : BaseEntityRepository<Account>, IAccountRepository
    {
        public AccountRepository(TempReaderContext context) : base(context)
        {
        }

        public override Task<IEnumerable<Account>> GetAllWithIncludeAsync(Expression<Func<Account, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public override async Task<Account> GetSingleWithIncludeAsync(Expression<Func<Account, bool>> expression)
        {
            return await QueryAll()
                .Include(e => e.Devices)
                .WhereIf(expression)
                .FirstOrDefaultAsync();
        }
    }
}
