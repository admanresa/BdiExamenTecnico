using System.Data.Entity.ModelConfiguration;
using BdiExamen.Model.Entities;

namespace BdiExamen.DAL.Mappings
{
    // Clase de mapeo para la entidad Examen, que define cómo se mapea a la base de datos.
    public class ExamenMap : EntityTypeConfiguration<Examen>
    {
        // Constructor que configura el mapeo de la entidad Examen a la tabla tblExamen en la base de datos.
        // Define la clave primaria, las propiedades y sus restricciones.
        public ExamenMap()
        {
            ToTable("tblExamen");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsOptional();
        }
    }
}