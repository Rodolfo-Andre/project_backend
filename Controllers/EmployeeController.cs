using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employeeService;
        private readonly IRole _roleService;


        public EmployeeController(IEmployee employeeService, IRole roleService)
        {
            _employeeService = employeeService;
            _roleService = roleService;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeGet>>> GetEmployee()
        {
            return Ok((await _employeeService.GetAll()).Adapt<List<EmployeeGet>>());
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeGet>> GetEmployee(int id)
        {
            var employee = await _employeeService.GetById(id);

            if (employee == null)
            {
                return NotFound("Empleado no encontrado");
            }

            var employeeGet = employee.Adapt<EmployeeGet>();

            return Ok(employeeGet);
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<EmployeeGet>> UpdateEmployee(int id, [FromBody] EmployeeUpdate employeeUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _employeeService.GetById(id);

            if (employee == null)
            {
                return NotFound("Empleado no encontrado");
            }

            var isNotDniUnique = !await _employeeService.IsDniUnique(employeeUpdate.Dni, employee.Id);
            var isNotPhoneUnique = !await _employeeService.IsPhoneUnique(employeeUpdate.Phone, employee.Id);

            if (isNotDniUnique && isNotPhoneUnique)
            {
                return Conflict("El DNI y teléfono ya está en uso");
            }

            if (isNotDniUnique)
            {
                return Conflict("El DNI ya está en uso");
            }

            if (isNotPhoneUnique)
            {
                return Conflict("El teléfono ya está en uso");
            }


            if (employee.RoleId != employeeUpdate.RoleId)
            {
                var role = await _roleService.GetById(employeeUpdate.RoleId);

                if (role == null)
                {
                    return NotFound("Rol no encontrado");
                }

                employee.RoleId = employeeUpdate.RoleId;
            }

            employee.FirstName = employeeUpdate.FirstName;
            employee.LastName = employeeUpdate.LastName;
            employee.Phone = employeeUpdate.Phone;
            employee.Dni = employeeUpdate.Dni;
            employee.User.Email = employeeUpdate.User.Email;

            await _employeeService.UpdateEmployee(employee);

            var getEmployee = (await _employeeService.GetById(id)).Adapt<EmployeeGet>();

            return Ok(getEmployee);
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<EmployeeGet>> CreateEmployee([FromBody] EmployeeCreate employee)
        {
            if (!ModelState.IsValid) // Validar si el modelo es válido
            {
                return BadRequest(ModelState); // Devolver un BadRequest con los errores de validación
            }

            var isNotDniUnique = !await _employeeService.IsDniUnique(employee.Dni);
            var isNotPhoneUnique = !await _employeeService.IsPhoneUnique(employee.Phone);

            if (isNotDniUnique && isNotPhoneUnique)
            {
                return Conflict("El DNI y teléfono ya está en uso");
            }

            if (isNotDniUnique)
            {
                return Conflict("El DNI ya está en uso");
            }

            if (isNotPhoneUnique)
            {
                return Conflict("El teléfono ya está en uso");
            }

            var role = await _roleService.GetById(employee.RoleId);

            if (role == null)
            {
                return NotFound("Rol no encontrado");
            }

            var newEmployee = employee.Adapt<Employee>();

            await _employeeService.CreateEmployee(newEmployee);

            var getEmployee = (await _employeeService.GetById(newEmployee.Id)).Adapt<EmployeeGet>();

            return CreatedAtAction(nameof(GetEmployee), new { id = getEmployee.Id }, getEmployee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeService.GetById(id);

            if (employee == null)
            {
                return NotFound("Empleado no encontrado");
            }

            await _employeeService.DeleteEmployee(employee);

            return NoContent();
        }

        [HttpGet("{id}/number-commands")]
        public async Task<ActionResult<int>> GetNumberCommandsInEmployee(int id)
        {
            var employee = await _employeeService.GetById(id);

            if (employee == null)
            {
                return NotFound("Empleado no encontrado");
            }

            var count = await _employeeService.GetNumberCommandsInEmployee(employee.Id);

            return Ok(count);
        }
    }
}
