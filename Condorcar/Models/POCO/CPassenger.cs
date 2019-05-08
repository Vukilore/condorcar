using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Condorcar.Models.POCO
{
    //[Table("T_CPassenger")]
    public class CPassenger : CUser
    {
        public bool Smoker { get; set; }

    }
}