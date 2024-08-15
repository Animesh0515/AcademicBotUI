using AcademicBorUI.Data;
using AcademicBotUI.Entity;
using AcademicBotUI.ViewModel;
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

        public bool IncludeEducationBackground { get; set; }
        public List<UserChatHistoryViewModel> UserChatHistories { get; set; } = new List<UserChatHistoryViewModel>();
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
            if (_signInManager.IsSignedIn(User))
            {
                var userID = _userManager.GetUserId(User);
                var chatHistory = _applicationDbContext.UserChatHistory
                                .Where(x => x.UserId == userID)
                                .Include(x => x.ChatHistories)
                                .OrderByDescending(x => x.ModifiedDate)
                                .ToList();
                foreach (UserChatHistory uch in chatHistory)
                {
                    string userInput = uch.ChatHistories.OrderBy(x => x.CreatedDate).Select(x => x.UserInput).First();
                    UserChatHistories.Add(
                     new UserChatHistoryViewModel()
                     {
                         UserChatHistoryId = uch.Id,
                         DisplayMessage = userInput
                     }
                    );
                }
                var value = _applicationDbContext.UserSettings.Where(x => x.SettingId == Guid.Parse("b3abe086-70b3-4add-b7b3-b6334a667257") && x.UserId == userID)?.Select(x => x.Value).FirstOrDefault();
                if(String.IsNullOrEmpty(value))
                {
                    IncludeEducationBackground=true;
                }
                else
                {
                    IncludeEducationBackground=bool.Parse(value);
                }

            }

        }

        public async Task<IActionResult> OnGetGetChatHistory(Guid chatID)
        {
            List<ChatHistoryViewModel> chatHistoryViewModelList = new List<ChatHistoryViewModel>();
            var chatHistories = await _applicationDbContext.UserChatHistory.Where(x => x.Id == chatID).Select(x => x.ChatHistories.OrderBy(x => x.CreatedDate).ToList()).FirstAsync();
            foreach (var h in chatHistories)
            {
                ChatHistoryViewModel chatHistoryViewModel = new ChatHistoryViewModel()
                {
                    sender = "user",
                    content = h.UserInput
                };
                chatHistoryViewModelList.Add(chatHistoryViewModel);

                ChatHistoryViewModel chatHistoryViewModel1 = new ChatHistoryViewModel()
                {
                    sender = "bot",
                    content = h.Response
                };
                chatHistoryViewModelList.Add(chatHistoryViewModel1);
            }

            return new JsonResult(new { data = chatHistoryViewModelList, sucess = true });
        }
        public async Task<IActionResult> OnGetCallRagAPI(string userInput, bool includeBackground, Guid? chatID)
        {
            try
            {

                var userID = _userManager.GetUserId(User);
                string userBackground = String.Empty;
                if (_signInManager.IsSignedIn(User) && includeBackground)
                {
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
                var request = new HttpRequestMessage(HttpMethod.Post, ragAPIUrl + "/api/rag");
                var content = new StringContent(JsonSerializer.Serialize(new { user_input = userInput, user_background = userBackground }), null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseData = await response.Content.ReadAsStringAsync();
                Guid? savedChatID = null;
                if (_signInManager.IsSignedIn(User))
                {
                     savedChatID = await saveChat(userInput, responseData, userID, chatID);
                }
                return new JsonResult(new { Message = responseData, chatId= savedChatID, success = true });

            }
            catch (Exception ex)
            {
                return new JsonResult(new { Message = ex, sucess = false });
            }
        }

        public async Task<Guid> saveChat(string userInput, string response, string userID, Guid? userChatHistoryID)
        {
           
                Guid chatID;
                if (userChatHistoryID == null)
                {
                    UserChatHistory userChatHistory = new UserChatHistory()
                    {
                        UserId = userID,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                    };

                    ChatHistory chatHistory = new ChatHistory()
                    {
                        UserInput = userInput,
                        Response = response,
                        CreatedDate = DateTime.Now
                    };
                    userChatHistory.ChatHistories.Add(chatHistory);
                    _applicationDbContext.UserChatHistory.Add(userChatHistory);
                    chatID = userChatHistory.Id;
                }
                else
                {
                    chatID = (Guid)userChatHistoryID;
                    var userChatHistories = _applicationDbContext.UserChatHistory.Include(x => x.ChatHistories).FirstOrDefault(x => x.UserId == userID && x.Id == userChatHistoryID);
                    if (userChatHistories != null)
                    {
                        userChatHistories.ModifiedDate = DateTime.Now;
                        ChatHistory chatHistory = new ChatHistory()
                        {
                            UserInput = userInput,
                            Response = response,
                            CreatedDate = DateTime.Now,
                            UserChatHistoryId = chatID
                        };
                        _applicationDbContext.ChatHistory.Add(chatHistory);
                    }
                    

                }
                await _applicationDbContext.SaveChangesAsync();
                return chatID;
        }

        public async Task<IActionResult> OnGetUpdateSetting(string checkedvalue)
        {
            var userID = _userManager.GetUserId(User);

            UserSettings userSetting= _applicationDbContext.UserSettings.Where(x => x.SettingId == Guid.Parse("b3abe086-70b3-4add-b7b3-b6334a667257") && x.UserId == userID).FirstOrDefault();
            if (userSetting != null)
            {
                userSetting.Value= checkedvalue;
            }

            await _applicationDbContext.SaveChangesAsync();
            return new JsonResult(new {sucess = true });

        }
    }
}
