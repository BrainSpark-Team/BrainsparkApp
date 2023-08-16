using BrainSpark.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrainSpark.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private static List<ExamCategory> Categories = new List<ExamCategory>
    {
        new  ExamCategory
        {
            ImgUrl = "/Images/img1.jpg",
            ImgAlt = "Nature picture",
            TimeLimit = "No time limit",
            Title = "Nature Exam",
            Questions = 20,
            Difficulty = "Easy"
        },
        new  ExamCategory
        {
            ImgUrl = "/Images/img1.jpg",
            TimeLimit = "No time limit",
            Title = "Nature Exam2",
            Questions = 15,
            Difficulty = "Easy"
        },
        new  ExamCategory
        {
            ImgUrl = "/Images/img1.jpg",
            TimeLimit = "No time limit",
            Title = "Nature Exam3",
            Questions = 12,
            Difficulty = "Easy"
        },
        new  ExamCategory
        {
            ImgUrl = "/Images/img1.jpg",
            TimeLimit = "No time limit",
            Title = "Nature Exam4",
            Questions = 12,
            Difficulty = "Easy"
        },
        new  ExamCategory
        {
            ImgUrl = "/Images/img1.jpg",
            TimeLimit = "No time limit",
            Title = "Nature Exam5",
            Questions = 12,
            Difficulty = "Easy"
        }
    };
        [HttpGet]
        public async Task<IActionResult> GetCategories() {
            return Ok(Categories);
        }
    }
}
