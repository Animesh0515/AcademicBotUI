using AcademicBotUI.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicBotUI.Entity
{
    public class UserEducationBackground
    {
        public UserEducationBackground()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string LevelOfStudy { get; set; }

        [Required]
        public string FieldOfStudy { get; set; }

        [Required]
        public string OverallScore { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public virtual List<StudyLevelSubject> Subjects { get; set; } = new List<StudyLevelSubject>();
    }
}
