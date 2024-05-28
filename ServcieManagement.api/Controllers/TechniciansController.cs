using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;
using ServiceManagement.WebAPI.DTOs;
using System.Reflection.PortableExecutable;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechniciansController : ControllerBase
    {
        private readonly ITechniciansRepository _techniciansRepository;
        private readonly IMapper _mapper;

        public TechniciansController(ITechniciansRepository techniciansRepository,IMapper mapper)
        {
            _techniciansRepository = techniciansRepository;
            _mapper = mapper;
        }

        // GET: api/<TechniciansController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_techniciansRepository.GetAll());
        }

        // GET api/<TechniciansController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_techniciansRepository.Get(x=>x.ID == id));
        }

        // GET api/<TechniciansController>/GetTechniciansticket
        [HttpGet("GetTechniciansticket")]
        public IActionResult GetTechniciansticket()
        {
            return Ok(_techniciansRepository.GettechTicketDetails());
        }

        // GET api/<TechniciansController>/GetTechniciansticket
        [HttpGet("GetReponseCountByTechnician")]
        public IActionResult GetReponseCountByTechnician()
        {
            var count = _techniciansRepository.GetAll(null, x => x.ServiceRequests);
            var tchData = count.Select(t=> new TechnicianRespnseViolation
            {
                ID = t.ID,
                Name = t.Name,
                ticketCount = t.ServiceRequests.Count(),
                responseViolations = t.ServiceRequests.Count(
                r => r.RequestedDate.HasValue && r.RespondTime.HasValue &&
                        (r.RequestedDate.Value- r.RespondTime.Value).TotalHours <= 5)
                       
            }).ToList();
            return Ok(tchData);
        }

        // POST api/<TechniciansController>
        [HttpPost]
        public IActionResult Post([FromBody] TechniciansDTO techniciandto)
        {
            try
            {
                if (techniciandto == null)
                {
                    return BadRequest("Invalid technician info!!");
                }
                var technician = _mapper.Map<Technicians>(techniciandto);
                _techniciansRepository.Add(technician);
                var st = _techniciansRepository.save();
                if (st)
                {
                    return Ok(technician);
                }

                return BadRequest("Something went wrong!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<TechniciansController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Technicians technician)
        {
            try
            {
                if (id != technician.ID)
                {
                    return BadRequest("Invalid technician data!");
                }
                _techniciansRepository.Update(technician);
                var st = _techniciansRepository.save();
                return Ok(st);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<TechniciansController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Invalid technician id!");
                }
                var technician = _techniciansRepository.Get(c => c.ID == id);
                _techniciansRepository.Delete(technician);
                var st = _techniciansRepository.save();
                return Ok(st);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
