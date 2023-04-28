using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBApi_Dep_Dep_Employee.Data;
using WEBApi_Dep_Dep_Employee.Models;
using WEBApi_Dep_Dep_Employee.Models.ViewModels;

namespace WEBApi_Dep_Dep_Employee.Controllers
{

    [Route("api/employee")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetEmployee()
        {
            // Query the database to retrieve employee data, departments, and designations.
            var employeeFromDb = (from employee in _context.Employees
                                  join EmployeeDepartment in _context.DepartmentEmployees  //join is used to match EmployeeId between the Employees and DepartmentEmployees tables.
                                  on employee.EmployeeId equals EmployeeDepartment.EmployeeId //specifies that we want to join the Employees table with the DepartmentEmployees table on the EmployeeId column.
                                  select new EmployeeVMModel // specifies that we want to create a new EmployeeVMModel object for each row in the joined result set.
                                  {
                                      // Map the database results to an EmployeeVMModel ViewModel.
                                      EmployeeId = employee.EmployeeId,
                                      EmployeeName = employee.EmployeeName,
                                      EmployeeAddress = employee.EmployeeAddress,
                                      EmployeeSalary = employee.EmployeeSalary,
                                      EmployeeEmail = employee.EmployeeEmail,
                                      EmployeeAge = employee.EmployeeAge,
                                      DesignationName = employee.Designation.DesignationName,
                                      DesignationId = employee.DesignationId,
                                      DepartmentName = _context.DepartmentEmployees
                                          .Where(departmentEmployee => departmentEmployee.EmployeeId == employee.EmployeeId)
                                          .Select(employee => employee.Department.DepartmentName).ToList(),
                                      DepartmentId = _context.DepartmentEmployees
                                          .Where(departmentEmployee => departmentEmployee.EmployeeId == employee.EmployeeId)
                                          .Select(employee => employee.Department.DepartmentId).ToList(),
                                  });

            // Create an empty list of EmployeeVMModel objects.
            List<EmployeeVMModel> employeeVMModels = new List<EmployeeVMModel>();

            // Iterate over the mapped database results and check for duplicate employee IDs.
            foreach (var employee in employeeFromDb)
            {
                if (employeeVMModels.FirstOrDefault(employeeList => employeeList.EmployeeId == employee.EmployeeId) == null)
                {
                    // Add the EmployeeVMModel object to the employeeVMModels list if it does not exist.
                    employeeVMModels.Add(employee);
                }
            }

            // Return an HTTP Ok response with the employeeVMModels list.
            return Ok(employeeVMModels);
        }


        [HttpGet("{id:int}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employeeInDb = _context.Employees.Include(employee => employee.Designation).FirstOrDefault(employee => employee.EmployeeId == id);
            if (employeeInDb == null)
            {
                return NotFound();
            }
            var departmentInDb = _context.DepartmentEmployees
                .Where(departmentEmployee => departmentEmployee.EmployeeId == employeeInDb.EmployeeId)
                .Join(_context.Departments, departmentEmployee => departmentEmployee.DepartmentId, department => department.DepartmentId,
                (departmentEmployee, department) => new { department.DepartmentName });

            var departmentNamesList = departmentInDb.Select(d => d.DepartmentName).ToList();

            var employeeVMModels = new EmployeeVMModel
            {
                EmployeeId = employeeInDb.EmployeeId,
                EmployeeName = employeeInDb.EmployeeName,
                EmployeeAddress = employeeInDb.EmployeeAddress,
                EmployeeSalary = employeeInDb.EmployeeSalary,
                DepartmentName = departmentNamesList,
                DesignationId = employeeInDb.DesignationId,
                DesignationName = employeeInDb.Designation.DesignationName,
            };
            return Ok(employeeVMModels);
        }

        [HttpPost]
        public IActionResult SaveEmployee([FromBody] EmployeeVMModel employeeVMModel)
        {

            if (ModelState.IsValid && employeeVMModel != null)
            {
                var employee = new Employee
                {
                    EmployeeName = employeeVMModel.EmployeeName,
                    EmployeeAddress = employeeVMModel.EmployeeAddress,
                    EmployeeAge = employeeVMModel.EmployeeAge,
                    EmployeeEmail = employeeVMModel.EmployeeEmail,
                    EmployeeSalary = employeeVMModel.EmployeeSalary,
                    DesignationId = employeeVMModel.DesignationId
                };

                _context.Employees.Add(employee);
                _context.SaveChanges();

                var departmentEmployees = employeeVMModel.DepartmentId
                    .Select(departmentId => new DepartmentEmployee
                    {
                        EmployeeId = employee.EmployeeId,
                        DepartmentId = departmentId
                    })
                    .ToList();

                _context.DepartmentEmployees.AddRange(departmentEmployees);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        //[HttpPut]
        //public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeVMModel employeeVMModel)
        //{
        //    if (ModelState.IsValid && employeeVMModel != null)
        //    {
        //        // Updating and Saving Employees and Designation
        //        var employee = await _context.Employees.FindAsync(employeeVMModel.EmployeeId);
        //        employee.DesignationId = employeeVMModel.DesignationId;

        //        // Updating the assigned Departments to the Employees

        //        List<DepartmentEmployee> departmentEmployees = new List<DepartmentEmployee>();
        //        foreach (var empDep in employeeVMModel.DepartmentId)
        //        {
        //            if (!_context.DepartmentEmployees.Any(de => de.EmployeeId == employee.EmployeeId && de.DepartmentId == empDep))
        //            {
        //                DepartmentEmployee departmentEmployee = new DepartmentEmployee()
        //                {
        //                    EmployeeId = employee.EmployeeId,
        //                    DepartmentId = empDep
        //                };
        //                departmentEmployees.Add(departmentEmployee);
        //            }
        //        }
        //        _context.DepartmentEmployees.AddRange(departmentEmployees);
        //        _context.RemoveRange(_context.DepartmentEmployees.Where(departmentEmployee => departmentEmployee.EmployeeId == employee.EmployeeId && !employeeVMModel.DepartmentId.Contains(departmentEmployee.DepartmentId)));

        //        await _context.SaveChangesAsync();
        //        return Ok();
        //    }
        //    return BadRequest();
        //}
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeVMModel employeeVMModel)
        {
            if (employeeVMModel != null && ModelState.IsValid)
            {
                Employee employee = new Employee()
                {
                    EmployeeId = employeeVMModel.EmployeeId,
                    EmployeeName = employeeVMModel.EmployeeName,
                    EmployeeAddress = employeeVMModel.EmployeeAddress,
                    EmployeeSalary = employeeVMModel.EmployeeSalary,
                    EmployeeEmail = employeeVMModel.EmployeeEmail,
                    EmployeeAge = employeeVMModel.EmployeeAge,
                    DesignationId = employeeVMModel.DesignationId,
                };
                _context.Employees.Update(employee);
                _context.SaveChanges();

                // Updating skills to the user

                List<DepartmentEmployee> departmentEmployees = new List<DepartmentEmployee>();
                foreach (var depemp in employeeVMModel.DepartmentId)
                {
                    if (!_context.DepartmentEmployees.Any(de => de.EmployeeId == employee.EmployeeId && de.DepartmentId == depemp))
                    {
                        DepartmentEmployee departmentEmployee = new DepartmentEmployee()
                        {
                            EmployeeId = employee.EmployeeId,
                            DepartmentId = depemp
                        };
                        departmentEmployees.Add(departmentEmployee);
                    }
                }
                _context.DepartmentEmployees.AddRange(departmentEmployees);
                _context.RemoveRange(_context.DepartmentEmployees.Where(ed => ed.EmployeeId == employee.EmployeeId && !employeeVMModel.DepartmentId.Contains(ed.DepartmentId)));

                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var empIndb = await _context.Employees.FindAsync(id);
            if (empIndb == null) return NotFound();
            _context.Employees.Remove(empIndb);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

