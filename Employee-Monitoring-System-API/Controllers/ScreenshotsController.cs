using Microsoft.AspNetCore.Mvc;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;

namespace Employee_Monitoring_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenshotsController : ControllerBase
    {
        private readonly IScreenshotRepository _screenshotRepository;

        public ScreenshotsController(IScreenshotRepository screenshotRepository)
        {
            _screenshotRepository = screenshotRepository;
        }

        // GET: api/Screenshots
        [HttpGet]
        public ActionResult<IEnumerable<Screenshot>> GetScreenshots()
        {
            var ss = _screenshotRepository.GetAllScreenshots();
            return Ok(ss);
        }

        // GET: api/Screenshots/5
        [HttpGet("{id}")]
        public ActionResult<Screenshot> GetScreenshot(int id)
        {
            var screenshot = _screenshotRepository.GetScreenshot(id);

            if (screenshot == null)
            {
                return NotFound();
            }

            return Ok(screenshot);
        }

        // PUT: api/Screenshots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutScreenshot(int id, Screenshot screenshot)
        {
            if (id != screenshot.ScreenshotId)
            {
                return BadRequest();
            }

            var updatedSs = _screenshotRepository.Update(screenshot);
            if(updatedSs != null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Screenshots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Screenshot> PostScreenshot(Screenshot screenshot)
        {
            var addedSs = _screenshotRepository.Add(screenshot);    
            return CreatedAtAction("GetScreenshot", new { id = addedSs.ScreenshotId }, addedSs);
        }

        // DELETE: api/Screenshots/5
        [HttpDelete("{id}")]
        public IActionResult DeleteScreenshot(int id)
        {
            var screenshot = _screenshotRepository.Delete(id);
            if (screenshot == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        //private bool ScreenshotExists(Guid id)
        //{
        //    return _context.Screenshots.Any(e => e.ScreenshotId == id);
        //}
    }
}
