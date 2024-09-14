using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using ServiceManagement.Domain.Entities;
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
        private readonly IQuotationRepository quotationRepository;

        public QuotationController(IInventroryRepository inventrory, ICompanyRepository company, IQuotationRepository quotationRepository)
        {
            this.inventrory = inventrory;
            this.company = company;
            this.quotationRepository = quotationRepository;
        }

        [HttpPost("GetQuotationDetails")]
        public IActionResult GetQuotationDetails(QuatationDTO quot)
        {           

            var items = quot.quotationItems.ToList();
            var quotItems = new List<QuotationitemDetailssDTO>();
            decimal? totalAmount = 0;
            decimal? totalVat = 0;            
            decimal? vatAmount = 0;
            string avlability = string.Empty;
            foreach (var item in items)
            {
                var inv = this.inventrory.Get(x => x.PartsID == item.partsId);
                var tot = inv.ProductCost * item.quantityUsed;
                if (inv.VatPercent != null) {
                    vatAmount = tot * ((decimal)inv.VatPercent / (decimal)100);
                } 
                if(inv.QuantityOnHand > 0)
                    avlability = "Ex Stock";
                else
                    avlability = $"{inv.LeadTime} Weeks";
                
               
                quotItems.Add(new QuotationitemDetailssDTO
                {
                    partsId = inv.PartsID,
                    Amount = inv.ProductCost,
                    Description =   inv.Description,
                    quantityUsed = item.quantityUsed,
                    Total = tot,
                    vatPercent = inv.VatPercent,
                    vat = vatAmount,
                    availablility = avlability
                });
                totalAmount += tot;
                totalVat += vatAmount;                
            }

            if(quot.serviceCharges > 0)
            {
                quotItems.Add(new QuotationitemDetailssDTO
                {
                    Amount = quot.serviceCharges,
                    quantityUsed = 1,
                    Description = "Service charge",
                    Total = quot.serviceCharges,
                    vat = quot.serviceCharges * (decimal).05,
                    vatPercent = 5
                });
                totalAmount += quot.serviceCharges;
                totalVat += quot.serviceCharges * (decimal).05;
            }

            var quotConverted = new QuoatationConvertedDTO();
            if (quot.companyID != 0)
            {
                var comp = this.company.Get(x => x.ID == quot.companyID);
                quotConverted = new QuoatationConvertedDTO
                {
                    CompanyID = quot.companyID,
                    CompanyName = comp.Name,
                    CompanyAddress = comp.Address,
                    dateCreated = quot.dateCreated,
                    validUntil = quot.validUntil,
                    subTotal = totalAmount,
                    totalVat = totalVat,
                    totalAmount = totalAmount + totalVat,
                    quotationItems = quotItems
                };
            }
            else
            {
                quotConverted = new QuoatationConvertedDTO
                {
                    CompanyName = quot.companyName,
                    CompanyAddress = quot.companyAddress,
                    dateCreated = quot.dateCreated,
                    validUntil = quot.validUntil,
                    subTotal = totalAmount,
                    totalVat = totalVat,
                    totalAmount = totalAmount + totalVat,
                    quotationItems = quotItems
                };
            }
            quotConverted.paymentTerms = quot.paymentTerms;
            return Ok(quotConverted);
        }

        [HttpPost("PostQuotationDetails")]
        public async Task<IActionResult> PostQuotationDetails(QuoatationConvertedDTO dTO)
        {
           
            var quotItens = new List<QuotationItems>();
            foreach(var item in dTO.quotationItems)
            {
                quotItens.Add(new QuotationItems
                {
                    Description = item.Description,
                    PartsID = item.partsId,
                    Quantity = item.quantityUsed,
                    TotalPrice = item.Total,
                    UnitPrice = item.Amount,
                    VatAmount = item.vat,
                    Vat = item.vatPercent,
                    Availability = item.availablility
                });
            }

            var Quotation = new Quotations
            {
                CompanyID = dTO.CompanyID == 0 ? null : dTO.CompanyID,
                MachineID = null,
                DateCreated = dTO.dateCreated,
                ValidUntil = dTO.validUntil,
                TotalAmount = dTO.totalAmount,
                QuotationNum = dTO.quotationNum,
                companyAddress = dTO.CompanyAddress,
                companyName = dTO.CompanyName,
                Trn = dTO.Trn,
                SubTotal = dTO.subTotal,
                TotalVat = dTO.totalVat,
                PaymentTerms = dTO.paymentTerms,
                QuotationItems = quotItens
            };

            this.quotationRepository.Add(Quotation);
            var st = this.quotationRepository.save();

            return Ok(st);
        }
    }
}
