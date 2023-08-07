using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using MyCosmosApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Security; 
using Microsoft.AspNetCore.Authorization;


namespace MyCosmosApi.Controllers
{
   // ExamsController.cs
[ApiController]
[Route("api/[controller]")]
public class ExamsController : ControllerBase
{
    [Authorize(Roles = "Admin")]
    public class ExamsController : ControllerBase
    {
        // This action requires the "Admin" role to access
        [HttpGet("admin-only")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult AdminOnlyAction()
        {
            return Ok("This is an admin-only endpoint.");
        }
    }
    private readonly ApplicationDbContext _context;

    public ExamsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetExams()
    {
        var exams = _context.Exams.ToList();
        return Ok(exams);
    }

    [HttpPost]
    public IActionResult CreateExam(Exam exam)
    {
        _context.Exams.Add(exam);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetExams), new { id = exam.Id }, exam);
    }

    // Metodă pentru obținerea examenului curent al examinatorului
    [HttpGet("current")]
    [Authorize(Roles = "examiner")]
    public IActionResult GetCurrentExam()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var examiner = Examiner.GetExaminerByUserId(_context, userId);

        if (examiner == null)
        {
            return NotFound("Examiner not found.");
        }

        var currentExam = _context.Exams.FirstOrDefault(e => e.IsActive());
        
        if (currentExam == null)
        {
            return NotFound("No active exams.");
        }

        return Ok(currentExam);
    }

    // Alte metode pentru actualizarea, ștergerea examenelor, adăugarea întrebărilor etc.
}

}
