

namespace Metricaencuesta.Models
{
    public partial class documento
    {
        public int id_documento { get; set; }
        public string tipo { get; set; }
        public string nombre { get; set; }
        public string ruta { get; set; }
        public string estado { get; set; }
        public System.DateTime fecha_registro { get; set; }
        public string usuario_registro { get; set; }
        public string leido_por { get; set; }
    }
}