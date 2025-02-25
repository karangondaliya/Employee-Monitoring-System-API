using Microsoft.AspNetCore.Mvc;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;
using AutoMapper;
using Employee_Monitoring_System_API.DTOs;

namespace Employee_Monitoring_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenshotsController : ControllerBase
    {
        private readonly IScreenshotRepository _screenshotRepository;
        private readonly IMapper _mapper;

        public ScreenshotsController(IScreenshotRepository screenshotRepository, IMapper mapper)
        {
            _screenshotRepository = screenshotRepository;
            _mapper = mapper;
        }

        // GET: api/Screenshots
        [HttpGet]
        public ActionResult<IEnumerable<ScreenshotDTO>> GetScreenshots()
        {
            var ss = _screenshotRepository.GetAllScreenshots();
            var ssDTOs = _mapper.Map<IEnumerable<ScreenshotDTO>>(ss);
            return Ok(ssDTOs);
        }

        // GET: api/Screenshots/5
        [HttpGet("{id}")]
        public ActionResult<ScreenshotDTO> GetScreenshot(int id)
        {
            var screenshot = _screenshotRepository.GetScreenshot(id);

            if (screenshot == null)
            {
                return NotFound();
            }
            var screenshotDTO = _mapper.Map<ScreenshotDTO>(screenshot);

            return Ok(screenshotDTO);
        }

        // PUT: api/Screenshots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
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
            var updatedSsDTO = _mapper.Map<ScreenshotDTO>(updatedSs);
            return Ok(updatedSsDTO);
        }

        // POST: api/Screenshots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Screenshot> PostScreenshot(ScreenshotDTO screenshotDTO)
        {
            var screenshot = _mapper.Map<Screenshot>(screenshotDTO);
            var addedSs = _screenshotRepository.Add(screenshot);   
            
            var createdScreenshotDTO = _mapper.Map<ScreenshotDTO>(addedSs);
            return CreatedAtAction(nameof(GetScreenshot), new { id = createdScreenshotDTO.ScreenshotId }, createdScreenshotDTO);
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
