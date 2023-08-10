using System;
using System.ComponentModel.DataAnnotations;

namespace MyCosmosApi.Models
{
      [Key]
    public int Id { get; set; }
    
    public string UserId { get; set; }
    
    public string FullName { get; set; }

    // Alte proprietăți ale examinatorului

    // Metoda pentru obținerea examinatorului după ID-ul utilizatorului
    public static Examiner GetExaminerByUserId(ApplicationDbContext context, string userId)
    {
        return context.Examiners.FirstOrDefault(e => e.UserId == userId);
    }

}