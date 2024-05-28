using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;
using ServiceManagement.WebAPI.DTOs;
using System.Reflection.PortableExecutable;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestController : ControllerBase
    {
        private readonly IServiceRequestsRepository _serviceRequests;
        private readonly IRequestSequenceRepository _requestSequence;

        public ServiceRequestController(IServiceRequestsRepository serviceRequests, IRequestSequenceRepository requestSequence)
        {
            _serviceRequests = serviceRequests;
            _requestSequence = requestSequence;
        }
        // GET: api/<ServiceRequests>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_serviceRequests.GetAllTasks());
        }

        [HttpGet("GetByCategory")]
        public IActionResult GetByTechnician()
        {           
            return Ok(_serviceRequests.GetByCategory());
        }

        [HttpGet("GetByStatus")]
        public IActionResult GetByStatus()
        {
            return Ok(_serviceRequests.GetByStatus());
        }

        [HttpGet("GetFilteredRequest")]
        public async Task<IActionResult> GetServiceRequests(
           [FromQuery] int? companyId = null,
           [FromQuery] int? branchId = null,
           [FromQuery] int? machineId = null,
           [FromQuery]  int? technicianId = null,
           [FromQuery] int? statusId = null)
        {
            var serviceRequests =  _serviceRequests.GetFIlteredRequests(companyId, branchId, machineId, technicianId, statusId);
            return Ok(serviceRequests);
        }

        // GET api/<ServiceRequests>/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            return Ok(_serviceRequests.GetByTicketID(id));
        }

        //GET api//<ServiceRequests>/GetByTechnician/5
        [HttpGet("GetByTechnician/{id}")]
        public IActionResult GetByTechnician(long id)
        {
            return Ok(_serviceRequests.GetByTechnician(id));
        }

        // POST api/<ServiceRequests>
        [HttpPost]
        public  IActionResult Post([FromBody] ServiceRequests data)
        {
            try
            {

                if (data == null)
                {
                    return BadRequest("Invalid request info!!");
                }
                var localTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Dubai");
                var localDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(data.EstimatedCompleteDate), localTimeZone);
                var localcreatedDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(data.RequestedDate), localTimeZone);
                data.RequestID = "#tc"+_requestSequence.GetNextTicketNumber().ToString();
                data.ServiceStatusID = 1;
                data.EstimatedCompleteDate = localDate;
                data.RequestedDate = localcreatedDate;
                _serviceRequests.Add(data);
               
                var st = _serviceRequests.save();
                if (st)
                {
                    return Ok(data);
                }

                return BadRequest("Something went wrong!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ServiceRequests>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ServiceRequests requests)
        {
            if (requests == null)
            {
                return BadRequest("Company value is not valid!");
            }
            //var company = _mapper.Map<Companies>(requests);
            this._serviceRequests.Update(requests);
            var status = this._serviceRequests.save();
            return Ok(status);            
        }

        [HttpPut("UpdateStatus/{id}")]
        public IActionResult UpdateStatus(int id, [FromBody] ServiceRequestStatusDTO data)
        {
            if (data == null)
            {
                return BadRequest("Company value is not valid!");
            }
            var request = this._serviceRequests.Get(x => x.ID == data.ServiceID);
            if(request == null)
            {
                return BadRequest("Service Request is not found!");
            }
            request.ServiceStatusID = data.ServiceStatusID;
            request.TechnicianComment = data.TechnicianComment;
            if(data.ServiceStatusID == 3)
            {
                request.ComplatedDate = DateTime.Now;
            }
            //var company = _mapper.Map<Companies>(requests);
            this._serviceRequests.Update(request);
            var status = this._serviceRequests.save();
            return Ok(status);
        }

        [HttpPut("RespondRequest/{id}")]
        public IActionResult UpdateResponse(int id, [FromBody] ServiceRequestResponseDTO data)
        {
            if (data == null)
            {
                return BadRequest("Company value is not valid!");
            }
            var request = this._serviceRequests.Get(x => x.ID == data.ServiceID);
            if (request == null)
            {
                return BadRequest("Service Request is not found!");
            }
            
            TimeSpan diff = (DateTime.Now - Convert.ToDateTime(request.RequestedDate));
            var respondedhours = diff.TotalHours;
            request.RespondMessage = data.RespondMessage;
            request.RespondTime = DateTime.Now;
            request.RespondedinHours = respondedhours;
            this._serviceRequests.Update(request);
            var status = this._serviceRequests.save();
            return Ok(status);
        }

        // DELETE api/<ServiceRequests>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var request = _serviceRequests.Get(x => x.ID == id,x=>x.ServiceParts);
                if (request == null)
                {
                    return NotFound("No data found for the id!");
                }
                _serviceRequests.Delete(request);
                var st = _serviceRequests.save();
                return Ok(st);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
