
using System;
using System.Collections.Generic;
using System.Linq;
using SistemaGestion.Modelos;

namespace SistemaGestion.Servicios
{
    public class RegistroService
    {
        private readonly List<Registro> registros =
            new List<Registro>();

        private int siguienteId = 1;

        // Agregar un registro
        public Registro Agregar(string nombre, string correo)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(correo))
                throw new ArgumentException("El correo es obligatorio.");

            Registro nuevo = new Registro(
                siguienteId++,
                nombre.Trim(),
                correo.Trim()
            );

            registros.Add(nuevo);
            return nuevo;
        }

        // Obtener todos los registros
        public List<Registro> ObtenerTodos()
        {
            return new List<Registro>(registros);
        }

        // Buscar registros por nombre
        public List<Registro> Buscar(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return ObtenerTodos();

            return registros
                .Where(r => r.Nombre.IndexOf(
                    nombre,
                    StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }

        // Editar un registro
        public bool Editar(int id, string nombre, string correo)
        {
            if (string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(correo))
                throw new ArgumentException(
                    "El nombre y el correo son obligatorios.");

            Registro registro = registros
                .FirstOrDefault(r => r.Id == id);

            if (registro == null)
                return false;

            registro.Nombre = nombre.Trim();
            registro.Correo = correo.Trim();

            return true;
        }

        // Eliminar un registro
        public bool Eliminar(int id)
        {
            Registro registro = registros
                .FirstOrDefault(r => r.Id == id);

            if (registro == null)
                return false;

            registros.Remove(registro);
            return true;
        }
    }
}

