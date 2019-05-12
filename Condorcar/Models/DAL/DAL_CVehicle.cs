﻿using Condorcar.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Condorcar.Models.DAL
{
    public class DAL_CVehicle
    {
        private BddContext bdd;

        public DAL_CVehicle()
        {
            bdd = new BddContext();
        }

        public CVehicle Get(int id)
        {
            return bdd.T_CVehicle.Where(p => p.Id == id).SingleOrDefault();
        }

        public List<CVehicle> GetAll()
        {
            return bdd.T_CVehicle.ToList();
        }
        
        public void Add(CVehicle vehicle)
        {
            //var t = bdd.T_CVehicle.Where(p => p.Pseudo == user.Pseudo).SingleOrDefault();
            //if (t != null)
            //    throw new Exception();

            bdd.T_CVehicle.Add(vehicle);
            bdd.SaveChanges();
        }

        public void Dispose()
        {
            bdd.Dispose();
        }
    }
}