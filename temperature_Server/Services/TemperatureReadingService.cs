using temperature_Server.Data;
using temperature_Server.Repositories;

namespace temperature_Server.Services
{
    public class TemperatureReadingService : BaseEntityService<TemperatureReading, int>, ITemperatureReadingService
    {
        private readonly ITemperatureReadingRepository _TemperatureReadingRepository;
        public TemperatureReadingService(ITemperatureReadingRepository repository) : base(repository)
        {
            _TemperatureReadingRepository = repository;
        }

        //public async Task<TemperatureReading> FindByUserId(string? UserId)
        //{
        //    return await _TemperatureReadingRepository.GetSingleAsync(e => e.UserId == UserId);
        //}
    }
}
