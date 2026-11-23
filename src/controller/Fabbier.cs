using System;
using System.Collections.Generic;

namespace controller.Fabbier
{
    public class Fabbier()
    {
        private static readonly Dictionary<string, List<String>> promp = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                {"Pollo al limón", new List<string> {"Pechuga de pollo", "Limón", "Ajo", "Aceite de oliva", "Romero"}},
                {"Tacos al pastor", new List<string> {"Carne de cerdo", "Piña", "Achiote", "Tortillas de maíz", "Cilantro", "Cebolla"}},
                {"Spaghetti Carbonara", new List<string> {"Espagueti", "Huevo", "Queso pecorino", "Guanciale o tocino", "Pimienta negra"}},
                {"Guacamole", new List<string> {"Aguacate", "Cebolla", "Cilantro", "Tomate", "Limón", "Chile serrano"}},
                {"Ceviche de pescado", new List<string> {"Filete de pescado blanco", "Limón", "Cebolla morada", "Cilantro", "Pepino", "Chile"}},
                {"Enchiladas verdes", new List<string> {"Tortillas de maíz", "Pollo deshebrado", "Tomate verde", "Chile serrano", "Crema", "Queso fresco"}},
                {"Risotto de champiñones", new List<string> {"Arroz arborio", "Champiñones", "Caldo de verduras", "Vino blanco", "Queso parmesano", "Mantequilla"}},
                {"Lasaña a la boloñesa", new List<string> {"Láminas de lasaña", "Carne molida de res", "Salsa de tomate", "Salsa bechamel", "Queso mozzarella"}},
                {"Salmón al horno", new List<string> {"Filete de salmón", "Espárragos", "Mantequilla", "Eneldo", "Limón", "Ajo"}},
                {"Chilaquiles rojos", new List<string> {"Totopos de maíz", "Salsa roja de jitomate", "Pollo o huevo", "Crema", "Queso añejo", "Cebolla"}},
                {"Paella de mariscos", new List<string> {"Arroz bomba", "Camarones", "Mejillones", "Calamares", "Azafrán", "Caldo de pescado", "Pimiento"}},
                {"Tortilla española", new List<string> {"Huevos", "Papas", "Cebolla", "Aceite de oliva", "Sal"}},
                {"Ensalada César", new List<string> {"Lechuga romana", "Pechuga de pollo", "Crutones", "Queso parmesano", "Aderezo César"}},
                {"Fajitas de res", new List<string> {"Arrachera o falda de res", "Pimientos de colores", "Cebolla", "Tortillas de harina", "Salsa de soya"}},
                {"Brownies de chocolate", new List<string> {"Chocolate oscuro", "Mantequilla", "Azúcar", "Huevos", "Harina de trigo", "Nueces"}}
            };
        
        public static List<string> GetAllRecipeNames()
        {
            return promp.Keys.ToList();
        }

        public static string GetAllRecipeNamesAsText(string separator = ", ")
        {
            return string.Join(separator, promp.Keys);
        }
        public static string GetResponse(string input)
        {
            if (promp.TryGetValue(input, out List<string>? responses) && responses is not null)
            {
                Random random = new Random();
                int index = random.Next(responses.Count);
                return responses[index];
            }
            else
            {
                string listaTexto = Fabbier.GetAllRecipeNamesAsText("\n");
                Console.WriteLine(listaTexto);
                return "No se encontró una respuesta para la entrada proporcionada.";
            }
        }
    }
}