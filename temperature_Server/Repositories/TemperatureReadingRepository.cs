using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using temperature_Server.Data;
using temperature_Server.Data.Context;
using temperature_Server.Extensions;

namespace temperature_Server.Repositories
{
    public class TemperatureReadingRepository : BaseEntityRepository<TemperatureReading>, ITemperatureReadingRepository
    {
        public TemperatureReadingRepository(TempReaderContext context) : base(context)
        {
        }

        public override Task<IEnumerable<TemperatureReading>> GetAllWithIncludeAsync(Expression<Func<TemperatureReading, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public override async Task<TemperatureReading> GetSingleWithIncludeAsync(Expression<Func<TemperatureReading, bool>> expression)
        {
            return await QueryAll()
                .Include(e => e.Device)
                .WhereIf(expression)
                .FirstOrDefaultAsync();
        }
    }
}
