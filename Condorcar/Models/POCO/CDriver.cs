using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Condorcar.Models.POCO
{
    //[Table("T_CDriver")]
    public class CDriver : CUser
    {
        public short GlobalNote { get; set; }        // Note globale (moyenne) attribué par les passager sur 5
        public virtual List<CVehicle> Vehicles { get; set; } // Liste des véhicules du conducteur

        // Add vehice, Delete Vehicle, Edit ?

        public CDriver() // Constructeur
        {
            GlobalNote = 0;
        }

        public void AddVehicle(CVehicle vehicle)
        {
            Vehicles.Add(vehicle);
            vehicle.Register();
        }
    }
}