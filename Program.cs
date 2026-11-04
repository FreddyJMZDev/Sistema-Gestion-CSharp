
using System;
using System.Text;
using SistemaGestion.Modelos;
using SistemaGestion.Servicios;

namespace SistemaGestion
{
    class Program
    {
        static RegistroService servicio = new RegistroService();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            bool ejecutando = true;

            while (ejecutando)
            {
                Console.Clear();

                Console.WriteLine("=================================");
                Console.WriteLine("    SISTEMA DE GESTIÓN C#");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Agregar registro");
                Console.WriteLine("2. Mostrar registros");
                Console.WriteLine("3. Buscar registro");
                Console.WriteLine("4. Editar registro");
                Console.WriteLine("5. Eliminar registro");
                Console.WriteLine("0. Salir");
                Console.WriteLine("=================================");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                Console.Clear();

                switch (opcion)
                {
                    case "1":
                        AgregarRegistro();
                        break;

                    case "2":
                        MostrarRegistros();
                        break;

                    case "3":
                        BuscarRegistro();
                        break;

                    case "4":
                        EditarRegistro();
                        break;

                    case "5":
                        EliminarRegistro();
                        break;

                    case "0":
                        ejecutando = false;
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                if (ejecutando)
                {
                    Console.WriteLine("\nPresione una tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void AgregarRegistro()
        {
            Console.WriteLine("=== AGREGAR REGISTRO ===");

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Correo: ");
            string correo = Console.ReadLine();

            try
            {
                Registro nuevo = servicio.Agregar(nombre, correo);
                Console.WriteLine(
                    "\nRegistro agregado correctamente. ID: " + nuevo.Id);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("\nError: " + ex.Message);
            }
        }

        static void MostrarRegistros()
        {
            Console.WriteLine("=== LISTA DE REGISTROS ===");

            var registros = servicio.ObtenerTodos();

            if (registros.Count == 0)
            {
                Console.WriteLine("No hay registros disponibles.");
                return;
            }

            foreach (Registro registro in registros)
            {
                MostrarRegistro(registro);
            }
        }

        static void BuscarRegistro()
        {
            Console.WriteLine("=== BUSCAR REGISTRO ===");

            Console.Write("Nombre a buscar: ");
            string nombre = Console.ReadLine();

            var resultados = servicio.Buscar(nombre);

            if (resultados.Count == 0)
            {
                Console.WriteLine("No se encontraron registros.");
                return;
            }

            foreach (Registro registro in resultados)
            {
                MostrarRegistro(registro);
            }
        }

        static void EditarRegistro()
        {
            Console.WriteLine("=== EDITAR REGISTRO ===");

            Console.Write("ID del registro: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID no válido.");
                return;
            }

            Console.Write("Nuevo nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Nuevo correo: ");
            string correo = Console.ReadLine();

            try
            {
                bool editado = servicio.Editar(id, nombre, correo);

                Console.WriteLine(editado
                    ? "Registro actualizado correctamente."
                    : "No existe un registro con ese ID.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void EliminarRegistro()
        {
            Console.WriteLine("=== ELIMINAR REGISTRO ===");

            Console.Write("ID del registro: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID no válido.");
                return;
            }

            Console.Write("¿Está seguro? (S/N): ");
            string confirmacion = Console.ReadLine();

            if (!string.Equals(
                confirmacion,
                "S",
                StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Operación cancelada.");
                return;
            }

            bool eliminado = servicio.Eliminar(id);

            Console.WriteLine(eliminado
                ? "Registro eliminado correctamente."
                : "No existe un registro con ese ID.");
        }

        static void MostrarRegistro(Registro registro)
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine("ID: " + registro.Id);
            Console.WriteLine("Nombre: " + registro.Nombre);
            Console.WriteLine("Correo: " + registro.Correo);
        }
    }
}

