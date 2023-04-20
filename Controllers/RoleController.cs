﻿using Mapster;
using Microsoft.AspNetCore.Mvc;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public readonly IRole _roleService;

        public RoleController(IRole roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleGet>>> GetRole()
        {
            return Ok((await _roleService.GetAll()).Adapt<List<RoleGet>>());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<RoleGet>> GetRole(int id)
        {
            var role = await _roleService.GetById(id);

            if (role == null)
            {
                return NotFound("Rol no encontrado");
            }

            var roleGet = role.Adapt<RoleGet>();

            return Ok(roleGet);
        }

        [HttpPost]
        public async Task<ActionResult<RoleGet>> CreateRole([FromBody] RolePrincipal role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newRole = role.Adapt<Role>();

            await _roleService.CreateRole(newRole);

            var getRole = (await _roleService.GetById(newRole.Id)).Adapt<RoleGet>();

            return CreatedAtAction(nameof(GetRole), new { id = getRole.Id }, getRole);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RoleGet>> UpdateRole(int id, [FromBody] RolePrincipal roleUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = await _roleService.GetById(id);

            if (role == null)
            {
                return NotFound("Rol no encontrado");
            }

            role.RoleName = roleUpdate.RoleName;

            await _roleService.UpdateRole(role);

            var getRole = (await _roleService.GetById(id)).Adapt<RoleGet>();

            return Ok(getRole);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            var role = await _roleService.GetById(id);

            if (role == null)
            {
                return NotFound("Rol no encontrado");
            }

            await _roleService.DeleteRole(role);

            return NoContent();
        }
    }
}
