using System;
using System.Collections.Generic;

namespace Metricaencuesta.Models
{
    public partial class gpregunta
    {

        public int id_gpregunta { get; set; }
        public string descripcion { get; set; }
        public string estado { get; set; }
        public Nullable<System.DateTime> fec_reg { get; set; }
        public Nullable<System.DateTime> fec_mod { get; set; }
        public string usu_reg { get; set; }
        public string usu_mod { get; set; }
    }
}
