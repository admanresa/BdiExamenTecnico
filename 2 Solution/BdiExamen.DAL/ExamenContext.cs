using System.Data.Entity;
using BdiExamen.DAL.Mappings;
using BdiExamen.Model.Entities;

namespace BdiExamen.DAL
{
    // Contexto de base de datos para la entidad Examen.
    // Hereda de DbContext para interactuar con la base de datos.
    // Se configura para no generar ni alterar la tabla automáticamente, ya que se asume que la tabla ya existe por un script. Estableciendo el inicializador de base de datos en null para evitar la creación o alteración automática de la tabla.
    // Contiene un DbSet<Examen> que representa la colección de entidades Examen en la base de datos.
    // El método OnModelCreating se utiliza para configurar la entidad Examen mediante la clase ExamenMap.
    // Esta clase es parte del patrón de diseño Repository y Unit of Work, facilitando la gestión de datos en la aplicación.
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
            // Configuración de la entidad Examen
            modelBuilder.Configurations.Add(new ExamenMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}