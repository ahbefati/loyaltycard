using loyaltycard.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using loyaltycard.Models;
using Microsoft.EntityFrameworkCore;


namespace loyaltycard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchAddressController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public BranchAddressController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<BranchAddress>>> GetAllBranchAddresses()
        {
            var BranchAddresses = await appDbContext.BranchAddresses.ToListAsync();
            var ActiveBranchAddresses = BranchAddresses.FindAll(e => e.Status == "Active");
            return Ok(ActiveBranchAddresses);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BranchAddress>> GetBranchAddress(int id)
        {
            var BranchAddress = await appDbContext.BranchAddresses.FirstOrDefaultAsync(e => e.Id == id);
            if (BranchAddress != null)
            {
                return Ok(BranchAddress);
            }
            return NotFound();
        }
        [HttpGet("inactive")]
        public async Task<ActionResult<List<BranchAddress>>> GetInactiveBranchAddresses()
        {
            var BranchAddress = await appDbContext.BranchAddresses.ToListAsync();
            var inactiveBranchAddress = BranchAddress.FindAll(e => e.Status == "Inactive");
            if (inactiveBranchAddress != null)
            {
                return Ok(inactiveBranchAddress);
            }
            return NotFound();
        }

        //Add
        [HttpPost]
        public async Task<ActionResult<int>> AddBranchAdress(BranchAddress newBranchAddress)
            
        {
            var testBranch = await appDbContext.Branches.FirstOrDefaultAsync(e => e.Id == newBranchAddress.BranchId);
            var testAddress = await appDbContext.Addresses.FirstOrDefaultAsync(e => e.Id == newBranchAddress.AddressId);//making sure didnt entering a inactive branch or address
            if (newBranchAddress != null&&testBranch.Status=="Active"&&testAddress.Status=="Active")
            {
                try
                {
                     newBranchAddress.Status = "Active";
                    appDbContext.BranchAddresses.Add(newBranchAddress);
                   

                    await appDbContext.SaveChangesAsync();

                    
                    
                    return Ok(newBranchAddress.Id);
                }
                catch (Exception ex) { BadRequest(ex); }
            }
            return BadRequest("invalid branch or adress or null value");
        }

        //Delete
        [HttpDelete]
        public async Task<ActionResult<Boolean>> DeleteBranchAddress(int id)
        {
            var BranchAddress = await appDbContext.BranchAddresses.FirstOrDefaultAsync(e => e.Id == id);
            if (BranchAddress != null)
            {
                try
                {
                    BranchAddress.Status="Inactive";
                    await appDbContext.SaveChangesAsync();


                    return Ok(true);
                }
                catch (Exception ex) { return NotFound(ex); }
            }
            else { return BadRequest(false); }
        }


        [HttpPut]
        public async Task<ActionResult<BranchAddress>> UpdateBranchAddress(BranchAddress updatedBranchAddress)
        {
            if (updatedBranchAddress != null)
            {
                var BranchAddress = await appDbContext.BranchAddresses.FirstOrDefaultAsync(e => e.Id == updatedBranchAddress.Id);
                if (BranchAddress != null)
                {
                    try
                    {
                        BranchAddress.Id = updatedBranchAddress.Id;
                        BranchAddress.BranchId = updatedBranchAddress.BranchId;
                        BranchAddress.AddressId = updatedBranchAddress.AddressId;
                        if (updatedBranchAddress.Status == "Active" || updatedBranchAddress.Status == "Inactive") { BranchAddress.Status = updatedBranchAddress.Status; };



                        await appDbContext.SaveChangesAsync();


                        return Ok(BranchAddress);
                    }
                    catch (Exception ex) { BadRequest(ex); }

                }
                else { return NotFound(); }
            }
            return BadRequest();
        }

    }
}