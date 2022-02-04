using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlantCareFramework.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace PlantCareFramework.ViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }

        [Display (Name = "Role")]
        public List<string> RoleNames { get; set; }
    }
}
