using Employee_Monitoring_System_API.Data;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;

namespace Employee_Monitoring_System_API.Repository
{
    public class AppsettingRepository : IAppsettingRepository
    {
        private readonly AppDbContext _context;

        public AppsettingRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(AppSettings appSettings)
        {
            _context.AppSettings.Add(appSettings);
            _context.SaveChanges();
        }

        public void Delete(string key)
        {
            AppSettings appsettings = _context.AppSettings.Find(key);
            if (appsettings != null)
            {
                _context.AppSettings.Remove(appsettings);
                _context.SaveChanges();
            }
        }

        public IEnumerable<AppSettings> GetAll()
        {
            return _context.AppSettings.ToList();
        }

        public AppSettings GetByKey(string key)
        {
            return _context.AppSettings.Find(key);
        }

        public void Update(AppSettings appSettingsChanges)
        {
            var appset = _context.AppSettings.Attach(appSettingsChanges);
            appset.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
