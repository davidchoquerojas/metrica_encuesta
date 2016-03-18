using System.Data.Entity.ModelConfiguration;

namespace Metricaencuesta.Models.Mapping
{
    public class documentoMap:EntityTypeConfiguration<documento>
    {
        public documentoMap()
        {
            HasKey(t => t.id_documento);
            Property(t => t.tipo).IsFixedLength().HasMaxLength(2);
            Property(t => t.nombre).IsRequired().HasMaxLength(100);
            Property(t => t.ruta).IsRequired().HasMaxLength(150);
            Property(t => t.estado).IsFixedLength().HasMaxLength(1);

            ToTable("documentos");
            Property(t => t.id_documento).HasColumnName("id_documento");
            Property(t => t.nombre).HasColumnName("nombre");
            Property(t => t.tipo).HasColumnName("tipo");
            Property(t => t.ruta).HasColumnName("ruta");
            Property(t => t.estado).HasColumnName("estado");
            Property(t => t.fecha_registro).HasColumnName("fecha_registro");
            Property(t => t.usuario_registro).HasColumnName("usuario_registro");
        }
    }
}