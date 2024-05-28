using Microsoft.AspNetCore.Mvc;
using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;
using ServiceManagement.WebAPI.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicePartsController : ControllerBase
    {
        private readonly IservicePartsRepository sParts;

        public ServicePartsController(IservicePartsRepository sParts)
        {
            this.sParts = sParts;
        }
        // GET: api/<ServicePartsController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.sParts.GetAll());
        }

        // GET api/<ServicePartsController>/5

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(this.sParts.Get(s => s.ID == id));
        }

        // GET api/<ServicePartsController>/ByService/5

        [HttpGet("ByService/{id}")]
        public IActionResult GetByService(int id)
        {
            var sPartss = this.sParts.GetAll(s => s.ServiceID == id, x => x.Parts).ToList();
           
                var usedParts = sPartss.Select(p => new ServicePartsDTO
                {
                    QuantityUsed = p.QuantityUsed,
                    ID = p.ID,
                    PartName = p.Parts.Description,
                    PartsID = p.PartsID,
                    ServiceID = p.ServiceID
                }).ToList();
            return Ok(usedParts);
        }

        // POST api/<ServicePartsController>
        [HttpPost]
        public IActionResult Post([FromBody] List<ServiceParts> data)
        {
            if(data == null)
            {
                return NotFound("No data sent");
            }

            this.sParts.AddRange(data);
            var status = this.sParts.save();
            return Ok(status);
        }

        // PUT api/<ServicePartsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ServicePartsDTO value)
        {
            try
            {
                if (value == null)
                {
                    return BadRequest("Invalid Service Parts info!");
                }

                var usedPart = this.sParts.Get(p => p.ServiceID == value.ServiceID && p.PartsID == value.PartsID);
                if (usedPart != null)
                {
                    usedPart.QuantityUsed = value.QuantityUsed;
                    this.sParts.Update(usedPart);
                    var st = this.sParts.save();
                    return Ok(st);
                }
                else
                {
                    return BadRequest("No data found!");
                }
                
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ServicePartsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest("Invalid id!");
                }
                var branch = this.sParts.Get(c => c.ID == id);
                this.sParts.Delete(branch);
                var st = this.sParts.save();
                return Ok(st);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
