using System;
using System.Data.Entity;
using System.Linq;
using BdiExamen.Model.Dtos;
using BdiExamen.Model.Entities;

namespace BdiExamen.DAL
{
    public class ExamenRepository : IExamenRepository
    {
        private const string NoExisteMensaje = "No se encontró un registro con el Id indicado.";

        public AgregarResult Agregar(string nombre, string descripcion)
        {
            using (var context = new ExamenContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var entidad = new Examen { Nombre = nombre, Descripcion = descripcion };
                    context.Examenes.Add(entidad);
                    context.SaveChanges();

                    transaction.Commit();
                    return AgregarResult.Ok(entidad.Id, "Registro creado correctamente.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return AgregarResult.Fallido(-1, ex.Message);
                }
            }
        }

        public OperationResult Actualizar(int id, string nombre, string descripcion)
        {
            using (var context = new ExamenContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var entidad = context.Examenes.FirstOrDefault(e => e.Id == id);
                    if (entidad == null)
                    {
                        transaction.Rollback();
                        return OperationResult.Error(1, NoExisteMensaje);
                    }

                    entidad.Nombre = nombre;
                    entidad.Descripcion = descripcion;
                    context.SaveChanges();

                    transaction.Commit();
                    return OperationResult.Ok("Registro actualizado correctamente.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return OperationResult.Error(-1, ex.Message);
                }
            }
        }

        public OperationResult Eliminar(int id)
        {
            using (var context = new ExamenContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var entidad = context.Examenes.FirstOrDefault(e => e.Id == id);
                    if (entidad == null)
                    {
                        transaction.Rollback();
                        return OperationResult.Error(1, NoExisteMensaje);
                    }

                    context.Examenes.Remove(entidad);
                    context.SaveChanges();

                    transaction.Commit();
                    return OperationResult.Ok("Registro eliminado correctamente.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return OperationResult.Error(-1, ex.Message);
                }
            }
        }

        public ConsultaResult Consultar(int? id, string nombre, string descripcion)
        {
            using (var context = new ExamenContext())
            {
                try
                {
                    var query = context.Examenes.AsQueryable();

                    if (id.HasValue)
                        query = query.Where(e => e.Id == id.Value);

                    if (!string.IsNullOrEmpty(nombre))
                        query = query.Where(e => e.Nombre.Contains(nombre));

                    if (!string.IsNullOrEmpty(descripcion))
                        query = query.Where(e => e.Descripcion.Contains(descripcion));

                    var resultados = query.OrderBy(e => e.Id).ToList();
                    return ConsultaResult.Ok(resultados);
                }
                catch (Exception ex)
                {
                    return ConsultaResult.Error(ex.Message);
                }
            }
        }
    }
}