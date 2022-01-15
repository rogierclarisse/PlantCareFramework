using System.ComponentModel.DataAnnotations;

namespace PlantCareFramework.Models
{
    public class Place
    {
        public char Id { get; set; }

        [Required]
        public string Location { get; set; }
        
    }
}
