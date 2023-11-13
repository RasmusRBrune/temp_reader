
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using temperature_Server.Data;

namespace temperature_Server.Services
{
    public interface ITemperatureReaderDeviceService : IService<TemperatureReaderDevice, Guid>
    {
        //public Task<TemperatureReaderDevice> FindByUserId(string UserId);
    }
}
