using loyaltycard.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using loyaltycard.Models;
using Microsoft.EntityFrameworkCore;


namespace loyaltycard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public EmployeeController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees()
        {
            var Employees = await appDbContext.Employees.ToListAsync();
            var activeEmployees = Employees.FindAll(e => e.Status == "Active");
            return Ok(activeEmployees);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> Getemployee(int id)
        {
            var Employee = await appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (Employee != null)
            {
                return Ok(Employee);
            }
            return NotFound();
        }
        [HttpGet("inactive")]
        public async Task<ActionResult<List<Employee>>> GetInactiveEmployees()
        {
            var employees = await appDbContext.Employees.ToListAsync();
            var inactiveEmployees = employees.FindAll(e => e.Status == "Inactive");
            if (employees != null)
            {
                return Ok(inactiveEmployees);
            }
            return NotFound();
        }

        //Add
        [HttpPost]
        public async Task<ActionResult<int>> AddEmployee(Employee newEmployee)
        {
            if (newEmployee != null)
            {
                try
                {
                    newEmployee.Status = "Active";
                    appDbContext.Employees.Add(newEmployee);
                    await appDbContext.SaveChangesAsync();


                   
                    
                    return Ok(newEmployee.Id);
                }
                catch (Exception ex) { NotFound(ex); }
            }
            return BadRequest();
        }

        //Delete
        [HttpDelete]
        public async Task<ActionResult<Boolean>> DeleteEmployee(int id)
        {
            var Employee = await appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (Employee != null)
            {
                try
                {
                    Employee.Status="Inactive";
                    await appDbContext.SaveChangesAsync();


                    return Ok(true);
                }
                catch (Exception ex) { return NotFound(ex); }
            }
            else { return BadRequest(false); }
        }


        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee updatedEmployee)
        {
            if (updatedEmployee != null)
            {
                var Employee = await appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == updatedEmployee.Id);
                if (Employee != null)
                {
                    try
                    {
                        Employee.Id = updatedEmployee.Id;
                        Employee.Name = updatedEmployee.Name;
                        Employee.BranchId = updatedEmployee.BranchId;
                        if (updatedEmployee.Status == "Active" || updatedEmployee.Status == "Inactive") { Employee.Status = updatedEmployee.Status; };



                        await appDbContext.SaveChangesAsync();


                        return Ok(Employee);
                    }
                    catch (Exception ex) { BadRequest(ex); }

                }
                else { return NotFound(); }
            }
            return BadRequest();
        }

    }
}