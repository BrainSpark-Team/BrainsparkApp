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
            CategoryId = 0,
            CategoryType= "Nature",
            ImgUrl = "/Images/img1.jpg",
            ImgAlt = "Nature picture",
            TimeLimit = "No time limit",
            Title = "Nature Exam",
            Questions = 20,
            Difficulty = "Easy"
        },
        new  ExamCategory
        {
			CategoryId = 1,
            CategoryType= "Nature",
            ImgUrl = "/Images/img2.jpg",
            TimeLimit = "No time limit",
            Title = "Nature Exam2",
            Questions = 15,
            Difficulty = "Easy"
        },
        new  ExamCategory
        {
            CategoryId = 2,
            CategoryType= "Biology",
            ImgUrl = "/Images/img2.jpg",
            TimeLimit = "No time limit",
            Title = "Nature Exam3",
            Questions = 12,
            Difficulty = "Easy"
        },
        new  ExamCategory
        {
            CategoryId = 3,
            CategoryType= "Biology",
            ImgUrl = "/Images/img1.jpg",
            TimeLimit = "No time limit",
            Title = "Nature Exam4",
            Questions = 12,
            Difficulty = "Easy"
        },
        new  ExamCategory
        {
            CategoryId = 4,
            CategoryType= "Biology",
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

		[HttpGet("{categoryId}")]
		public async Task<IActionResult> GetCategory(int categoryId)
		{
			ExamCategory category = Categories.FirstOrDefault(c => c.CategoryId == categoryId);

			if (category != null)
			{
				return Ok(category);
			}
			else
			{
				return NotFound();
			}
		}
	}
}
