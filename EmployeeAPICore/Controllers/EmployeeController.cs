using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAPICore.Model;
using EmployeeAPICore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPICore.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _services;
        public EmployeeController(IEmployeeServices services)
        {
            _services = services;
        }
        [Route("GetEmployeeList")]
        [HttpGet]
        public IActionResult GetEmployeeList()
        {
            return Ok(_services.GetEmployeeList().Value);
        }
        [Route("GetEmployeeDetailsById/{employeeId}")]
        [HttpGet]
        public IActionResult GetEmployeeDetailsById(Guid? employeeId)
        {
            return Ok(_services.GetEmployeeDetailsById(employeeId).Value);
        }
        [Route("DeleteEmployeeById/{employeeId}")]
        [HttpGet]
        public IActionResult DeleteEmployeeById(Guid? employeeId)
        {
            return Ok(_services.DeleteEmployeeById(employeeId).Value);
        }

        [Route("CreateEmployee")]
        [HttpPost]
        public IActionResult CreateEmployee(Employee employee)
        {
            return Ok(_services.CreateEmployee(employee).Value);
        }

        [Route("UpdateEmployee")]
        [HttpPost]
        public IActionResult UpdateEmployee(Employee employee)
        {
            return Ok(_services.UpdateEmployee(employee).Value);
        }

        [Route("GetDepartmentList")]
        [HttpGet]
        public IActionResult GetDepartmentList()
        {
            return Ok(_services.GetDepartmentList().Value);
        }

        [Route("CreateDepartment")]
        [HttpPost]
        public IActionResult CreateDepartment(Department department)
        {
            return Ok(_services.CreateDepartment(department).Value);
        }

        [Route("UpdateDepartment")]
        [HttpPost]
        public IActionResult UpdateDepartment(Department department)
        {
            return Ok(_services.UpdateDepartment(department).Value);
        }
    }
}