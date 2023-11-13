using temperature_Server.Data;
using temperature_Server.Repositories;

namespace temperature_Server.Services
{
    public class TemperatureReaderDeviceService : BaseEntityService<TemperatureReaderDevice, Guid>, ITemperatureReaderDeviceService
    {
        private readonly ITemperatureReaderDeviceRepository _TemperatureReaderDeviceRepository;
        public TemperatureReaderDeviceService(ITemperatureReaderDeviceRepository repository) : base(repository)
        {
            _TemperatureReaderDeviceRepository = repository;
        }

        //public async Task<TemperatureReaderDevice> FindByUserId(string? UserId)
        //{
        //    return await _TemperatureReaderDeviceRepository.GetSingleAsync(e => e.UserId == UserId);
        //}
    }
}
