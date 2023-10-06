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

            String rutaFitcher = null;
            Console.Write("Introduce la ruta del archivo de texto: ");
            rutaFitcher = Console.ReadLine();
         
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
                        string hashText = BitConverter.ToString(hashResult).Replace("-", string.Empty);

                        Console.WriteLine($"El hash del archivo es:  {rutaFitcher}:");
                        Console.WriteLine(hashText);
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
           
            Console.ReadKey();


            }

        }
    }
