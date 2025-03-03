using Microsoft.AspNetCore.Mvc;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;
using AutoMapper;
using Employee_Monitoring_System_API.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Employee_Monitoring_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppSettingsController : ControllerBase
    {
        private readonly IAppsettingRepository _asr;
        private readonly IMapper _mapper;
        public AppSettingsController(IAppsettingRepository asr, IMapper mapper)
        {
            _asr = asr;
            _mapper = mapper;
        }

        // GET: api/AppSettings
        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        public ActionResult<IEnumerable<AppSettings>> GetAppSettings()
        {
            var appSettings = _asr.GetAll();
            return Ok(appSettings);
        }

        // GET: api/AppSettings/5
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public ActionResult<AppSettings> GetAppSettings(string id)
        {
            var appSettings = _asr.GetByKey(id);

            if (appSettings == null)
            {
                return NotFound();
            }
            return Ok(appSettings);
        }

        // PUT: api/AppSettings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult PutAppSettings(string id, AppSettings appSetting)
        {
            if (id != appSetting.SettingKey)
            {
                return BadRequest();
            }
            _asr.Update(appSetting);
            return Ok();
        }

        // POST: api/AppSettings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public ActionResult PostAppSettings(AppSettings appSettings)
        {
            _asr.Add(appSettings);
            return Ok();
        }

        // DELETE: api/AppSettings/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteAppSettings(string id)
        {
            _asr.Delete(id);
            return NoContent();
        }

        //private bool AppSettingsExists(string id)
        //{
        //    return _context.AppSettings.Any(e => e.SettingKey == id);
        //}
    }
}
