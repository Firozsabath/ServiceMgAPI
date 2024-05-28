using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.Domain.Entities;
using ServiceManagement.Domain.Interfaces;
using ServiceManagement.EFCore.Repositories;
using ServiceManagement.WebAPI.DTOs;

namespace ServiceManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IMapper _mapper;

        public CompaniesController(ICompanyRepository companyRepository, IMapper mapper)
        {
            this.companyRepository = companyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var rs = this.companyRepository.GetAll();
                var compDTO = _mapper.Map<IList<CompanyDTO>>(rs);
                return Ok(compDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }            
            return Ok();
        }

        // GET api/<TechniciansController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var company = _mapper.Map<CompanyDTO>(this.companyRepository.Get(x => x.ID == id));
            return Ok(company);
        }

        [HttpPost]
        public IActionResult Post(CompanyDTO companies)
        {
            var company = _mapper.Map<Companies>(companies);
            this.companyRepository.Add(company);
            var status = this.companyRepository.save();
            return Ok(company);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id,CompanyDTO companies)
        {
            if(companies == null)
            {
                return BadRequest("Company value is not valid!");
            }
            var company = _mapper.Map<Companies>(companies);
            this.companyRepository.Update(company);
            var status = this.companyRepository.save();
            return Ok(status);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var company = this.companyRepository.Get(x => x.ID == id);

            if(company == null)
            {
                return BadRequest("No company is found!");
            }

            this.companyRepository.Delete(company);
            var status = this.companyRepository.save();

            return Ok(status);

        }
    }
}
