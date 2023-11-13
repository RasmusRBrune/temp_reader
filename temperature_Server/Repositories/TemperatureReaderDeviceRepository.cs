using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using temperature_Server.Data;
using temperature_Server.Data.Context;
using temperature_Server.Extensions;

namespace temperature_Server.Repositories
{
    public class TemperatureReaderDeviceRepository : BaseEntityRepository<TemperatureReaderDevice>, ITemperatureReaderDeviceRepository
    {
        public TemperatureReaderDeviceRepository(TempReaderContext context) : base(context)
        {
        }

        public override Task<IEnumerable<TemperatureReaderDevice>> GetAllWithIncludeAsync(Expression<Func<TemperatureReaderDevice, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public override async Task<TemperatureReaderDevice> GetSingleWithIncludeAsync(Expression<Func<TemperatureReaderDevice, bool>> expression)
        {
            return await QueryAll()
                .Include(e => e.Account)
                .Include(e => e.ReadingLogs)
                .Include(e => e.TimeLogs)
                .WhereIf(expression)
                .FirstOrDefaultAsync();
        }
    }
}
