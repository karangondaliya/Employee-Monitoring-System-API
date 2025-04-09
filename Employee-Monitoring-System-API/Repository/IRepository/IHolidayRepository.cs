using Employee_Monitoring_System_API.Models;

namespace Employee_Monitoring_System_API.Repository.IRepository
{
    public interface IHolidayRepository
    {
        Holiday AddHoliday(Holiday holiday);
        Holiday UpdateHoliday(Holiday holidayChanges);
        Holiday DeleteHoliday(int id);
        IEnumerable<Holiday> GetAllHolidays();
        Holiday GetHolidayById(int id);
        IEnumerable<Holiday> GetHolidaysByDateRange(DateTime startDate, DateTime endDate);
    }
}
