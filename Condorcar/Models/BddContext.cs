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
        private static BddContext instance = null;
        
        private BddContext()
        {
            Database.SetInitializer(new TTT());
        }

        public static BddContext GetInstance()
        {
            if (instance == null)
                instance = new BddContext();
            return instance;
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
            List<CVehicle> vehs = new List<CVehicle>();
            vehs.Add(new CVehicle { Model = "Seat Ibiza", Seat = 4, CanSmoke = true });
            vehs.Add(new CVehicle { Model = "Opel Astra", Seat = 3, CanSmoke = false });
            try
            {
                context.T_CUser.Add(new CDriver
                {
                    Pseudo = "Vukilore",
                    Password = "6F-0A-CA-9E-92-C0-04-C2-74-CC-BB-EC-DD-69-3C-10",
                    ConfirmPassword = "6F-0A-CA-9E-92-C0-04-C2-74-CC-BB-EC-DD-69-3C-10",
                    Address = "Rue des pigeons",
                    Email = "vukilore@0rg.fr",
                    Vehicles = vehs
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine(e.Message);
            }
        }
    }
}