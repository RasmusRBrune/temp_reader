using Microsoft.Identity.Client;
using temperature_Server.Data;
using temperature_Server.Repositories;

namespace temperature_Server.Services
{
    public class TemperatureReaderDeviceService : BaseEntityService<TemperatureReaderDevice, Guid>, ITemperatureReaderDeviceService
    {
        private readonly ITemperatureReaderDeviceRepository _TemperatureReaderDeviceRepository;
		private readonly ITemperatureReaderDeviceKeyRepository _keyRepository;
        private readonly IDeviceTimeLogRepository timeLogRepository;
        private readonly ITemperatureReadingRepository temperatureReadingRepository;

        public TemperatureReaderDeviceService(ITemperatureReaderDeviceRepository repository, ITemperatureReaderDeviceKeyRepository keyRepository, IDeviceTimeLogRepository timeLogRepository, ITemperatureReadingRepository temperatureReadingRepository) : base(repository)
        {
            _TemperatureReaderDeviceRepository = repository;
			_keyRepository = keyRepository;
            this.timeLogRepository = timeLogRepository;
            this.temperatureReadingRepository = temperatureReadingRepository;
        }

        public override async Task<TemperatureReaderDevice> AddAsync(TemperatureReaderDevice entity)
        {
            var device = await _TemperatureReaderDeviceRepository.AddAsync(entity);
			var key = await _keyRepository.AddAsync(new()
			{
				DeviceId = device.Id,
				Key = DateTime.Now.ToString()
			});
			
			return await _TemperatureReaderDeviceRepository.GetSingleAsync(e=>e.Id==key.DeviceId);

			
        }

        public async Task<TemperatureReaderDevice> PairWithAccount(string deviceKey, Guid accountId)
		{

			var deviceKeyEntity = await _keyRepository.GetSingleWithIncludeAsync(e => e.Key == deviceKey);
			deviceKeyEntity.Device.AccountId = accountId;
			await _TemperatureReaderDeviceRepository.UpdateAsync(deviceKeyEntity.Device);
			return await _TemperatureReaderDeviceRepository.GetSingleAsync(e=>e.Id==deviceKeyEntity.Device.Id);
		}

        public async Task<TemperatureReaderDevice> WipeDataAsync(Guid deviceId)
        {
            var device = await GetSingleAsync(deviceId);
            var test = device.Id;
            var test1 = device.ReadingLogs.First().Id;
            var test2 = device.ReadingLogs.Last().Id;
            var test3 = device.TimeLogs.First().Id;
            var test4 = device.TimeLogs.Last().Id;

            foreach (var item in device.ReadingLogs)
            {
                await temperatureReadingRepository.DeleteAsync(e=>e.Id==item.Id);
            }
            foreach (var item in device.TimeLogs)
            {
                await timeLogRepository.DeleteAsync(e => e.Id == item.Id);
            }
            return await _TemperatureReaderDeviceRepository.GetSingleAsync(e => e.Id == deviceId);
        }

        //public async Task<TemperatureReaderDevice> FindByUserId(string? UserId)
        //{
        //    return await _TemperatureReaderDeviceRepository.GetSingleAsync(e => e.UserId == UserId);
        //}
    }
}
