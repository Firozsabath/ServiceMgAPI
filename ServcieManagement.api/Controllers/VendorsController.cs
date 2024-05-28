using Microsoft.AspNetCore.Mvc;
using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;
using ServiceManagement.WebAPI.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorRepository vendorRepository;

        public VendorsController(IVendorRepository vendorRepository)
        {
            this.vendorRepository = vendorRepository;
        }
        
        // GET: api/<VendorsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var vendors = this.vendorRepository.GetAll();
                
                return Ok(vendors);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Ok();
        }

        // GET api/<VendorsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(this.vendorRepository.Get(x=>x.ID == id));
        }

        // POST api/<VendorsController>
        [HttpPost]
        public IActionResult Post([FromBody] Vendors vendor)
        {
            if (vendor == null)
            {
                return BadRequest("Vendor value is not valid!");
            }           
            this.vendorRepository.Add(vendor);
            var status = this.vendorRepository.save();
            return Ok(status);
        }

        // PUT api/<VendorsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Vendors vendor)
        {
            if (vendor == null)
            {
                return BadRequest("Vendor value is not valid!");
            }            
            this.vendorRepository.Update(vendor);
            var status = this.vendorRepository.save();
            return Ok(status);
        }

        // DELETE api/<VendorsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Vendor = this.vendorRepository.Get(x => x.ID == id);

            if (Vendor == null)
            {
                return BadRequest("No Vendor is found!");
            }

            this.vendorRepository.Delete(Vendor);
            var status = this.vendorRepository.save();

            return Ok(status);
        }
    }
}
