using Microsoft.AspNetCore.Mvc;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;
using AutoMapper;
using Employee_Monitoring_System_API.DTOs;

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
        public ActionResult<IEnumerable<AppSettingDTO>> GetAppSettings()
        {
            var appSettings = _asr.GetAll();
            var appSettingsDTO = _mapper.Map<IEnumerable<AppSettingDTO>>(appSettings);
            return Ok(appSettingsDTO);
        }

        // GET: api/AppSettings/5
        [HttpGet("{id}")]
        public ActionResult<AppSettingDTO> GetAppSettings(string id)
        {
            var appSettings = _asr.GetByKey(id);

            if (appSettings == null)
            {
                return NotFound();
            }
            var appSettingsDTO = _mapper.Map<AppSettingDTO>(appSettings);
            return Ok(appSettingsDTO);
        }

        // PUT: api/AppSettings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutAppSettings(string id, AppSettingDTO appSettingDTO)
        {
            if (id != appSettingDTO.SettingKey)
            {
                return BadRequest();
            }

            var appSettings = _mapper.Map<AppSettings>(appSettingDTO);
            _asr.Update(appSettings);
            return Ok();
        }

        // POST: api/AppSettings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult PostAppSettings(AppSettingDTO appSettingsDTO)
        {
            var appSettings = _mapper.Map<AppSettings>(appSettingsDTO);
            _asr.Add(appSettings);
            return Ok();
        }

        // DELETE: api/AppSettings/5
        [HttpDelete("{id}")]
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
