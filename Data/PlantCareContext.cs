#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlantCareFramework.Models;

namespace PlantCareFramework.Data
{
    public class PlantCareContext : DbContext
    {
        public PlantCareContext (DbContextOptions<PlantCareContext> options)
            : base(options)
        {
        }

        public DbSet<PlantCareFramework.Models.Place> Place { get; set; }

        public DbSet<PlantCareFramework.Models.Light> Light { get; set; }

        public DbSet<PlantCareFramework.Models.Plant> Plant { get; set; }
    }
}
