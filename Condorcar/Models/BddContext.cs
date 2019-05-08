using Condorcar.Models.POCO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Condorcar.Models
{
    public class BddContext : DbContext
    {
        public DbSet<CUser> T_CUser { get; set; }
       // public DbSet<CDriver> T_CDriver { get; set; }
       // public DbSet<CPassenger> T_CPassenger { get; set; }
        public DbSet<CRide> T_CRide { get; set; }
        public DbSet<CVehicle> T_CVehicle { get; set; }

        public BddContext()
        {
            Database.SetInitializer(new TTT());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CUser>()
                        .Map<CDriver>(m => m.Requires("Type").HasValue("Driver"))
                        .Map<CPassenger>(m => m.Requires("Type").HasValue("Passenger"));
        }

        public System.Data.Entity.DbSet<Condorcar.Models.POCO.CDriver> CUsers { get; set; }
    }

    public class TTT : DropCreateDatabaseIfModelChanges<BddContext>
    {
        protected override void Seed(BddContext context)
        {
           // context.T_CUser.Add(new CUser { Pseudo = "Vukilore", Password = "azerty" });//todo remplir les arguments
        }
    }
}