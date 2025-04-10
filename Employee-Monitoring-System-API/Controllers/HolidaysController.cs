using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Employee_Monitoring_System_API.DTOs;
using Employee_Monitoring_System_API.Models;
using Employee_Monitoring_System_API.Repository.IRepository;

namespace Employee_Monitoring_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidaysController : ControllerBase
    {
        private readonly IHolidayRepository _holidayRepo;
        private readonly IMapper _mapper;

        public HolidaysController(IHolidayRepository holidayRepo, IMapper mapper)
        {
            _holidayRepo = holidayRepo;
            _mapper = mapper;
        }

        // GET: api/Holidays
        [HttpGet]
        [Authorize(Policy = "EmployeePolicy")]
        public ActionResult<IEnumerable<HolidayDTO>> GetHolidays()
        {
            var holidays = _holidayRepo.GetAllHolidays();
            if (holidays == null || !holidays.Any())
                return NotFound("No holidays found.");

            var dtos = _mapper.Map<IEnumerable<HolidayDTO>>(holidays);
            return Ok(dtos);
        }

        // GET: api/Holidays/5
        [HttpGet("{id}")]
        [Authorize(Policy = "EmployeePolicy")]
        public ActionResult<HolidayDTO> GetHoliday(int id)
        {
            var holiday = _holidayRepo.GetHolidayById(id);
            if (holiday == null)
                return NotFound("Holiday not found.");

            var dto = _mapper.Map<HolidayDTO>(holiday);
            return Ok(dto);
        }

        // GET: api/Holidays/range?start=2025-01-01&end=2025-12-31
        [HttpGet("range")]
        [Authorize(Policy = "EmployeePolicy")]
        public ActionResult<IEnumerable<HolidayDTO>> GetHolidaysInRange([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var holidays = _holidayRepo.GetHolidaysByDateRange(start, end);
            if (holidays == null || !holidays.Any())
                return NotFound("No holidays found in the specified range.");

            var dtos = _mapper.Map<IEnumerable<HolidayDTO>>(holidays);
            return Ok(dtos);
        }

        // POST: api/Holidays
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public ActionResult<HolidayDTO> PostHoliday([FromBody] HolidayDTO dto)
        {
            var holiday = _mapper.Map<Holiday>(dto);
            _holidayRepo.AddHoliday(holiday);

            var createdDto = _mapper.Map<HolidayDTO>(holiday);
            return CreatedAtAction(nameof(GetHoliday), new { id = createdDto.HolidayId }, createdDto);
        }

        // PATCH: api/Holidays/5
        [HttpPatch("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult PatchHoliday(int id, [FromBody] JsonPatchDocument<HolidayDTO> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest("Invalid patch document.");

            var holiday = _holidayRepo.GetHolidayById(id);
            if (holiday == null)
                return NotFound("Holiday not found.");

            var dto = _mapper.Map<HolidayDTO>(holiday);
            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _mapper.Map(dto, holiday);
            _holidayRepo.UpdateHoliday(holiday);

            var updatedDto = _mapper.Map<HolidayDTO>(holiday);
            return Ok(updatedDto);
        }

        // DELETE: api/Holidays/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteHoliday(int id)
        {
            var holiday = _holidayRepo.GetHolidayById(id);
            if (holiday == null)
                return NotFound("Holiday not found.");

            _holidayRepo.DeleteHoliday(id);
            return Ok($"Holiday Deleted {id}");
        }
    }
}
