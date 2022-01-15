using Microsoft.EntityFrameworkCore;
using PlantCareFramework.Models;

namespace PlantCareFramework.Data
{
    public class Seeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PlantCareContext(serviceProvider.GetRequiredService<DbContextOptions<PlantCareContext>>()))
            {
                context.Database.EnsureCreated();

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
                        new Plant { Name = "Monstera", WaterQuantity = 200, LightId = 'M', PlaceId = 'I'},
                        new Plant { Name = "Sanseveria", WaterQuantity = 10, LightId = 'N', PlaceId = 'I' },
                        new Plant { Name = "Pannekoek Plant", WaterQuantity = 50, LightId = 'M', PlaceId = 'I' },
                        new Plant { Name = "Olijfboom", WaterQuantity = 300, LightId = 'F', PlaceId = 'O' }
                        );
                    context.SaveChanges();
                }

            }
        }
    }
}
