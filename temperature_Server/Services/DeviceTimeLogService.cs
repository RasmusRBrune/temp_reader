using temperature_Server.Data;
using temperature_Server.Repositories;

namespace temperature_Server.Services
{
    public class DeviceTimeLogService : BaseEntityService<DeviceTimeLog, int>, IDeviceTimeLogService
    {
        private readonly IDeviceTimeLogRepository _DeviceTimeLogRepository;
        public DeviceTimeLogService(IDeviceTimeLogRepository repository) : base(repository)
        {
            _DeviceTimeLogRepository = repository;
        }

        //public async Task<DeviceTimeLog> FindByUserId(string? UserId)
        //{
        //    return await _DeviceTimeLogRepository.GetSingleAsync(e => e.UserId == UserId);
        //}
    }
}
