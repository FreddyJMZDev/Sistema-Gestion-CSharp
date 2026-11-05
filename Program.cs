using System;
using System.Text;
using SistemaGestion.Modelos;
using SistemaGestion.Servicios;

namespace SistemaGestion
{
    internal class Program
    {
        private static readonly RegistroService Servicio = new RegistroService();

        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool activo = true;
            while (activo)
            {
                Console.WriteLine("\n=== SISTEMA DE GESTIÓN ===");
                Console.WriteLine("1. Agregar registro");
                Console.WriteLine("2. Mostrar registros");
                Console.WriteLine("3. Buscar por nombre");
                Console.WriteLine("4. Editar registro");
                Console.WriteLine("5. Eliminar registro");
                Console.WriteLine("0. Salir");
                Console.Write("Opción: ");
                string opcion = Console.ReadLine();
                try
                {
                    switch (opcion)
                    {
                        case "1": Agregar(); break;
                        case "2": Mostrar(); break;
                        case "3": Buscar(); break;
                        case "4": Editar(); break;
                        case "5": Eliminar(); break;
                        case "0": activo = false; break;
                        default: Console.WriteLine("Opción no válida."); break;
                    }
                }
                catch (Exception ex) when (ex is ArgumentException || ex is System.IO.IOException || ex is System.Runtime.Serialization.SerializationException || ex is UnauthorizedAccessException)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private static void Agregar()
        {
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Correo: ");
            string correo = Console.ReadLine();
            Registro creado = Servicio.Agregar(nombre, correo);
            Console.WriteLine("Registro creado. ID: " + creado.Id);
        }

        private static void Mostrar()
        {
            var registros = Servicio.ObtenerTodos();
            if (registros.Count == 0) Console.WriteLine("No hay registros.");
            foreach (Registro registro in registros) Imprimir(registro);
        }

        private static void Buscar()
        {
            Console.Write("Nombre a buscar: ");
            var resultados = Servicio.Buscar(Console.ReadLine());
            if (resultados.Count == 0) Console.WriteLine("Sin resultados.");
            foreach (Registro registro in resultados) Imprimir(registro);
        }

        private static void Editar()
        {
            int id = LeerId();
            if (id < 0) return;
            Console.Write("Nuevo nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Nuevo correo: ");
            string correo = Console.ReadLine();
            Console.WriteLine(Servicio.Editar(id, nombre, correo)
                ? "Registro actualizado." : "No se encontró ese ID.");
        }

        private static void Eliminar()
        {
            int id = LeerId();
            if (id < 0) return;
            Console.Write("¿Confirmas eliminación? (S/N): ");
            if (!string.Equals(Console.ReadLine(), "S", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Cancelado.");
                return;
            }
            Console.WriteLine(Servicio.Eliminar(id)
                ? "Registro eliminado." : "No se encontró ese ID.");
        }

        private static int LeerId()
        {
            Console.Write("ID: ");
            int id;
            if (int.TryParse(Console.ReadLine(), out id) && id > 0) return id;
            Console.WriteLine("ID no válido.");
            return -1;
        }

        private static void Imprimir(Registro r)
        {
            Console.WriteLine("[{0}] {1} | {2}", r.Id, r.Nombre, r.Correo);
        }
    }
}
