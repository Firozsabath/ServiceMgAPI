using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;
using System.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchesRepository _branchesrepo;

        public BranchesController(IBranchesRepository branchesrepo)
        {
            _branchesrepo = branchesrepo;
        }
        // GET: api/<BranchesController>
        [HttpGet]
        public IActionResult Get()
        {
            var branches = _branchesrepo.GetAll();
            return Ok(branches);
        }

        // GET api/<BranchesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            return Ok(_branchesrepo.Get(c => c.ID == id));
        }

        [HttpGet("GetBYCompany/{id}")]
        public IActionResult GetbyCompany(long id)
        {
            return Ok(_branchesrepo.GetAll(c => c.CompanyID == id));
        }


        [HttpGet("GetBranchwithCompnay")]
        public IActionResult GetbyCompany()
        {
            return Ok(_branchesrepo.GetBranchwithCompnay());
        }

        // POST api/<BranchesController>
        [HttpPost]
        public IActionResult Post([FromBody] Branches branch)
        {

            try
            {
                _branchesrepo.Add(branch);
                var status = _branchesrepo.save();
                if (status)
                {
                    return Ok(branch);
                }
                else
                {
                    return BadRequest("Something went wrong!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("Upload")]
        public IActionResult Uploaddocs()
        {

            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Uploads");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        // PUT api/<BranchesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Branches branch)
        {
            try
            {
                if (branch == null)
                {
                    return BadRequest("Invalid branch info!");
                }
                _branchesrepo.Update(branch);
                var st = _branchesrepo.save();
                return Ok(st);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<BranchesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest("Invalid id!");
                }
                var branch = _branchesrepo.Get(c => c.ID == id);
                _branchesrepo.Delete(branch);
                var st = _branchesrepo.save();
                return Ok(st);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
