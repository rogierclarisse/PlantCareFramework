using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlantCareFramework.Areas.Identity.Data;

namespace PlantCareFramework.ViewModels
{
    public class UserRoleViewModel
    {
        public SelectList userRoles { get; set; }
       
        public string userId { get; set; }

       
        //public string userId { get; set; }
    }
}
