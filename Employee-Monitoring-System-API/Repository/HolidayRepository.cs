using Employee_Monitoring_System_API.Data;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;

namespace Employee_Monitoring_System_API.Repository
{
    public class HolidayRepository : IHolidayRepository
    {
        private readonly AppDbContext _context;

        public HolidayRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Holiday> GetAllHolidays()
        {
            return _context.Holidays.ToList();
        }

        public IEnumerable<Holiday> GetHolidaysByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.Holidays
                .Where(h => h.Date >= startDate && h.Date <= endDate)
                .ToList();
        }

        public Holiday AddHoliday(Holiday holiday)
        {
            if (holiday == null)
            {
                throw new ArgumentNullException(nameof(holiday));
            }
            _context.Holidays.Add(holiday);
            _context.SaveChanges();
            return holiday;
        }

        public Holiday UpdateHoliday(Holiday holidayChanges)
        {
            if (holidayChanges == null)
            {
                throw new ArgumentNullException(nameof(holidayChanges));
            }
            var existingHoliday = _context.Holidays.Find(holidayChanges.HolidayId);
            if (existingHoliday != null)
            {
                _context.Entry(existingHoliday).CurrentValues.SetValues(holidayChanges);
                _context.SaveChanges();
                return existingHoliday;
            }
            return null;
        }

        public Holiday DeleteHoliday(int id)
        {
            var holiday = _context.Holidays.Find(id);
            if (holiday != null)
            {
                _context.Holidays.Remove(holiday);
                _context.SaveChanges();
                return holiday;
            }
            return null;
        }

        public Holiday GetHolidayById(int id)
        {
            var holiday = _context.Holidays.Find(id);
            if (holiday != null)
            {
                return holiday;
            }
            return null;
        }
    }
}
