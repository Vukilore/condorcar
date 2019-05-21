using Condorcar.Models.POCO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Condorcar.Models.DAL
{
    public class DAL_CRide
    {
        private BddContext bdd;

        public DAL_CRide()
        {
            bdd = BddContext.GetInstance();
        }
        
        public CRide Get(int id)
        {
            return bdd.T_CRide.Where(p => p.Id == id).SingleOrDefault();
        }
        
        public CRide GetDriverRide(CDriver driver)
        {
            return bdd.T_CRide.Where(p => p.Driver == driver).SingleOrDefault();
        }

        public List<CRide> GetAll()
        {
             return bdd.T_CRide.ToList();
        }

        public List<CRide> GetAll(CDriver driver)
        {
            return bdd.T_CRide.Where(p => p.Driver.Id == driver.Id).ToList();
        }

        public void SaveRide(CRide ride)
        {
            bdd.Entry(ride).State = EntityState.Modified;
            bdd.SaveChanges();
        }

        public void Add(CRide ride)
        {
            bdd.T_CRide.Add(ride);
            bdd.SaveChanges();
        }

        public void Dispose()
        {
            bdd.Dispose();
        }
    }
}