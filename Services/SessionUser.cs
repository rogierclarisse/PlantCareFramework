using PlantCareFramework.Areas.Identity.Data;
using PlantCareFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace PlantCareFramework.Services
{
    public class SessionUser
    {
        static Timer CleanUpTimer;
        class UserStats
        {
            public DateTime FirstEntered { get; set; }
            public DateTime LastEntered { get; set; }
            public int Count { get; set; }
            public ApplicationUser User { get; set; }
        }


        readonly RequestDelegate _next;
        static Dictionary<string, UserStats> UserDictionary = new Dictionary<string, UserStats>();

        public SessionUser(RequestDelegate next)
        {
            _next = next;
            //CleanUpTimer = new Timer(CleanUp, null, 21600000, 21600000);
        }

        public async Task Invoke(HttpContext httpContext, ApplicationDbContext dbContext)
        {
            string name = httpContext.User.Identity.Name == null ? "-" : httpContext.User.Identity.Name;
            try
            {
                UserStats us = UserDictionary[name];
                us.Count++;
                us.LastEntered = DateTime.Now;
            }
            catch
            {
                UserDictionary[name] = new UserStats
                {
                    User = dbContext.Users.FirstOrDefault(u => u.UserName == name),
                    Count = 1,
                    LastEntered = DateTime.Now
                };
            }
        }


        public static ApplicationUser GetUser(string? userName)
        {
            return UserDictionary[userName == null ? "-" : userName].User;
        }

        private void CleanUp(object? state)
        {
            DateTime checkTime = DateTime.Now - new TimeSpan(0, 6, 0, 0, 0);
            Dictionary<string, UserStats> remove = new Dictionary<string, UserStats>();
            foreach (KeyValuePair<string, UserStats> us in UserDictionary)
            {
                if (us.Value.LastEntered < checkTime)
                    remove[us.Key] = us.Value;
            }
            foreach (KeyValuePair<string, UserStats> us in remove)
                UserDictionary.Remove(us.Key);
        }

        public static void Delete(string userName)
        {
            UserDictionary.Remove(userName);
        }
    }
}
