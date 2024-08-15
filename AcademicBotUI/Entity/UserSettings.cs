using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicBotUI.Entity
{
    public class UserSettings
    {
        [Key]
        public Guid Id { get; set; }
        public string Value { get; set; }
        public Guid SettingId { get; set; }
        public string UserId { get; set; }

        [ForeignKey("SettingId")]
        public virtual Setting Setting { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

    }
}
