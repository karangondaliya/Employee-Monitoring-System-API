using Employee_Monitoring_System_API.Data;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Employee_Monitoring_System_API.Repository
{
    public class ScreenshotRepository : IScreenshotRepository
    {
        private readonly AppDbContext _context;

        public ScreenshotRepository(AppDbContext context)
        {
            _context = context;
        }

        public Screenshot Add(Screenshot screenshot)
        {
            _context.Screenshots.Add(screenshot);
            _context.SaveChanges();
            return screenshot;
        }

        public Screenshot Delete(int id)
        {
            Screenshot ss = _context.Screenshots.Find(id);
            if (ss != null)
            {
                _context.Screenshots.Remove(ss);
                _context.SaveChanges();
            }
            return ss;
        }

        public IEnumerable<Screenshot> GetAllScreenshots()
        {
            return _context.Screenshots.ToList();
        }

        public Screenshot GetScreenshot(int id)
        {
            return _context.Screenshots.Find(id);
        }

        public Screenshot Update(Screenshot screenshotChanges)
        {
            var ss = _context.Screenshots.Attach(screenshotChanges);
            ss.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return screenshotChanges;
        }
    }
}
