using Microsoft.EntityFrameworkCore;
using PlantCareFramework.Models;
using Microsoft.AspNetCore.Identity;
using PlantCareFramework.Areas.Identity.Data;

namespace PlantCareFramework.Data
{
    public class Seeder
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Database.EnsureCreated();

                if (!context.Language.Any())
                {
                    context.Language.AddRange(
                        new Language() { Id = "-", Name = "-", Cultures = "-", IsSystemLanguage = false },
                        new Language() { Id = "de", Name = "Deutsch", Cultures = "DE", IsSystemLanguage = false },
                        new Language() { Id = "en", Name = "English", Cultures = "UK;US", IsSystemLanguage = true },
                        new Language() { Id = "es", Name = "Español", Cultures = "ES", IsSystemLanguage = false },
                        new Language() { Id = "fr", Name = "français", Cultures = "BE;FR", IsSystemLanguage = true },
                        new Language() { Id = "nl", Name = "Nederlands", Cultures = "BE;NL", IsSystemLanguage = true }
                    );
                    context.SaveChanges();
                }


                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
                       new IdentityRole { Id = "User", Name = "User", NormalizedName = "USER"},
                       new IdentityRole { Id = "Admin", Name = "Admin", NormalizedName = "ADMIN" }
                        );
                   
                }
                context.SaveChanges();


                ApplicationUser user = null;
                ApplicationUser admin = null;

                if (!context.Users.Any())
                {
                    //user = new ApplicationUser
                    //{
                    //    FirstName = "User",
                    //    LastName = "User",
                    //    UserName = "User",
                    //    Email = "user@user.be",
                    //    EmailConfirmed = true
                    //};
                    // userManager.CreateAsync(user, "User1234!");

                    admin = new ApplicationUser
                    {
                        FirstName = "Admin",
                        LastName = "Admin",
                        UserName = "Admin",
                        Email = "admin@admin.be",
                        EmailConfirmed = true,
                        LanguageId = "en"
                    };
                     userManager.CreateAsync(admin, "Admin1234!");
                }


                Thread.Sleep(2000);
              


                if (!context.Place.Any())
                {
                    context.Place.AddRange(
                        new Place { Id = 'I', Location = "Inside" },
                        new Place { Id = 'O', Location = "Outside" }
                        );
                }
                context.SaveChanges();

                if (!context.Light.Any())
                {
                    context.Light.AddRange(
                        new Light { Id = 'N', LightIntensity = "None" },
                        new Light { Id = 'M', LightIntensity = "Medium" },
                        new Light { Id = 'F', LightIntensity = "Full" }
                        );
                }
                context.SaveChanges();

                if (!context.Plant.Any())
                {
                    context.Plant.AddRange(
                        new Plant { Name = "Monstera", WaterQuantity = 200, LightId = 'M', PlaceId = 'I', AppUserId = admin.Id},
                        new Plant { Name = "Sanseveria", WaterQuantity = 10, LightId = 'N', PlaceId = 'I', AppUserId = admin.Id },
                        new Plant { Name = "Pannekoek Plant", WaterQuantity = 50, LightId = 'M', PlaceId = 'I', AppUserId = admin.Id },
                        new Plant { Name = "Olijfboom", WaterQuantity = 300, LightId = 'F', PlaceId = 'O', AppUserId = admin.Id }
                        );
                    context.SaveChanges();
                }

                if (context.Users.Any())
                {


                    if (!context.UserRoles.Any())
                    {
                        context.UserRoles.AddRange(
                           // new IdentityUserRole<string> { RoleId = "User", UserId = user.Id },
                            new IdentityUserRole<string> { RoleId = "Admin", UserId = admin.Id }
                            );
                    }
                    context.SaveChanges();
                }

                List<string> supportedLanguages = new List<string>();
                Language.AllLanguages = context.Language.ToList();
                Language.LanguageDictionary = new Dictionary<string, Language>();
                Language.SystemLanguages = new List<Language>();

                supportedLanguages.Add("nl-BE");
                foreach (Language l in Language.AllLanguages)
                {
                    if (l.Id != "-")
                    {
                        Language.LanguageDictionary[l.Id] = l;
                        if (l.IsSystemLanguage)
                            Language.SystemLanguages.Add(l);
                        supportedLanguages.Add(l.Id);
                        string[] even = l.Cultures.Split(";");
                        foreach (string e in even)
                        {
                            supportedLanguages.Add(l.Id + "-" + e);
                        }
                    }
                }
                Language.SupportedLanguages = supportedLanguages.ToArray();


            }
        }
    }
}
