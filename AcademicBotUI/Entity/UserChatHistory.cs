using System.ComponentModel.DataAnnotations;

namespace AcademicBotUI.Entity
{
    public class UserChatHistory
    {
        public UserChatHistory()
        {
            Id= Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual List<ChatHistory> ChatHistories { get; set; } = new List<ChatHistory>();


    }
}
