using EmployeeAPICore.Common;
using EmployeeAPICore.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace EmployeeAPICore.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly EmployeeAPICoreDbContext _context;
        private readonly UnitOfWork.UnitOfWork _uow;
        public EmployeeServices(EmployeeAPICoreDbContext context)
        {
            _context = context;
            _uow = new UnitOfWork.UnitOfWork(_context);
        }

        public JsonResult GetEmployeeList()
        {
            return new JsonResult(
                _uow.Employees.GetAll()
                .Join(_uow.Departments.GetAll(),
                x => x.DepartmentId,
                y => y.Id,
                (x, y) => new
                {
                    Employee = x,
                    Department = y
                })
                .Select(x => new
                {
                    x.Employee.Id,
                    x.Employee.FirstName,
                    x.Employee.LastName,
                    x.Employee.Gender,
                    x.Employee.DateOfBirth,
                    x.Department.Name

                })
                .OrderBy(x => x.FirstName)
                .ToList()
                );
        }
        public JsonResult GetEmployeeDetailsById(Guid? employeeId)
        {
            return new JsonResult(
                _uow.Employees.Where(x => x.Id == employeeId)
                                .Join(_uow.Departments.GetAll(),
                                x => x.DepartmentId,
                                y => y.Id,
                                (x, y) => new
                                {
                                    Employee = x,
                                    Department = y
                                })
                                .Select(x => new
                                {
                                    x.Employee.Id,
                                    x.Employee.FirstName,
                                    x.Employee.LastName,
                                    x.Employee.Gender,
                                    x.Employee.DateOfBirth,
                                    x.Department.Name,
                                    DepartmentId = x.Department.Id
                                })
                                .FirstOrDefault()
                );
        }
        public JsonResult CreateEmployee(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            _uow.Employees.Add(employee);
            _uow.Save();
            Generator.IsReport = "Success";
            Generator.Message = "Employee Created Successfully";
            return new JsonResult(new
            {
                Generator.IsReport,
                Generator.Message
            });
        }
        public JsonResult UpdateEmployee(Employee employee)
        {
            var employeeToUpdate = _uow.Employees.GetById(employee.Id);
            if (employeeToUpdate != null)
            {
                employeeToUpdate.FirstName = employee.FirstName;
                employeeToUpdate.LastName = employee.LastName;
                employeeToUpdate.Gender = employee.Gender;
                employeeToUpdate.DateOfBirth = employee.DateOfBirth;
                employeeToUpdate.DepartmentId = employee.DepartmentId;
                _uow.Employees.Update(employeeToUpdate);
                _uow.Save();
                Generator.IsReport = "Success";
                Generator.Message = "Employee updated successfully";
            }
            else
            {
                Generator.IsReport = "Success";
                Generator.Message = "404 Not Found";
            }
            return new JsonResult(new
            {
                Generator.IsReport,
                Generator.Message
            });
        }
        public JsonResult DeleteEmployeeById(Guid? employeeId)
        {
            var employeeToDelete = _uow.Employees.GetById(employeeId);
            if (employeeToDelete != null)
            {
                _uow.Employees.Delete(employeeToDelete);
                _uow.Save();
                Generator.IsReport = "Success";
                Generator.Message = "Employee Deleted Successfully";
            }
            else
            {
                Generator.IsReport = "Error";
                Generator.Message = "404 Not Found";
            }
            return new JsonResult(new
            {
                Generator.IsReport,
                Generator.Message
            });
        }

        public JsonResult CreateDepartment(Department department)
        {
            department.Id = Guid.NewGuid();
            _uow.Departments.Add(department);
            _uow.Save();
            Generator.IsReport = "Success";
            Generator.Message = "Department Created Successfully";
            return new JsonResult(

                new
                {
                    Generator.IsReport,
                    Generator.Message
                });
        }
        public JsonResult UpdateDepartment(Department department)
        {
            var departmentToUpdate = _uow.Departments.GetById(department.Id);
            if (departmentToUpdate != null)
            {
                departmentToUpdate.Name = department.Name;
                _uow.Departments.Update(departmentToUpdate);
                _uow.Save();
                Generator.IsReport = "Success";
                Generator.Message = "Department updated successfully";
            }
            else
            {
                Generator.IsReport = "Error";
                Generator.Message = "404 Not Found";
            }

            return new JsonResult(new
            {
                Generator.IsReport,
                Generator.Message
            });
        }
        public JsonResult GetDepartmentList()
        {
            return new JsonResult(_uow.Departments.GetAll());
        }
    }
    public interface IEmployeeServices
    {
        JsonResult GetEmployeeList();
        JsonResult GetEmployeeDetailsById(Guid? employeeId);
        JsonResult CreateEmployee(Employee employee);
        JsonResult UpdateEmployee(Employee employee);
        JsonResult DeleteEmployeeById(Guid? employeeId);

        JsonResult CreateDepartment(Department department);
        JsonResult UpdateDepartment(Department department);
        JsonResult GetDepartmentList();
    }
}
