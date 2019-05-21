using Condorcar.Models.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Condorcar.Models.POCO
{
    //[Table("T_CPassenger")]
    public class CPassenger : CUser
    {
        [Display(Name = "Fumeur ?")]
        public bool Smoker { get; set; }

        public List<CRide> RideList { get; set; }

        public CPassenger() { RideList = new List<CRide>(); }

        public void AddToRide(CRide ride)
        {
            RideList.Add(ride);
            DAL_CUser user = new DAL_CUser();
            user.Save(this);
            //ride.AddPassenger(this);
        }

    }
}