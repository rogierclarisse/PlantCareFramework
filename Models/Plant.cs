using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantCareFramework.Models
{
    public class Plant
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Plant Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Weekly Water Quantity")]
        public int WaterQuantity { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Placing")]
        [ForeignKey("Place")]
        public char PlaceId { get; set; }
        public Place? Place { get; set; }

        [Required]
        [Display(Name = "Light Intensity")]
        [ForeignKey("Light")]
        public char LightId { get; set; }
        public Light? Light { get; set; }   
        
    }
}
