using System.Collections.Generic;

namespace BdiExamen.ApiExamen.Validation
{
    // Valida datos de entrada. No lanza excepciones de UI ni muestra nada;
    // solo informa si es válido y, si no, por qué (el Front decide cómo mostrarlo).
    public class ExamenValidator
    {
        // Valida el nombre y la descripción de un examen.
        // Devuelve true si ambos son válidos; de lo contrario, devuelve false y llena la lista de errores con mensajes descriptivos.
        public bool EsValido(string nombre, string descripcion, out List<string> errores)
        {
            errores = new List<string>();

            if (string.IsNullOrWhiteSpace(nombre))
                errores.Add("El nombre es obligatorio.");
            else if (nombre.Length > 100)
                errores.Add("El nombre no puede superar los 100 caracteres.");

            if (!string.IsNullOrEmpty(descripcion) && descripcion.Length > 500)
                errores.Add("La descripción no puede superar los 500 caracteres.");

            return errores.Count == 0;
        }
        // Valida que el id sea mayor a cero.
        // Devuelve true si es válido; de lo contrario, devuelve false y llena el mensaje de error con una descripción.
        public bool IdValido(int id, out string error)
        {
            error = null;

            if (id <= 0)
            {
                error = "El Id debe ser mayor a cero.";
                return false;
            }

            return true;
        }
    }
}