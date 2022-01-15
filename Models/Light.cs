using System.ComponentModel.DataAnnotations;

namespace PlantCareFramework.Models
{
    public class Light
    {
        public char Id { get; set; } 

        [Required]
        public string LightIntensity { get; set; }

    }
}
