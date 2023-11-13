
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using temperature_Server.Data;

namespace temperature_Server.Services
{
    public interface IDeviceTimeLogService : IService<DeviceTimeLog, int>
    {
        //public Task<DeviceTimeLog> FindByUserId(string UserId);
    }
}
