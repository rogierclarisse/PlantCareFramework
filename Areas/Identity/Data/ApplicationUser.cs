using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PlantCareFramework.Models;

namespace PlantCareFramework.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    internal object Roles;

    public string FirstName { get; set; }
    public string LastName { get; set; }

    [ForeignKey("Language")]
    public string? LanguageId { get; set; }
    public Language? Language { get; set; }

    public bool IsActive { get; set; } = true;

}

