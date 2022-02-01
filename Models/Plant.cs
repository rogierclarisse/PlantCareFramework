using Microsoft.AspNetCore.Mvc.Rendering;
using PlantCareFramework.Areas.Identity.Data;
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

        [Display(Name = "Necessary Watering (ml)")]
        public int WaterNeeded { get; set; } = 0;

        [Display(Name = "Last Watering (date)")]
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

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public ApplicationUser? AppUser { get; set; }

        public DateTime? Deleted { get; set; } = DateTime.MaxValue;

        
    }

    //public class PlantTaskViewModel
    //{
    //    public string Name { get; set; }
    //    public int Waterquantity { get; set; }

       
    //}
    //public class PlantIndexViewModel
    //{
    //    public int SelectedItem { get; set; }
    //    public string SearchField { get; set; }
    //    public List<Plant> Plants { get; set; }
    //    public SelectList PlaceToSelect { get; set; }

    //}
}
