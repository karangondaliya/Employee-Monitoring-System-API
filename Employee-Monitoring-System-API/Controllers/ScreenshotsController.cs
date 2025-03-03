using Microsoft.AspNetCore.Mvc;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;
using AutoMapper;
using Employee_Monitoring_System_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Employee_Monitoring_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenshotsController : ControllerBase
    {
        private readonly IScreenshotRepository _screenshotRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserRepository _ur;

        public ScreenshotsController(IScreenshotRepository screenshotRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment, IUserRepository ur)
        {
            _screenshotRepository = screenshotRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _ur = ur;
        }

        // GET: api/Screenshots
        [HttpGet]
        [Authorize(Policy = "EmployeePolicy")]
        [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK, "image/png")]
        public ActionResult<IEnumerable<ScreenshotDTO>> GetScreenshots()
        {
            var screenshots = _screenshotRepository.GetAllScreenshots();
            var screenshotDTOs = screenshots.Select(ss => new ScreenshotDTO
            {
                ScreenshotId = ss.ScreenshotId,
                ImagePath = $"{Request.Scheme}://{Request.Host}{(ss.ImagePath.StartsWith("/") ? ss.ImagePath : "/" + ss.ImagePath)}"
            });

            return Ok(screenshotDTOs);
        }

        // GET: api/Screenshots/5
        [HttpGet("{id}")]
        [Authorize(Policy = "EmployeePolicy")]
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
        [Authorize(Policy = "EmployeePolicy")]
        public IActionResult PutScreenshot(int id, Screenshot screenshot)
        {
            if (id != screenshot.ScreenshotId)
            {
                return BadRequest();
            }

            var updatedSs = _screenshotRepository.Update(screenshot);
            if(updatedSs == null)
            {
                return NotFound();
            }
            var updatedSsDTO = _mapper.Map<ScreenshotDTO>(updatedSs);
            return Ok(updatedSsDTO);
        }

        // POST: api/Screenshots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "EmployeePolicy")]
        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile imageFile)
        {
            // Extract email from JWT token
            var email = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("Invalid user session.");
            }

            // Fetch userId from database using email
            var user = _ur.FindByEmail(email);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            if (string.IsNullOrEmpty(_webHostEnvironment.WebRootPath))
            {
                return StatusCode(500, "Server error: Web root path is not set.");
            }

            // Define folder path
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Generate unique filename
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Save file to server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Save only relative path to the database
            var screenshot = new Screenshot
            {
                UserId = user.Id,
                ImagePath = $"/uploads/{uniqueFileName}"
            };

            var addedSs = _screenshotRepository.Add(screenshot);
            var createdScreenshotDTO = new ScreenshotDTO
            {
                ScreenshotId = addedSs.ScreenshotId,
                ImagePath = $"{Request.Scheme}://{Request.Host}{screenshot.ImagePath}" // Full URL
            };

            return CreatedAtAction(nameof(GetScreenshots), new { id = createdScreenshotDTO.ScreenshotId }, createdScreenshotDTO);
        }

        // DELETE: api/Screenshots/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteScreenshot(int id)
        {
            var screenshot = _screenshotRepository.GetScreenshot(id);
            if (screenshot == null)
            {
                return NotFound("Screenshot not found.");
            }

            // Delete the file from the server
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, screenshot.ImagePath.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            _screenshotRepository.Delete(id);
            return NoContent();
        }

        //private bool ScreenshotExists(Guid id)
        //{
        //    return _context.Screenshots.Any(e => e.ScreenshotId == id);
        //}
    }
}
