using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Metricaencuesta.Models.helper
{
    public class queryHelper
    {
        [Key]
        public int id_query { get; set; }

        [Display(Name = "Query")]
        [Required(ErrorMessage = "Campo requerido")]
        [DataType(DataType.MultilineText)]
        public string strQuery { get; set; }

        public DataTable data { get; set; } = new DataTable();
    }
}