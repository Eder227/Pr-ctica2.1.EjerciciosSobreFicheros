using System;
using System.IO;

class Program
{
    static string ficheroActual = null;

    static void Main(string[] args)
    {
        bool salir = false;

        while (!salir)
        {
            MostrarMenu();
            Console.Write("Selecciona una opcion (1-6): ");
            string opcion = Console.ReadLine().Trim();

            switch (opcion)
            {
                case "1":
                    CrearFichero();
                    break;
                case "2":
                    AnadirRegistros();
                    break;
                case "3":
                    EliminarRegistros();
                    break;
                case "4":
                    EliminarFichero();
                    break;
                case "5":
                    VisualizarRegistros();
                    break;
                case "6":
                    Console.WriteLine("Cerrando la aplicacion...");
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opcion no valida. Intenta de nuevo.");
                    break;
            }
        }
    }

    static void MostrarMenu()
    {
        Console.WriteLine("\n--- MENU DE OPCIONES ---");
        Console.WriteLine("Fichero actual: " + (ficheroActual ?? "Ninguno"));
        Console.WriteLine("------------------------");
        Console.WriteLine("1. Crear un nuevo fichero");
        Console.WriteLine("2. Anadir registros en el fichero");
        Console.WriteLine("3. Eliminar todos los registros del fichero");
        Console.WriteLine("4. Eliminar el fichero");
        Console.WriteLine("5. Visualizar los registros");
        Console.WriteLine("6. Cerrar la aplicacion");
    }

    static bool VerificarFichero()
    {
        if (string.IsNullOrEmpty(ficheroActual))
        {
            Console.WriteLine("No hay ningun fichero activo.");
            Console.Write("Introduce el nombre del fichero con el que quieres trabajar: ");
            string nombre = Console.ReadLine().Trim();

            if (File.Exists(nombre))
            {
                ficheroActual = nombre;
                return true;
            }
            else
            {
                Console.WriteLine("El fichero especificado no existe.");
                return false;
            }
        }
        return true;
    }

    static void CrearFichero()
    {
        Console.Write("Nombre del nuevo fichero (ej. datos.txt): ");
        string nombre = Console.ReadLine().Trim();

        if (string.IsNullOrEmpty(nombre))
        {
            Console.WriteLine("El nombre no puede estar vacio.");
            return;
        }

        try
        {
            using (StreamWriter sw = File.AppendText(nombre)) { }
            ficheroActual = nombre;
            Console.WriteLine($"Fichero '{nombre}' creado y seleccionado.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al crear el fichero: " + ex.Message);
        }
    }

    static void AnadirRegistros()
    {
        if (!VerificarFichero()) return;

        Console.WriteLine($"Escribe los registros para '{ficheroActual}'. Escribe 'FIN' para terminar.");
        while (true)
        {
            Console.Write("> ");
            string linea = Console.ReadLine();

            if (linea.Trim().Equals("FIN", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            try
            {
                using (StreamWriter sw = File.AppendText(ficheroActual))
                {
                    sw.WriteLine(linea);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar el registro: " + ex.Message);
            }
        }
    }

    static void EliminarRegistros()
    {
        if (!VerificarFichero()) return;

        Console.Write($"¿Deseas vaciar todo el contenido de '{ficheroActual}'? (s/n): ");
        string confirmacion = Console.ReadLine().Trim().ToLower();

        if (confirmacion == "s")
        {
            try
            {
                File.WriteAllText(ficheroActual, string.Empty);
                Console.WriteLine("Se han borrado todos los registros correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al vaciar el fichero: " + ex.Message);
            }
        }
    }

    static void EliminarFichero()
    {
        if (!VerificarFichero()) return;

        Console.Write($"¿Seguro que quieres borrar el fichero '{ficheroActual}' del disco? (s/n): ");
        string confirmacion = Console.ReadLine().Trim().ToLower();

        if (confirmacion == "s")
        {
            try
            {
                File.Delete(ficheroActual);
                Console.WriteLine($"El fichero '{ficheroActual}' ha sido eliminado.");
                ficheroActual = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el fichero: " + ex.Message);
            }
        }
    }

    static void VisualizarRegistros()
    {
        if (!VerificarFichero()) return;

        try
        {
            if (!File.Exists(ficheroActual))
            {
                Console.WriteLine("El fichero no existe en el disco.");
                ficheroActual = null;
                return;
            }

            string[] lineas = File.ReadAllLines(ficheroActual);

            if (lineas.Length == 0)
            {
                Console.WriteLine($"El fichero '{ficheroActual}' esta vacio.");
            }
            else
            {
                Console.WriteLine($"\n--- Contenido de {ficheroActual} ---");
                for (int i = 0; i < lineas.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {lineas[i]}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al leer el fichero: " + ex.Message);
        }
    }
}


