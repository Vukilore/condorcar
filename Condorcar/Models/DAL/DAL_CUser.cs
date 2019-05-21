﻿using Condorcar.Models.POCO;
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
            bdd = BddContext.GetInstance();
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

        public void SaveDriver(CDriver user)
        {
            bdd.Entry(user).State = EntityState.Modified;
            bdd.SaveChanges();
        }


        public void Dispose()
        {
            bdd.Dispose();
        }
    }
}