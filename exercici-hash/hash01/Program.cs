using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace hash01
{

    class Program
    {
        static void Main(string[] args)
        {

            Boolean opcion = true;
            String escoger;

            do
            {

                Console.WriteLine("1.- Calcular el HASH del archivo.");
                Console.WriteLine("2.- Comprovar integridad del archivo");
                Console.WriteLine("3.- Salir");
                Console.WriteLine("¿Que deseas hacer?");
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
            } while (opcion = true);
        }

    


    static void CalcularHash()
    {

        try
        {
            if (File.Exists(rutaFitcher))
            {
                // Leer el contenido del archivo
                string text = File.ReadAllText(rutaFitcher);

                // Convertir el texto a un array de bytes
                byte[] bytesIn = Encoding.UTF8.GetBytes(text);

                using (SHA512Managed SHA512 = new SHA512Managed())
                {
                    // Calcular el hash
                    byte[] hashResult = SHA512.ComputeHash(bytesIn);

                    // Convertir el hash a un string hexadecimal
                    string textoHash = BitConverter.ToString(hashResult).Replace("-", string.Empty);

                    Console.WriteLine($"El hash del archivo es:  {rutaFitcher}:");
                    Console.WriteLine(textoHash);

                    // Guardar archivo Hash con extensión .SHA:
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
        Console.WriteLine("Introduce la ruta del archivo: ");
        String rutaFitcher = Console.ReadLine();

        Console.WriteLine("Introduce la ruta del mismo archivo con extensión .SHA: ");
        String rutaSHA = Console.ReadLine();

        try
        {
            if (File.Exists(rutaFitcher) && File.Exists(rutaSHA))
            {
                // Leer el contenido del archivo SHA
                string textSHA = File.ReadAllText(rutaSHA);

                // Calcular HASH
                string calcularHash;
                using (SHA512Managed SHA512 = new SHA512Managed())
                {
                    //Leer archivo 
                    String text = File.ReadAllText(rutaFitcher);

                    // Convertir texto en array y calcular HASH
                    byte[] bytesIn = Encoding.UTF8.GetBytes(text);

                    byte[] hashResult = SHA512.ComputeHash(bytesIn);

                    // Convertir el hash a un string hexadecimal
                    string textoHash = BitConverter.ToString(hashResult).Replace("-", string.Empty);

                }

                //Verificar integridad

                if(textoHash == textoSHA)
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
