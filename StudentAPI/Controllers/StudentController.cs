using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Interfaces;
using StudentAPI.Models;
using System;

namespace StudentAPI.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [Produces("application/json")]
        [HttpPost("CreateStudent")]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            _studentService.CreateStudent(student);
            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }

        [Produces("application/json")]
        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAll()
        {
            var result = _studentService.GetAllStudents();
            return Ok(result);
        }

        [Produces("application/json")]
        [HttpGet("GetStudentById")]
        public async Task<IActionResult> GetStudentById(int Id)
        {
            var result = _studentService.GetStudentById(Id);
            return Ok(result);
        }

        [Produces("application/json")]
        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(Student student)
        {
            var result = _studentService.UpdateStudent(student);
            return Ok(result);
        }

        [Produces("application/json")]
        [HttpDelete("DeleteStudent")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = _studentService.DeleteStudent(Id);
            return Ok(result);
        }

    }
}
