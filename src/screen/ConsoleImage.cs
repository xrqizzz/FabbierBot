
using System;
using System.Collections.Generic;

using controller.Fabbier;

namespace screen.ConsoleImage
{
    public class ConsoleImage()
    {
        public static void runner(string[] args)
        {
            Console.Write("\n ChatBot de Recetas de Cocina");
            Console.Write("\n Escribe una receta o categoria.");

            while (true)
            {
                Console.Write("\n Tu: ");
                string? inputUser = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(inputUser))
                {
                    continue;
                }
                if (string.Equals(inputUser, "exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\n byeee...");
                    break;
                }
                string response = Fabbier.GetResponse(inputUser);
                Console.Write("\nBot: " + response);
            }
        }
    }
}