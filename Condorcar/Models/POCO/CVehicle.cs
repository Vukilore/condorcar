using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Condorcar.Models.POCO
{
    [Table("T_CVehicle")]
    public class CVehicle
    {
        public int Id { get; set; }
        public int Seat { get; set; }       // Places maximum 
        public string Model { get; set; }   // Modèle de la voiture
    }
}