using loyaltycard.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using loyaltycard.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Net;

namespace loyaltycard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public AddressController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Address>>> GetAllAddresses()
        {
            var addresses = await appDbContext.Addresses.ToListAsync();
            var activeAddresses = addresses.FindAll(e => e.Status == "Active");
            
            return Ok(activeAddresses);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var Address = await appDbContext.Addresses.FirstOrDefaultAsync(e => e.Id == id);
            if (Address != null)
            {
                return Ok(Address);
            }
            return NotFound();
        }
        [HttpGet("inactive")]
        public async Task<ActionResult<List<Address>>> GetInactiveAddresses()
        {
            var Address = await appDbContext.Addresses.ToListAsync();
            var inactiveAddress = Address.FindAll(e => e.Status == "Inactive");
            if (Address != null)
            {
                return Ok(inactiveAddress);
            }
            return NotFound();
        }

        //Add
        [HttpPost]
        public async Task<ActionResult<int>> AddAddress(AddressRequestModel newAddress)
        {
            if (newAddress != null)
            {
                try
                {
                    newAddress.Status = "Active";
                    appDbContext.Addresses.Add(newAddress);
                    
                    await appDbContext.SaveChangesAsync();
                    try
                    {
                        if (newAddress.CustomerId !=0)
                        {
                            var newCustomerAddress = new CustomerAddress();
                            newCustomerAddress.CustomerId = newAddress.CustomerId;
                            newCustomerAddress.AddressId = newAddress.Id;
                            newCustomerAddress.Status = "Active";

                            appDbContext.CustomerAddresses.Add(newCustomerAddress);
                            await appDbContext.SaveChangesAsync();
                        }
                        if (newAddress.BranchId!=0)
                        {
                            var newBranchAddress = new BranchAddress();
                            newBranchAddress.BranchId = newAddress.BranchId;
                            newBranchAddress.AddressId = newAddress.Id;
                            newBranchAddress.Status = "Active";

                            appDbContext.BranchAddresses.Add(newBranchAddress);
                            await appDbContext.SaveChangesAsync();
                        }



                        
                    }
                    catch (Exception ex) { NotFound(ex); }



                    
                    return Ok(newAddress.Id);
                }
                catch (Exception ex) { NotFound(ex); }
            }
            return BadRequest();
        }

        //Delete
        [HttpDelete]
        public async Task<ActionResult<Boolean>> DeleteAddress(int id)
        {
            var Address = await appDbContext.Addresses.FirstOrDefaultAsync(e => e.Id == id);
            var CustomersAddress = await appDbContext.CustomerAddresses.FirstOrDefaultAsync(e => e.AddressId == id);
            var BranchsAddress = await appDbContext.BranchAddresses.FirstOrDefaultAsync(e=>e.AddressId==id);
            
            if (Address != null)
            {
                try
                {
                    if(CustomersAddress != null) { CustomersAddress.Status = "Inactive"; };
                    if(BranchsAddress!= null) { BranchsAddress.Status = "Inactive"; };
                    Address.Status="Inactive";
                    await appDbContext.SaveChangesAsync();

                  


                    

                    return Ok(true);
                }
                catch (Exception ex) { return NotFound(ex); }
            }
            else { return BadRequest(false); }
        }


        [HttpPut]
        public async Task<ActionResult<Address>> UpdateAdress(Address updatedAddress)
        {
            if (updatedAddress != null)
            {
                var Address = await appDbContext.Addresses.FirstOrDefaultAsync(e => e.Id == updatedAddress.Id);
                if (Address != null)
                {
                    try
                    {
                        Address.Id = updatedAddress.Id;
                        Address.AddressLine = updatedAddress.AddressLine;
                        Address.District = updatedAddress.District;
                        Address.City = updatedAddress.City;
                        if(updatedAddress.Status=="Active" || updatedAddress.Status == "Inactive") { Address.Status = updatedAddress.Status; };
                        



                        await appDbContext.SaveChangesAsync();


                        return Ok(Address);
                    }
                    catch (Exception ex) { BadRequest(ex); }

                }
                else { return NotFound(); }
            }
            return BadRequest();
        }

    }
}