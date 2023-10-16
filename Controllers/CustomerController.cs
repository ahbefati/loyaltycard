using loyaltycard.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using loyaltycard.Models;
using Microsoft.EntityFrameworkCore;


namespace loyaltycard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public CustomerController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            var customer = await appDbContext.Customers.ToListAsync();
            var activeCustomer = customer.FindAll(e => e.Status == "Active");
            return Ok(activeCustomer);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await appDbContext.Customers.FirstOrDefaultAsync(e => e.Id == id);
            if (customer != null)
            {
                return Ok(customer);
            }
            return NotFound();
        }
        [HttpGet("inactive")]
        public async Task<ActionResult<List<Customer>>> GetInactiveCustomers()
        {
            var Customers = await appDbContext.Customers.ToListAsync();
            var inactiveCustomers = Customers.FindAll(e => e.Status == "Inactive");
            if (Customers != null)
            {
                return Ok(inactiveCustomers);
            }
            return NotFound();
        }

        //Add
        [HttpPost]
        public async Task<ActionResult<int>> AddCustomer(Customer newCustomer)
        {

            if (newCustomer != null)
            {
                try
                {
                    newCustomer.Status = "Active";
                    appDbContext.Customers.Add(newCustomer);
                    await appDbContext.SaveChangesAsync();


                  
                    

                    return Ok(newCustomer.Id);
                }
                catch (Exception ex) { NotFound(ex); }
            }
            return BadRequest();
        }

        //Delete
        [HttpDelete]
        public async Task<ActionResult<Boolean>> DeleteCustomer(int id)
        {
            var customer = await appDbContext.Customers.FirstOrDefaultAsync(e => e.Id == id);
            var customersAddress = await appDbContext.CustomerAddresses.FirstOrDefaultAsync(e => e.CustomerId == id);
            if (customer != null)
            {
                try
                {
                    customer.Status="Inactive";
                    if (customersAddress != null) { customersAddress.Status = "Inactive"; };
                    await appDbContext.SaveChangesAsync();

                    
                    return Ok(true);
                }
                catch (Exception ex){ return NotFound(ex); }
            }
            else { return Ok(false); };
        }


        [HttpPut]
        public async Task<ActionResult<Customer>> UpdateCustomer(Customer updatedCustomer)
        {
            if (updatedCustomer != null)
            {
                var customer = await appDbContext.Customers.FirstOrDefaultAsync(e => e.Id == updatedCustomer.Id);
                if (customer != null)
                {
                    customer.Id = updatedCustomer.Id;
                    customer.Name = updatedCustomer.Name;
                    customer.LastName = updatedCustomer.LastName;
                    customer.DayOfBirth = updatedCustomer.DayOfBirth;
                    customer.Mail = updatedCustomer.Mail;
                    if (updatedCustomer.Status == "Active" || updatedCustomer.Status == "Inactive") { customer.Status = updatedCustomer.Status; };
                    await appDbContext.SaveChangesAsync();

                    
                    return Ok(customer);
                }
                return NotFound();
            }
            return BadRequest();
        }

    }
}
