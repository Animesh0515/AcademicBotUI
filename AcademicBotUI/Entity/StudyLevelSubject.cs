using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicBotUI.Entity
{
    public class StudyLevelSubject
    {
        public StudyLevelSubject()
        {
                Id= Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Marks { get; set; }

        public Guid UserEducationBackgroundId { get; set; }

        public virtual UserEducationBackground  UserEducationBackground { get; set; }
    }
}
