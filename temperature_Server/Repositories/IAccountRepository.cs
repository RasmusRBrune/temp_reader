
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using temperature_Server.Data;
using temperature_Server.Repositories;

namespace temperature_Server.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
    }
}
