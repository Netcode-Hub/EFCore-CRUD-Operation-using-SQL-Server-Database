using EFCoreRelationships.Data;
using EFCoreRelationships.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationships.Controllers
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

        //Create
        [HttpPost]
        public async Task<ActionResult<List<Employee>>> AddEmployee(Employee newEmployee)
        {
            if(newEmployee != null)
            {
                appDbContext.Employees.Add(newEmployee);
                await appDbContext.SaveChangesAsync();
                return Ok(await appDbContext.Employees.ToListAsync());
            }
            return BadRequest();
        }

        //Read All Employees
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees()
        {
            var employees = await appDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        //Read single Employee
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee != null)
            {
                return Ok(employee);
            }
            return NotFound();
        }

        //Update
        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee updatedEmployee)
        {
            if(updatedEmployee != null)
            {
                var employee = await appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == updatedEmployee.Id);
                employee.Name = updatedEmployee.Name;
                employee.Age = updatedEmployee.Age;
                await appDbContext.SaveChangesAsync();
                return Ok(employee);
            }
            return BadRequest();
        }

        //Delete
        [HttpDelete]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            var employee = await appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if(employee != null)
            {
                appDbContext.Employees.Remove(employee);
                await appDbContext.SaveChangesAsync();
                return Ok(await appDbContext.Employees.ToListAsync());
            }
            return NotFound();
        }

    }
}
