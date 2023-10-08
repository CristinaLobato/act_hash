using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace hash01
{
    class Program
    {
        // Declarar las rutas de archivo a nivel de clase
        static string rutaFitcher;
        static string rutaSHA;

        static void Main(string[] args)
        {
            bool opcion = true;
            string escoger;

            do
            {
                Console.WriteLine("1.- Calcular el HASH del archivo.");
                Console.WriteLine("2.- Comprobar integridad del archivo");
                Console.WriteLine("3.- Salir");
                Console.WriteLine("¿Qué deseas hacer?");
                escoger = Console.ReadLine();

                switch (escoger)
                {
                    case "1":
                        CalcularHash();
                        break;
                    case "2":
                        Integridad();
                        break;
                    case "3":
                        opcion = false;
                        Console.WriteLine("Hasta pronto.");
                        break;
                    default:
                        Console.WriteLine("Esta opción no está en el menú. Elige una de las tres opciones descritas.");
                        break;
                }
            } while (opcion);
        }

        static void CalcularHash()
        {
            Console.WriteLine("Introduce la ruta del archivo: ");
            rutaFitcher = Console.ReadLine();

            try
            {
                if (File.Exists(rutaFitcher))
                {
                    string text = File.ReadAllText(rutaFitcher);
                    byte[] bytesIn = Encoding.UTF8.GetBytes(text);

                    using (SHA512Managed SHA512 = new SHA512Managed())
                    {
                        byte[] hashResult = SHA512.ComputeHash(bytesIn);
                        string textoHash = BitConverter.ToString(hashResult).Replace("-", string.Empty);

                        Console.WriteLine($"El hash del archivo es: {rutaFitcher}:");
                        Console.WriteLine(textoHash);

                        string rutaSHA = Path.ChangeExtension(rutaFitcher, ".SHA");
                        File.WriteAllText(rutaSHA, textoHash);
                        Console.WriteLine($"El hash se ha guardado en el archivo: {rutaSHA}");
                    }
                }
                else
                {
                    Console.WriteLine($"¡ERROR! El archivo {rutaFitcher} no existe.");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Ha habido un error al leer el archivo: {e.Message}");
            }
        }

        static void Integridad()
        {
            Console.WriteLine("Introduce la ruta del archivo con extensión .SHA: ");
            rutaSHA = Console.ReadLine();

            try
            {
                if (File.Exists(rutaFitcher) && File.Exists(rutaSHA))
                {
                    string textSHA = File.ReadAllText(rutaSHA);
                    string textoHash;

                    using (SHA512Managed SHA512 = new SHA512Managed())
                    {
                        string text = File.ReadAllText(rutaFitcher);
                        byte[] bytesIn = Encoding.UTF8.GetBytes(text);
                        byte[] hashResult = SHA512.ComputeHash(bytesIn);
                        textoHash = BitConverter.ToString(hashResult).Replace("-", string.Empty);
                    }

                    if (textoHash == textSHA)
                    {
                        Console.WriteLine("La integridad del archivo es correcta ya que el HASH coincide.");
                    }
                    else
                    {
                        Console.WriteLine("La integridad del archivo NO es correcta ya que el HASH NO coincide.");
                    }
                }
                else
                {
                    Console.WriteLine($"¡ERROR! El archivo {rutaFitcher} o {rutaSHA} no existe.");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Ha habido un error al leer el archivo: {e.Message}");
            }
        }
    }
}
