using AcademicBorUI.Data;
using AcademicBotUI.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AcademicBorUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration, SignInManager<ApplicationUser> signInManager, ApplicationDbContext applicationDbContext,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnGetCallRagAPI(string userInput)
        {
            try
            {
                string userBackground = String.Empty;
                if (_signInManager.IsSignedIn(User))
                {
                    var userID = _userManager.GetUserId(User);
                    var userDetails = _applicationDbContext.Users.Include(x => x.UserEducationBackground).ThenInclude(x => x.Subjects).FirstOrDefault(x => x.Id == userID);
                    if (userDetails != null)
                    {
                        if (userDetails.UserEducationBackground.Any())
                        {
                            foreach (var eb in userDetails.UserEducationBackground)
                            {
                                userBackground += "Degree:" + eb.LevelOfStudy + "\n\n";
                                userBackground += "Field of Study:" + eb.FieldOfStudy + "\n\n";
                                if (eb.Subjects.Any())
                                {
                                    userBackground += "Subjects and their marks:\n\n";

                                    foreach (var s in eb.Subjects)
                                    {
                                        userBackground += s.Name + "=" + s.Marks + "\n\n";
                                    }
                                }
                                userBackground += "Overall Score:" + eb.OverallScore + "\n\n";
                            }
                        }
                        if (!String.IsNullOrEmpty(userDetails.FieldsOfInterest))
                        {
                            userBackground += "Field Of Interest:" + userDetails.FieldsOfInterest + "\n\n";
                        }
                        if (!String.IsNullOrEmpty(userDetails.KeySkills))
                        {
                            userBackground += "Key Skills:" + userDetails.KeySkills + "\n\n";
                        }
                        if (!String.IsNullOrEmpty(userDetails.NotableAchievements))
                        {
                            userBackground += "Achievements:" + userDetails.NotableAchievements + "\n\n";
                        }


                    }
                }
                string ragAPIUrl = _configuration["ApiUrl"];
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://127.0.0.1:8000/api/rag");
                var content = new StringContent(JsonSerializer.Serialize(new { user_input = userInput, user_background = userBackground }), null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return new JsonResult(new { Message = response.Content.ReadAsStringAsync(), success = true });

            }
            catch (Exception ex)
            {
                return new JsonResult(new { Message = ex, sucess = false });
            }
        }
    }
}
