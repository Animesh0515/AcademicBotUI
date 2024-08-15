using AcademicBotUI.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AcademicBorUI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserEducationBackground> UserEducationBackground { get; set; }
        public virtual DbSet<StudyLevelSubject> StudyLevelSubject { get; set; }
        public virtual DbSet<UserChatHistory> UserChatHistory { get; set; }
        public virtual DbSet<ChatHistory> ChatHistory { get; set; }
        public virtual DbSet<Setting> Setting { get; set; }
        public virtual DbSet<UserSettings> UserSettings { get; set; }

    }
}
