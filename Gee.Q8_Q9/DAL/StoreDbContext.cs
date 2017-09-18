using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gee.Q8_Q9.Models;

namespace Gee.Q8_Q9.DAL
{
    public class StoreDbContext : DbContext
    {
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<WarehouseProduct> WarehouseProducts { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        public StoreDbContext():base("name=GGG")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //
            // It's assumed that the warehouse entity is dependent entity on country
            //
            modelBuilder.Entity<Country>().HasOptional(x => x.Warehouse).WithRequired(x => x.Country);
        }
    }
}
