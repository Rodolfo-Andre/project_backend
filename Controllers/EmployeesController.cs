using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;
using project_backend.Services;
using project_backend.Utils;

namespace project_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Administrador")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployee _employeeService;

        public EmployeesController(IEmployee employeeService, ILogger<EmployeesController> logger)
        {
            _employeeService = employeeService;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeGet>>> GetEmployee()
        {
            return (await _employeeService.GetAll()).Adapt<List<EmployeeGet>>();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeGet>> GetEmployee(int id)
        {
            
            var employee = await _employeeService.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeGet = employee.Adapt<EmployeeGet>();

            return employeeGet;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeGet>> PutEmployee(int id, EmployeePut employee)
        {
            if (id != employee.Id)
            {
                return BadRequest("El id del empleado no corresponde al empleado que intentas modificar");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateEmployee = await _employeeService.GetById(id);

            if (updateEmployee == null)
            {
                return NotFound();
            }

            updateEmployee.FirstName = employee.FirstName;
            updateEmployee.LastName = employee.LastName;
            updateEmployee.Phone = employee.Phone;
            updateEmployee.User.Email = employee.User.Email;
            updateEmployee.RoleId = employee.RoleId;

            await _employeeService.UpdateEmployee(updateEmployee);

            var getEmployee = (await GetEmployee(employee.Id)).Value;

            return StatusCode(200, getEmployee);
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeGet>> PostEmployee([FromBody] EmployeeCreate employee)
        {
            if (!ModelState.IsValid) // Validar si el modelo es válido
            {
                return BadRequest(ModelState); // Devolver un BadRequest con los errores de validación
            }

            var newEmployee = employee.Adapt<Employee>();

            await _employeeService.CreateEmployee(newEmployee);

            var getEmployee = (await GetEmployee(newEmployee.Id)).Value;

            return StatusCode(201, getEmployee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeService.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }

            await _employeeService.DeleteEmployee(employee);

            return NoContent();
        }
    }
}
