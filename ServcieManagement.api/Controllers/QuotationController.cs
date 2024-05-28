using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.Domain.Interfaces;
using ServiceManagement.EFCore.Repositories;
using ServiceManagement.WebAPI.DTOs;

namespace ServiceManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationController : ControllerBase
    {
        private readonly IInventroryRepository inventrory;
        private readonly ICompanyRepository company;

        public QuotationController(IInventroryRepository inventrory, ICompanyRepository company)
        {
            this.inventrory = inventrory;
            this.company = company;
        }

        [HttpPost("GetQuotationDetails")]
        public IActionResult GetQuotationDetails(QuatationDTO quot)
        {
            var comp = this.company.Get(x => x.ID == quot.companyID);

            var items = quot.quotationItems.ToList();
            var quotItems = new List<QuotationitemDetailssDTO>();
            decimal? totalAmount = 0;
            foreach (var item in items)
            {
                var inv = this.inventrory.Get(x => x.PartsID == item.partsId);
                var tot = inv.ProductCost*item.quantityUsed;
                quotItems.Add(new QuotationitemDetailssDTO
                {
                    partsId = inv.PartsID,
                    Amount = inv.ProductCost,
                    Description =   inv.Description,
                    quantityUsed = item.quantityUsed,
                    Total = tot,
                });
                totalAmount += tot;
            }

            var quotConverted = new QuoatationConvertedDTO
            {
                CompanyName = comp.Name,
                CompanyAddress = comp.Address,
                dateCreated = quot.dateCreated,
                validUntil = quot.validUntil,
                totalAmount = totalAmount,
                quotationItems = quotItems
            };
            return Ok(quotConverted);
        }
    }
}
