using Employee_Monitoring_System_API.Models;

namespace Employee_Monitoring_System_API.Repository.IRepository
{
    public interface IScreenshotRepository
    {
        Screenshot GetScreenshot(int id);
        IEnumerable<Screenshot> GetAllScreenshots();
        Screenshot Add(Screenshot screenshot);
        Screenshot Update(Screenshot screenshotChanges);
        Screenshot Delete(int id);
    }
}
