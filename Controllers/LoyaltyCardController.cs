using loyaltycard.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using loyaltycard.Models;
using Microsoft.EntityFrameworkCore;


namespace loyaltycard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyCardController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public LoyaltyCardController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<LoyaltyCard>>> GetAllLoyaltyCards()
        {
            var LoyaltyCards = await appDbContext.LoyaltyCards.ToListAsync();
            var activeLoyaltyCards = LoyaltyCards.FindAll(e => e.Status == "Active");
            return Ok(activeLoyaltyCards);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<LoyaltyCard>> GetLoyaltyCards(int id)
        {
            var LoyaltyCard = await appDbContext.LoyaltyCards.FirstOrDefaultAsync(e => e.Id == id);
            if (LoyaltyCard != null)
            {
                return Ok(LoyaltyCard);
            }
            return NotFound();
        }
        [HttpGet("inactive")]
        public async Task<ActionResult<List<LoyaltyCard>>> GetInactiveLoyaltyCards()
        {
            var loyaltyCards = await appDbContext.LoyaltyCards.ToListAsync();
            var inactiveLoyaltyCards = loyaltyCards.FindAll(e => e.Status == "Inactive");
            if (loyaltyCards != null)
            {
                return Ok(inactiveLoyaltyCards);
            }
            return NotFound();
        }

        //Add
        [HttpPost]
        public async Task<ActionResult<int>> AddLoyaltyCard(LoyaltyCard newLoyaltyCard)
            
        {
            var testCustomer = await appDbContext.Customers.FirstAsync(e => e.Id == newLoyaltyCard.CustomerId);

            if (newLoyaltyCard != null && testCustomer.Status=="Active")
            {
                try
                {
                    newLoyaltyCard.Status = "Active";
                    appDbContext.LoyaltyCards.Add(newLoyaltyCard);
                    await appDbContext.SaveChangesAsync();


                   
                    return Ok(newLoyaltyCard.Id);
                }
                catch (Exception ex){ NotFound(ex); }
            }
            return BadRequest();
        }

        //Delete
        [HttpDelete]
        public async Task<ActionResult<Boolean>> DeleteLoyaltyCard(int id)
        {
            var LoyaltyCard = await appDbContext.LoyaltyCards.FirstOrDefaultAsync(e => e.Id == id);
            if (LoyaltyCard != null)
            {
                try
                {
                    LoyaltyCard.Status = "Inactive";
                    await appDbContext.SaveChangesAsync();

                    
                    return Ok(true);
                }
                catch (Exception ex) { return NotFound(ex); }
            }
            else { return BadRequest(false); }
        }


        [HttpPut]
        public async Task<ActionResult<LoyaltyCard>> UpdateLoyaltyCard(LoyaltyCard updatedLoyaltyCard)
        {
            if (updatedLoyaltyCard != null)
            {
                var LoyaltyCard = await appDbContext.LoyaltyCards.FirstOrDefaultAsync(e => e.Id == updatedLoyaltyCard.Id);
                if (LoyaltyCard != null)
                {
                    try
                    {
                        LoyaltyCard.Id = updatedLoyaltyCard.Id;
                        LoyaltyCard.CustomerId = updatedLoyaltyCard.CustomerId;
                        LoyaltyCard.LoyaltyCardNo = updatedLoyaltyCard.LoyaltyCardNo;
                        LoyaltyCard.Amount = updatedLoyaltyCard.Amount;
                        LoyaltyCard.RegisterDate= updatedLoyaltyCard.RegisterDate;
                        if (updatedLoyaltyCard.Status == "Active" || updatedLoyaltyCard.Status == "Inactive") { LoyaltyCard.Status = updatedLoyaltyCard.Status; };



                        await appDbContext.SaveChangesAsync();

                        
                        return Ok(LoyaltyCard);
                    }
                    catch (Exception ex) { BadRequest(ex); }

                }
                else { return NotFound(); }
            }
            return BadRequest();
        }

    }
}