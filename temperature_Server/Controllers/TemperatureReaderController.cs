using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using temperature_Server.Services;

namespace temperature_Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TemperatureReaderController : ControllerBase
	{
		private readonly ITemperatureReaderDeviceService _deviceService;
		private readonly ITemperatureReadingService _readingService;
		private readonly IDeviceTimeLogService _timeLogService;

		public TemperatureReaderController(ITemperatureReaderDeviceService deviceService, ITemperatureReadingService readingService, IDeviceTimeLogService timeLogService)
		{
			_deviceService = deviceService;
			_readingService = readingService;
			_timeLogService = timeLogService;
		}

		[HttpGet("GetDeviceById/{id}")]
		public async Task<IActionResult> GetDeviceById(string id)
		{
			try
			{
				var data = await _deviceService.GetSingleAsync(Guid.Parse(id));
				if (data == null)
				{
					return BadRequest("Device could not be found");
				}
				else
				{
					return Ok(data);
				}
			}
			catch (Exception e)
			{

				return Problem(e.Message);
			}
		}

		[HttpPost("UpLoadData")]
		public async Task<IActionResult> UpLoadData(Guid id,DateTime timestamp,float temperature)
		{
			try
			{
				var data = await _readingService.AddAsync(new Data.TemperatureReading { DeviceId = id,TimeStamp=timestamp,Temperature= temperature });
				if (data == null)
				{
					return BadRequest("Device could not be found");
				}
				else
				{
					return Ok(data);
				}
			}
			catch (Exception e)
			{

				return Problem(e.Message);
			}
		}
	}
}
