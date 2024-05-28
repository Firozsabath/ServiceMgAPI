using Microsoft.AspNetCore.Mvc;
using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachinesController : ControllerBase
    {
        private readonly IMachinesRepository _machines;
        public MachinesController(IMachinesRepository machines)
        {
            _machines = machines;
        }

        // GET: api/<MachinesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var machines = _machines.GetAll(null, "a => a.ContractType");
            return Ok(await _machines.GetAllMachines());
        }

        // GET api/<MachinesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_machines.Get(c=>c.ID == id));
        }

        [HttpGet("GetBYBranch/{id}")]
        public IActionResult GetByBranch(int id)
        {
            return Ok(_machines.GetAll(c => c.BranchID == id));
        }

        // POST api/<MachinesController>
        [HttpPost]
        public IActionResult Post([FromBody] Machines machine)
        {
            try
            {
                if (machine == null)
                {
                    return BadRequest("Invalid machine info!!");
                }
                _machines.Add(machine);
                var st = _machines.save();
                if (st)
                {
                    return Ok(machine);
                }

                return BadRequest("Something went wrong!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        // PUT api/<MachinesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Machines machine)
        {
            try
            {
                if (id != machine.ID)
                {
                    return BadRequest("Invalid machine data!");
                }
                _machines.Update(machine);
                var st = _machines.save();
                return Ok(st);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<MachinesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Invalid mahcine id!");
                }
                var machine = _machines.Get(c => c.ID == id);
                _machines.Delete(machine);
                var st = _machines.save();
                return Ok(st);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
