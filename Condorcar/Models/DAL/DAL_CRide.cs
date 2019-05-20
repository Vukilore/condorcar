using Condorcar.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Condorcar.Models.DAL
{
    public class DAL_CRide
    {
        private BddContext bddRide;

        public DAL_CRide()
        {
            bddRide = new BddContext();
        }
        
        public CRide Get(int id)
        {
            return bddRide.T_CRide.Where(p => p.Id == id).SingleOrDefault();
        }
        
        public CRide GetDriverRide(CDriver driver)
        {
            return bddRide.T_CRide.Where(p => p.Driver == driver).SingleOrDefault();
        }

        public List<CRide> GetAll()
        {
            return bddRide.T_CRide.ToList();
        }
        
        public void Add(CRide ride)
        {
            /*var t = bddRide.T_CRide.Where(p => p.Pseudo == user.Pseudo).SingleOrDefault();
            if (t != null)
                throw new Exception();*/

            try
            {
                bddRide.T_CRide.Add(ride);
            }
            catch(Exception e)
            {
                var m = e.Message;
            }

            try
            {
                bddRide.SaveChanges();
            }
            catch(Exception e)
            {
                var m = e.Message;
            }
}

        public void Dispose()
        {
            bddRide.Dispose();
        }
    }
}