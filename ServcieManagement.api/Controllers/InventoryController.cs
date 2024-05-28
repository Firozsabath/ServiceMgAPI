using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;
using ServiceManagement.WebAPI.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventroryRepository _invRepo;

        public InventoryController(IInventroryRepository invRepo)
        {
            _invRepo = invRepo;
        }
        // GET: api/<InventoryController>
        [HttpGet]
        public IActionResult Get()
        {
            var invs = _invRepo.GetAll(null,x=>x.vendor).Select(i => new InventoryVMDTO
            {
                CategoryID = i.CategoryID,
                CreatedDate = i.CreatedDate,
                Description = i.Description,
                LastReorderDate = i.LastReorderDate,
                LastReorderQuantity = i.LastReorderQuantity,
                LeadTime = i.LeadTime,
                Manufacturer = i.Manufacturer,
                MiscellaniousChargeDescription = i.MiscellaniousChargeDescription,
                MiscellaniousCost = i.MiscellaniousCost,
                PartsID = i.PartsID,
                ProductCost = i.ProductCost,
                QuantityOnHand = i.QuantityOnHand,
                ReorderLevel = i.ReorderLevel,
                Supplier = i.Supplier,
                UnitCost = i.UnitCost,
                UpdatedDate = i.UpdatedDate,
                Vendorid = i.Vendorid,
                VendorName = i.vendor.Name,
                skuID = i.SkuID
                
            }).ToList();           
            return Ok(invs);
        }

        // GET api/<InventoryController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_invRepo.Get(c=>c.PartsID == id));
        }

        [HttpGet("CheckAvailability/{id}")]
        public IActionResult CheckAvailability(int id)
        {
            var cnt = _invRepo.Get(c => c.PartsID == id).QuantityOnHand;

             return Ok(cnt>0);
        }

        // POST api/<InventoryController>
        [HttpPost]
        public IActionResult Post([FromBody] Inventory inventory)
        {
            try
            {
                if (inventory == null)
                {
                    return BadRequest("Invalid Entry!");
                }
                _invRepo.Add(inventory);
                _invRepo.save();
                return Ok(inventory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<InventoryController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Inventory inventory)
        {
            try
            {
                if (id != inventory.PartsID)
                {
                    return BadRequest("Invalid inventory data!");
                }
                _invRepo.Update(inventory);
                var st = _invRepo.save();
                return Ok(st);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<InventoryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var inventory = _invRepo.Get(x => x.PartsID == id);
                if (inventory == null)
                {
                    return NotFound("No data found for the id!");
                }
                _invRepo.Delete(inventory);
                var st = _invRepo.save();
                return Ok(st);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
