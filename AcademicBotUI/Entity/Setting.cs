using System.ComponentModel.DataAnnotations;

namespace AcademicBotUI.Entity
{
    public class Setting
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public virtual List<UserSettings> UserSettings { get; set; } = new List<UserSettings>();
    }
}
