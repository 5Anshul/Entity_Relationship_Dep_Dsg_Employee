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

namespace WEBApi_Dep_Dep_Employee.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    [Route("api/department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetDepartment()
        {
            var departmentInDb = from Department in _context.Departments  // to retrieve all deparment records from the department table
                                 select new Department      //select is used to create a new Department object for each department record with only two properties
                                 {
                                     DepartmentId = Department.DepartmentId,
                                     DepartmentName = Department.DepartmentName
                                 };
            return Ok(departmentInDb);
        }
        [HttpGet("{id}")]
        public IActionResult GetDepartmentByEmployeeId(int id) // here we are getting department by employeeId 
        {
            var departmentFromDb = _context.DepartmentEmployees.Include(e => e.Department).Where(ep => ep.EmployeeId == id).ToList();
            if (departmentFromDb == null) return NotFound();
            return Ok(departmentFromDb);
        }
        [HttpPost]
        public IActionResult SaveDepartment([FromBody] Department department)// in this we are simply adding department and saving them in database
        {
            if (department != null && ModelState.IsValid)
            {
                _context.Departments.Add(department);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult UpdateDepartment([FromBody] Department department)// we are updating department
        {
            if (department != null && ModelState.IsValid)
            {
                _context.Departments.Update(department);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteDepartment(int id)// we are deleting data by id
        {
            var depfromdb = _context.Departments.Find(id);
            if (depfromdb == null) return NotFound();
            _context.Departments.Remove(depfromdb);
            _context.SaveChanges();
            return Ok();
        }
    }

}
