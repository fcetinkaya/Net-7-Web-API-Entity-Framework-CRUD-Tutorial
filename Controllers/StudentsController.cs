using dot7.API.Crud.Data;
using dot7.webapi.crud.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dot7.webapi.crud.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private readonly MyWorldDbContext _myWorldDbContext;

    public StudentController(MyWorldDbContext myWorldDbContext)
    {

        _myWorldDbContext = myWorldDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var students = await _myWorldDbContext.Students.ToListAsync();
        return Ok(students);
    }

    [HttpGet]
    [Route("get-student-by-id/{id:int}")]
    public async Task<IActionResult> GetStudentById(int studentId)
    {
        var student = await _myWorldDbContext.Students.FindAsync(studentId);
        return Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> AddStudent(Students student)
    {
        _myWorldDbContext.Students.Add(student);
        await _myWorldDbContext.SaveChangesAsync();
        return Created("get-student-by-id/{student.id}", student);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStudent(Students student)
    {
        _myWorldDbContext.Students.Update(student);
        await _myWorldDbContext.SaveChangesAsync();
        return NoContent();
    }

    [Route("{id:int}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _myWorldDbContext.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        _myWorldDbContext.Students.Remove(student);
        await _myWorldDbContext.SaveChangesAsync();
        return NoContent();
    }
}