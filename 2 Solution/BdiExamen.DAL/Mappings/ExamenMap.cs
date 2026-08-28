using System.Data.Entity.ModelConfiguration;
using BdiExamen.Model.Entities;

namespace BdiExamen.DAL.Mappings
{
    public class ExamenMap : EntityTypeConfiguration<Examen>
    {
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