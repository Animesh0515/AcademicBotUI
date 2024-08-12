using System.ComponentModel.DataAnnotations;

namespace AcademicBotUI.Model
{
    public class EducationEntry
    {
        [Required(ErrorMessage = "Level of study is required")]
        public string LevelOfStudy { get; set; }
        
        [Required(ErrorMessage = "Field of study is required")]
        public string FieldOfStudy { get; set; }

        [Required(ErrorMessage = "At least one subject is required")]
        public List<SubjectEntry> Subjects { get; set; } = new List<SubjectEntry>();

        [Required(ErrorMessage = "Overall score is required")]
        public string OverallScore { get; set; }
    }

    public class SubjectEntry
    {
        [Required(ErrorMessage = "Subject name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Subject marks are required")]
        public string Marks { get; set; }
    }

}
