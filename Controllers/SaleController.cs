using loyaltycard.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using loyaltycard.Models;
using Microsoft.EntityFrameworkCore;


namespace loyaltycard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public SaleController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Sale>>> GetAllSales()
        {
            var Sales = await appDbContext.Sales.ToListAsync();
            return Ok(Sales);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetSale(string id)
        {
            var Sale = await appDbContext.Sales.FirstOrDefaultAsync(e => e.Id == id);
            if (Sale != null)
            {
                return Ok(Sale);
            }
            return NotFound();
        }
        [HttpGet("inactive")]
        public async Task<ActionResult<List<Sale>>> GetInactiveSales()
        {
            var sales= await appDbContext.Sales.ToListAsync();
            var inactiveSales = sales.FindAll(e => e.Status == "Inactive");
            if (sales != null)
            {
                return Ok(inactiveSales);
            }
            return NotFound();
        }

        //Add
        [HttpPost]
        public async Task<ActionResult<string>> AddSale(Sale newSale)
        {
            if (newSale != null)
            {
                try
                {
                    appDbContext.Sales.Add(newSale);
                    await appDbContext.SaveChangesAsync();


                    
                    return Ok(newSale.Id);
                }
                catch (Exception ex) { NotFound(ex); }
            }
            return BadRequest();
        }

        //Delete
        [HttpDelete]
        public async Task<ActionResult<Boolean>> DeleteSale(string id)
        {
            var Sale = await appDbContext.Sales.FirstOrDefaultAsync(e => e.Id == id);
            if (Sale != null)
            {
                try
                {
                    Sale.Status="Inactive";
                    await appDbContext.SaveChangesAsync();


                    return Ok(true);
                }
                catch (Exception ex) { return NotFound(ex); }
            }
            else { return BadRequest(false); }
        }


        [HttpPut]
        public async Task<ActionResult<Sale>> UpdateSale(Sale updatedSale)
        {
            if (updatedSale != null)
            {
                var Sale = await appDbContext.Sales.FirstOrDefaultAsync(e => e.Id == updatedSale.Id);
                if (Sale != null)
                {
                    try
                    {
                        Sale.Id = updatedSale.Id;
                        Sale.Date = updatedSale.Date;
                        Sale.BranchId = updatedSale.BranchId;
                        Sale.EmployeeId = updatedSale.EmployeeId;
                        Sale.CustomerId = updatedSale.CustomerId;



                        await appDbContext.SaveChangesAsync();


                        return Ok(Sale);
                    }
                    catch (Exception ex) { BadRequest(ex); }

                }
                else { return NotFound(); }
            }
            return BadRequest();
        }

    }
}