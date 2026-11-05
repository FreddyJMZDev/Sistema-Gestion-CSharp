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
            string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Datos");
            Directory.CreateDirectory(carpeta);
            rutaArchivo = Path.Combine(carpeta, "registros.json");
            registros = Cargar();
            if (registros.Count > 0) siguienteId = registros.Max(r => r.Id) + 1;
        }

        public Registro Agregar(string nombre, string correo)
        {
            ValidadorRegistro.Validar(nombre, correo);
            Registro nuevo = new Registro(siguienteId, nombre.Trim(), correo.Trim());
            registros.Add(nuevo);
            try { Guardar(); }
            catch { registros.Remove(nuevo); throw; }
            siguienteId++;
            return nuevo;
        }

        public List<Registro> ObtenerTodos()
        {
            return registros.Select(r => new Registro(r.Id, r.Nombre, r.Correo)).ToList();
        }

        public List<Registro> Buscar(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return ObtenerTodos();
            return ObtenerTodos().Where(r =>
                r.Nombre.IndexOf(nombre, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        public bool Editar(int id, string nombre, string correo)
        {
            Registro registro = registros.FirstOrDefault(r => r.Id == id);
            if (registro == null) return false;
            ValidadorRegistro.Validar(nombre, correo);
            string originalNombre = registro.Nombre;
            string originalCorreo = registro.Correo;
            registro.Nombre = nombre.Trim();
            registro.Correo = correo.Trim();
            try { Guardar(); }
            catch { registro.Nombre = originalNombre; registro.Correo = originalCorreo; throw; }
            return true;
        }

        public bool Eliminar(int id)
        {
            int indice = registros.FindIndex(r => r.Id == id);
            if (indice < 0) return false;
            Registro eliminado = registros[indice];
            registros.RemoveAt(indice);
            try { Guardar(); }
            catch { registros.Insert(indice, eliminado); throw; }
            return true;
        }

        private void Guardar()
        {
            var serializador = new DataContractJsonSerializer(typeof(List<Registro>));
            using (FileStream archivo = new FileStream(rutaArchivo, FileMode.Create, FileAccess.Write))
                serializador.WriteObject(archivo, registros);
        }

        private List<Registro> Cargar()
        {
            if (!File.Exists(rutaArchivo)) return new List<Registro>();
            var serializador = new DataContractJsonSerializer(typeof(List<Registro>));
            using (FileStream archivo = File.OpenRead(rutaArchivo))
            {
                if (archivo.Length == 0) throw new InvalidDataException("El archivo JSON está vacío.");
                return (List<Registro>)serializador.ReadObject(archivo) ?? new List<Registro>();
            }
        }
    }
}
