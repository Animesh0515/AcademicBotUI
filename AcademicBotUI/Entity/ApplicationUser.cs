using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AcademicBotUI.Entity
{
    public class ApplicationUser:IdentityUser
    {
        public ApplicationUser():base()
        {
            
        }
        public ApplicationUser(string userName) : base(userName)
        {
        }

        [Required]
        public string FieldsOfInterest { get; set; }

        public string? KeySkills { get; set; }

        public string? NotableAchievements { get; set; }

        public virtual List<UserEducationBackground> UserEducationBackground { get; set; }= new List<UserEducationBackground>();
    }
}
