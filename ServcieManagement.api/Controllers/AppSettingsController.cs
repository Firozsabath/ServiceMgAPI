using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;
using ServiceManagement.WebAPI.DTOs;

namespace ServiceManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppSettingsController : ControllerBase
    {
        private readonly IRequestTypesRepository requestTypes;
        private readonly IRequestPrioritiesRepository requestPriorities;
        private readonly IMapper mapper;

        public AppSettingsController(IRequestTypesRepository requestTypes, 
            IRequestPrioritiesRepository requestPriorities,
            IMapper mapper)
        {
            this.requestTypes = requestTypes;
            this.requestPriorities = requestPriorities;
            this.mapper = mapper;
        }

        [HttpGet("GetRequestTypes")]
        public async Task<IActionResult> GetRequestTypes()
        {

            var rt = this.requestTypes.GetAll();
            return Ok(rt);
        }

        [HttpGet("GetRequestTypesByID")]
        public async Task<IActionResult> GetRequestTypesByID(int id)
        {
            var rt = this.requestTypes.Get(x=>x.ID == id);
            return Ok(rt);
        }

        [HttpPost("AddRequestType")]
        public async Task<IActionResult> AddRequestType(RequestTypesDTO rType)
        {
            if(rType == null)
            {
                return Unauthorized("Invalid Data!");
            }
            var rt = this.mapper.Map<RequestTypes>(rType);
            this.requestTypes.Add(rt);
            var st = this.requestTypes.save();            
            return Ok(st);
        }

        [HttpPut("UpdateRequestType/{id}")]
        public async Task<IActionResult> UpdateRequestType(int id,RequestTypes rType)
        {
            if (rType == null)
            {
                return Unauthorized("Invalid Data!");
            }
            this.requestTypes.Update(rType);
            var st = this.requestTypes.save();
            return Ok(st);
        }

        [HttpDelete("DeleteRequestType/{id}")]
        public async Task<IActionResult> UpdateRequestType(int id)
        {
            if (id == 0)
            {
                return Unauthorized("Invalid Data!");
            }
            var rd = this.requestTypes.Get(x=>x.ID == id);
            this.requestTypes.Delete(rd);
            var st = this.requestTypes.save();
            return Ok(st);
        }


        [HttpGet("GetRequestPriorities")]
        public async Task<IActionResult> GetRequestPriorities() {

            return Ok(this.requestPriorities.GetAll());
        }


        [HttpPost("AddRequestPriorities")]
        public async Task<IActionResult> AddRequestPriorities(RequestPrioritiesDTO rPriorities)
        {
            if (rPriorities == null)
            {
                return Unauthorized("Invalid Data!");
            }
            var rt = this.mapper.Map<PriorityLevels>(rPriorities);
            this.requestPriorities.Add(rt);
            var st = this.requestPriorities.save();
            return Ok(st);
        }

        [HttpPut("UpdateRequestPriorities/{id}")]
        public async Task<IActionResult> UpdateRequestPriorities(int id, RequestPrioritiesDTO rPriorities)
        {
            if (rPriorities == null)
            {
                return Unauthorized("Invalid Data!");
            }
            var rp = this.mapper.Map<PriorityLevels>(rPriorities);
            this.requestPriorities.Update(rp);
            var st = this.requestPriorities.save();
            return Ok(st);
        }

        [HttpDelete("DeleteRequestPriorities/{id}")]
        public async Task<IActionResult> DeleteRequestPriorities(int id)
        {
            if (id == 0)
            {
                return Unauthorized("Invalid Data!");
            }
            var rp = this.requestPriorities.Get(x => x.ID == id);
            this.requestPriorities.Delete(rp);
            var st = this.requestPriorities.save();
            return Ok(st);
        }
    }
}
