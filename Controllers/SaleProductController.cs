using loyaltycard.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using loyaltycard.Models;
using Microsoft.EntityFrameworkCore;


namespace loyaltycard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleProductController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public SaleProductController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<SaleProduct>>> GetAllSaleProducts()
        {
            var SaleProducts = await appDbContext.SaleProducts.ToListAsync();
            return Ok(SaleProducts);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SaleProduct>> GetsalesProduct(int id)
        {
            var SalesProduct = await appDbContext.SaleProducts.FirstOrDefaultAsync(e => e.Id == id);
            if (SalesProduct != null)
            {
                return Ok(SalesProduct);
            }
            return NotFound();
        }
        [HttpGet("inactive")]
        public async Task<ActionResult<List<SaleProduct>>> GetInactiveSaleProducts()
        {
            var saleProducts = await appDbContext.SaleProducts.ToListAsync();
            var inactiveSaleProducts = saleProducts.FindAll(e => e.Status == "Inactive");
            if (saleProducts != null)
            {
                return Ok(inactiveSaleProducts);
            }
            return NotFound();
        }

        //Add
        [HttpPost]
        public async Task<ActionResult<int>> AddSalesProduct(SaleProduct newSalesProduct)
        {
            if (newSalesProduct != null)
            {
                try
                {
                    appDbContext.SaleProducts.Add(newSalesProduct);
                    await appDbContext.SaveChangesAsync();


                    
                    return Ok(newSalesProduct.Id);
                }
                catch (Exception ex) { NotFound(ex); }
            }
            return BadRequest();
        }

        //Delete
        [HttpDelete]
        public async Task<ActionResult<Boolean>> DeleteSalesProduct(int id)
        {
            var SalesProduct = await appDbContext.SaleProducts.FirstOrDefaultAsync(e => e.Id == id);
            if (SalesProduct != null)
            {
                try
                {
                    SalesProduct.Status = "Inactive";
                    await appDbContext.SaveChangesAsync();


                    return Ok(true);
                }
                catch (Exception ex) { return NotFound(ex); }
            }
            else { return BadRequest(false); }
        }


        [HttpPut]
        public async Task<ActionResult<SaleProduct>> UpdateSalesProduct(SaleProduct updatedSalesProduct)
        {
            if (updatedSalesProduct != null)
            {
                var SalesProduct = await appDbContext.SaleProducts.FirstOrDefaultAsync(e => e.Id == updatedSalesProduct.Id);
                if (SalesProduct != null)
                {
                    try
                    {
                        SalesProduct.Id = updatedSalesProduct.Id;
                        SalesProduct.SaleId = updatedSalesProduct.SaleId;
                        SalesProduct.ProductId = updatedSalesProduct.ProductId;
                        SalesProduct.Cost = updatedSalesProduct.Cost;
                        SalesProduct.Bonus = updatedSalesProduct.Bonus;




                        await appDbContext.SaveChangesAsync();


                        return Ok(SalesProduct);
                    }
                    catch (Exception ex) { BadRequest(ex); }

                }
                else { return NotFound(); }
            }
            return BadRequest();
        }

    }
}