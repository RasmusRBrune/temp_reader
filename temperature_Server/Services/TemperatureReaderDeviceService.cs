using temperature_Server.Data;
using temperature_Server.Repositories;

namespace temperature_Server.Services
{
    public class TemperatureReaderDeviceService : BaseEntityService<TemperatureReaderDevice, Guid>, ITemperatureReaderDeviceService
    {
        private readonly ITemperatureReaderDeviceRepository _TemperatureReaderDeviceRepository;
		private readonly ITemperatureReaderDeviceKeyRepository _keyRepository;

		public TemperatureReaderDeviceService(ITemperatureReaderDeviceRepository repository, ITemperatureReaderDeviceKeyRepository keyRepository) : base(repository)
        {
            _TemperatureReaderDeviceRepository = repository;
			_keyRepository = keyRepository;
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

			var deviceKeyEntity = await _keyRepository.GetSingleAsync(e => e.Key == deviceKey);
			deviceKeyEntity.Device.AccountId = accountId;
			await _TemperatureReaderDeviceRepository.UpdateAsync(deviceKeyEntity.Device);
			return await _TemperatureReaderDeviceRepository.GetSingleAsync(e=>e.Id==deviceKeyEntity.Device.Id);
		}

		//public async Task<TemperatureReaderDevice> FindByUserId(string? UserId)
		//{
		//    return await _TemperatureReaderDeviceRepository.GetSingleAsync(e => e.UserId == UserId);
		//}
	}
}
