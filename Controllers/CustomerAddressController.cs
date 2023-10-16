using loyaltycard.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using loyaltycard.Models;
using Microsoft.EntityFrameworkCore;


namespace loyaltycard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public CustomerAddressController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<CustomerAddress>>> GetAllCustomerAddresses()
        {
            var CustomerAddresses = await appDbContext.CustomerAddresses.ToListAsync();
            var activeCustomerAddresses = CustomerAddresses.FindAll(e => e.Status == "Active");
            return Ok(activeCustomerAddresses);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerAddress>> GetcustomerAddress(int id)
        {
            var CustomerAddress = await appDbContext.CustomerAddresses.FirstOrDefaultAsync(e => e.Id == id);
            if (CustomerAddress != null)
            {
                return Ok(CustomerAddress);
            }
            return NotFound();
        }
        [HttpGet("inactive")]
        public async Task<ActionResult<List<CustomerAddress>>> GetInactiveCustomerAddresses()
        {
            var CustomerAddress = await appDbContext.CustomerAddresses.ToListAsync();
            var inactiveCustomerAddress = CustomerAddress.FindAll(e => e.Status == "Inactive");
            if (CustomerAddress != null)
            {
                return Ok(inactiveCustomerAddress);
            }
            return NotFound();
        }

        //Add
        [HttpPost]
        public async Task<ActionResult<int>> AddCustomerAddress(CustomerAddress newCustomerAddress)
        {
            var testCustomer = await appDbContext.Customers.FirstOrDefaultAsync(e => e.Id == newCustomerAddress.CustomerId);
            var testAddress = await appDbContext.Addresses.FirstOrDefaultAsync(e => e.Id == newCustomerAddress.AddressId);
            if (testCustomer != null && testAddress != null)
            {
                if (newCustomerAddress != null && testCustomer.Status == "Active" && testAddress.Status == "Active")
                {
                    try
                    {
                        newCustomerAddress.Status = "Active";
                        appDbContext.CustomerAddresses.Add(newCustomerAddress);
                        await appDbContext.SaveChangesAsync();




                        return Ok(newCustomerAddress.Id);
                    }
                    catch (Exception ex) { NotFound(ex); }
                }
            }
            return BadRequest();
        }

        //Delete
        [HttpDelete]
        public async Task<ActionResult<Boolean>> DeleteCustomerAddress(int id)
        {
            var CustomerAddress = await appDbContext.CustomerAddresses.FirstOrDefaultAsync(e => e.Id == id);
            if (CustomerAddress != null)
            {
                try
                {
                    CustomerAddress.Status = "Inactive";
                    await appDbContext.SaveChangesAsync();


                    return Ok(true);
                }
                catch (Exception ex) { return NotFound(ex); }
            }
            else { return BadRequest(false); }
        }


        [HttpPut]
        public async Task<ActionResult<CustomerAddress>> UpdateCustomerAddress(CustomerAddress updatedCustomerAddress)
        {
            if (updatedCustomerAddress != null)
            {
                var CustomerAddress = await appDbContext.CustomerAddresses.FirstOrDefaultAsync(e => e.Id == updatedCustomerAddress.Id);
                if (CustomerAddress != null)
                {
                    try
                    {
                        CustomerAddress.Id = updatedCustomerAddress.Id;
                        CustomerAddress.CustomerId = updatedCustomerAddress.CustomerId;
                        CustomerAddress.AddressId = updatedCustomerAddress.AddressId;



                        await appDbContext.SaveChangesAsync();


                        return Ok(CustomerAddress);
                    }
                    catch (Exception ex) { BadRequest(ex); }

                }
                else { return NotFound(); }
            }
            return BadRequest();
        }

    }
}
