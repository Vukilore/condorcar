using Condorcar.Models.POCO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Condorcar.Models.DAL
{
    public class DAL_CUser
    {
        private BddContext bdd;

        public DAL_CUser()
        {
            bdd = new BddContext();
        }
        
        public CUser Get(int id)
        {
            return bdd.T_CUser.Where(p => p.Id == id).SingleOrDefault();
        }
        
        public CUser Get(string pseudo)
        {
            return bdd.T_CUser.Where(p => p.Pseudo == pseudo).SingleOrDefault();
        }

        public CDriver GetDriver(string pseudo)
        {
            var d = bdd.T_CUser.Where(p => p.Pseudo == pseudo).OfType<CDriver>().SingleOrDefault();
            return d;
        }

        /* public List<CVehicle> GetUserVehicleByName(string pseudo)
         {
             return bdd.T_CUser.Where(p => p.Pseudo == pseudo).Select(p => p.Vehicles).ToList();
         }*/

        public List<CUser> GetAll()
        {
            return bdd.T_CUser.ToList();
        }
        
        public List<CDriver> GetAllDriver()
        {
            return bdd.T_CUser.OfType<CDriver>().ToList();
        }
        
        public List<CPassenger> GetAllPassenger()
        {
            return bdd.T_CUser.OfType<CPassenger>().ToList();
        }

        public void Add(CUser user)
        {
            var t = bdd.T_CUser.Where(p => p.Pseudo == user.Pseudo).SingleOrDefault();
            if (t != null)
                throw new Exception();

            bdd.T_CUser.Add(user);
            bdd.SaveChanges();
        }

       /* public void SaveDriver(CUser user)
        {
            var tmpUser = bdd.T_CUser.Where(p => p.Id == user.Id).SingleOrDefault();
            if (tmpUser == null)
                throw new Exception();

            bdd.Entry(tmpUser).State = user == null ? EntityState.Added : EntityState.Modified;
            try { bdd.SaveChanges(); }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
        }*/

        public void SaveDriver(CDriver user) 
        {
            DbEntityEntry<CDriver> entry = bdd.Entry(user);
            entry.Property(e => e.Vehicles).IsModified = true;
            try { bdd.SaveChanges(); }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        

        public void Dispose()
        {
            bdd.Dispose();
        }
    }
}