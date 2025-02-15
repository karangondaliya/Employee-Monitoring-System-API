using Employee_Monitoring_System_API.Models;
using System.Collections.Generic;

namespace Employee_Monitoring_System_API.Repository.IRepository
{
    public interface IAppsettingRepository
    {
        IEnumerable<AppSettings> GetAll();
        AppSettings GetByKey(string key);
        void Add(AppSettings appSettings);
        void Update(AppSettings appSettingsChanges);
        void Delete(string key);
    }
}
