using AutoMapper;
using Employees.Admin.Data.Interfaces;
using Employees.Admin.Models;
using Employees.Admin.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Admin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly IMapper _mapper;
        private readonly IEmployeesRepository _employeesRepository;

        public EmployeesController(ILogger<EmployeesController> logger, IMapper mapper, IEmployeesRepository employeesRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _employeesRepository = employeesRepository ?? throw new ArgumentNullException(nameof(employeesRepository));
        }

        [HttpPost]
        public async Task<ActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                var employee = _mapper.Map<Employee>(employeeDto);
                var r = await _employeesRepository.CreateEmployee(employee);
                return Ok(r);
            }
            catch (SqlException sqlex)
            {
                return BadRequest(sqlex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdeteEmployee([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                var employee = _mapper.Map<Employee>(employeeDto);
                var r = await _employeesRepository.UpdateEmployee(employee);

                if (r == null)
                    return NotFound("Employee not found");
                else
                    return Ok(r);
            }
            catch (SqlException sqlex)
            {
                return BadRequest(sqlex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{Id}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteEmployee(Guid Id)
        {
            try
            {
                var r = await _employeesRepository.DeleteEmployee(Id);
                if (r == null)
                    return NotFound("Employee not found");
                else
                    return Ok(r);
            }
            catch (SqlException sqlex)
            {
                return BadRequest(sqlex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{Id}")]
        [HttpGet]
        public async Task<ActionResult> GetEmployee(Guid Id)
        {
            try
            {
                var r = await _employeesRepository.GetEmployee(Id);
                if (r == null)
                    return NotFound("Employee not found");
                else
                    return Ok(r);
            }
            catch (SqlException sqlex)
            {
                return BadRequest(sqlex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetEmployees([FromQuery] int typeConsult, [FromQuery] int status, [FromQuery] string name, [FromQuery]  string rfc )
        {
            try
            {
                name = name != null ? name : string.Empty;
                rfc = rfc != null ? rfc : string.Empty;

                var r = await _employeesRepository.GetEmployees(typeConsult, status, name, rfc);
                return Ok(r);
            }
            catch (SqlException sqlex)
            {
                return BadRequest(sqlex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
