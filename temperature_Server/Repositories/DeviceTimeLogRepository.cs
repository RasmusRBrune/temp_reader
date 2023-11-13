using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using temperature_Server.Data;
using temperature_Server.Data.Context;
using temperature_Server.Extensions;

namespace temperature_Server.Repositories
{
    public class DeviceTimeLogRepository : BaseEntityRepository<DeviceTimeLog>, IDeviceTimeLogRepository
    {
        public DeviceTimeLogRepository(TempReaderContext context) : base(context)
        {
        }

        public override Task<IEnumerable<DeviceTimeLog>> GetAllWithIncludeAsync(Expression<Func<DeviceTimeLog, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public override async Task<DeviceTimeLog> GetSingleWithIncludeAsync(Expression<Func<DeviceTimeLog, bool>> expression)
        {
            return await QueryAll()
                .Include(e => e.Device)
                .WhereIf(expression)
                .FirstOrDefaultAsync();
        }
    }
}
