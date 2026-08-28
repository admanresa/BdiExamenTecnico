using System.Data.Entity;
using BdiExamen.DAL.Mappings;
using BdiExamen.Model.Entities;

namespace BdiExamen.DAL
{
    public class ExamenContext : DbContext
    {
        public ExamenContext() : base("name=ExamenContext")
        {
            // Nunca generar/alterar la tabla automáticamente: ya existe por script.
            Database.SetInitializer<ExamenContext>(null);
        }

        public DbSet<Examen> Examenes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ExamenMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}