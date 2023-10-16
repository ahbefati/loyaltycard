using loyaltycard.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using loyaltycard.Models;
using Microsoft.EntityFrameworkCore;


namespace loyaltycard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public BranchController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Branch>>> GetAllBranches()
        {
            var branches = await appDbContext.Branches.ToListAsync();
            var activeBranches = branches.FindAll(e => e.Status == "Active");
            return Ok(activeBranches);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Branch>> GetBranch(int id)
        {
            var Branch = await appDbContext.Branches.FirstOrDefaultAsync(e => e.Id == id);
            if (Branch != null)
            {
                return Ok(Branch);
            }
            return NotFound();
        }
        [HttpGet("inactive")]
        public async Task<ActionResult<List<Branch>>> GetInactiveBranches()
        {
            var Branches = await appDbContext.Branches.ToListAsync();
            var inactiveBranches = Branches.FindAll(e => e.Status == "Inactive");
            if (Branches != null)
            {
                return Ok(inactiveBranches);
            }
            return NotFound();
        }

        //Add
        [HttpPost]
        public async Task<ActionResult<int>> AddBranch(Branch newBranch)
        {
            if (newBranch != null)
            {
                try
                {
                    newBranch.Status = "Active";
                    appDbContext.Branches.Add(newBranch);
                    await appDbContext.SaveChangesAsync();
                    


                    return Ok(newBranch.Id);
                }
                catch (Exception ex) { NotFound(ex); }
            }
            return BadRequest();
        }

        //Delete
        [HttpDelete]
        public async Task<ActionResult<Boolean>> DeleteBranch(int id)
        {
            var Branch = await appDbContext.Branches.FirstOrDefaultAsync(e => e.Id == id);
            var BranchsAddress = await appDbContext.BranchAddresses.FirstOrDefaultAsync(e => e.BranchId == id);
            
            if (Branch != null)
            {
                try
                {

                    if (BranchsAddress != null) { BranchsAddress.Status = "Inactive"; };
                    Branch.Status="Inactive";
                    await appDbContext.SaveChangesAsync();


                    return Ok(true);
                }
                catch (Exception ex) { return NotFound(ex); }
            }
            else { return BadRequest(false); }
        }


        [HttpPut]
        public async Task<ActionResult<Branch>> UpdateBranch(Branch updatedBranch)
        {
            if (updatedBranch != null)
            {
                var Branch = await appDbContext.Branches.FirstOrDefaultAsync(e => e.Id == updatedBranch.Id);
                if (Branch != null)
                {
                    try
                    {
                        Branch.Id = updatedBranch.Id;
                        Branch.Name = updatedBranch.Name;
                        if (updatedBranch.Status == "Active" || updatedBranch.Status == "Inactive") { Branch.Status = updatedBranch.Status; };



                        await appDbContext.SaveChangesAsync();


                        return Ok(Branch);
                    }
                    catch (Exception ex) { BadRequest(ex); }

                }
                else { return NotFound(); }
            }
            return BadRequest();
        }

    }
}