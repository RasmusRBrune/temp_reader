using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using temperature_Server.Data;
using temperature_Server.Data.Context;
using temperature_Server.Extensions;

namespace temperature_Server.Repositories
{
    public class TemperatureReaderDeviceKeyRepository : BaseEntityRepository<TemperatureReaderDeviceKey>, ITemperatureReaderDeviceKeyRepository
    {
        public TemperatureReaderDeviceKeyRepository(TempReaderContext context) : base(context)
        {
        }

        public override Task<IEnumerable<TemperatureReaderDeviceKey>> GetAllWithIncludeAsync(Expression<Func<TemperatureReaderDeviceKey, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public override async Task<TemperatureReaderDeviceKey> GetSingleWithIncludeAsync(Expression<Func<TemperatureReaderDeviceKey, bool>> expression)
        {
            return await QueryAll()
                .Include(e => e.Device)
                .WhereIf(expression)
                .FirstOrDefaultAsync();
        }
    }
}
