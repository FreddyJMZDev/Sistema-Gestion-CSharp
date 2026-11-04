
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using SistemaGestion.Modelos;

namespace SistemaGestion.Servicios
{
    public class RegistroService
    {
        private readonly List<Registro> registros;
        private readonly string rutaArchivo;
        private int siguienteId = 1;

        public RegistroService()
        {
            string carpeta = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Datos"
            );

            Directory.CreateDirectory(carpeta);

            rutaArchivo = Path.Combine(
                carpeta,
                "registros.json"
            );

            registros = Cargar();

            if (registros.Count > 0)
                siguienteId = registros.Max(r => r.Id) + 1;
        }

        // AGREGAR
        public Registro Agregar(string nombre, string correo)
        {
            Validar(nombre, correo);

            Registro nuevo = new Registro(
                siguienteId,
                nombre.Trim(),
                correo.Trim()
            );

            registros.Add(nuevo);
            siguienteId++;

            Guardar();
            return nuevo;
        }

        // LISTAR
        public List<Registro> ObtenerTodos()
        {
            return new List<Registro>(registros);
        }

        // BUSCAR
        public List<Registro> Buscar(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return ObtenerTodos();

            return registros.Where(r =>
                r.Nombre.IndexOf(
                    nombre,
                    StringComparison.OrdinalIgnoreCase
                ) >= 0
            ).ToList();
        }

        // EDITAR
        public bool Editar(int id, string nombre, string correo)
        {
            Validar(nombre, correo);

            Registro registro = registros
                .FirstOrDefault(r => r.Id == id);

            if (registro == null)
                return false;

            registro.Nombre = nombre.Trim();
            registro.Correo = correo.Trim();

            Guardar();
            return true;
        }

        // ELIMINAR
        public bool Eliminar(int id)
        {
            Registro registro = registros
                .FirstOrDefault(r => r.Id == id);

            if (registro == null)
                return false;

            registros.Remove(registro);
            Guardar();

            return true;
        }

        // VALIDACIONES
        private void Validar(string nombre, string correo)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException(
                    "El nombre es obligatorio."
                );

            if (string.IsNullOrWhiteSpace(correo))
                throw new ArgumentException(
                    "El correo es obligatorio."
                );
        }

        // GUARDAR EN JSON
        private void Guardar()
        {
            var serializador =
                new DataContractJsonSerializer(
                    typeof(List<Registro>)
                );

            using (FileStream archivo = new FileStream(
                rutaArchivo,
                FileMode.Create,
                FileAccess.Write))
            {
                serializador.WriteObject(archivo, registros);
            }
        }

        // CARGAR DESDE JSON
        private List<Registro> Cargar()
        {
            if (!File.Exists(rutaArchivo))
                return new List<Registro>();

            var serializador =
                new DataContractJsonSerializer(
                    typeof(List<Registro>)
                );

            using (FileStream archivo = File.OpenRead(rutaArchivo))
            {
                if (archivo.Length == 0)
                    throw new InvalidDataException(
                        "El archivo JSON está vacío."
                    );

                return (List<Registro>)
                    serializador.ReadObject(archivo);
            }
        }
    }
}
