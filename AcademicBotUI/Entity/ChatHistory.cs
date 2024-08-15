using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicBotUI.Entity
{
    public class ChatHistory
    {
        public ChatHistory()
        {
            ChatHistoryId= Guid.NewGuid();
        }
        [Key]
        public Guid ChatHistoryId { get; set; }
        public string UserInput { get; set; }
        public string Response { get; set; }
        public Guid UserChatHistoryId { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("UserChatHistoryId")]
        public virtual UserChatHistory UserChatHistory { get; set; }
    }
}
