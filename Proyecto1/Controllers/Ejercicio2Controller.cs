using Microsoft.AspNetCore.Mvc;

namespace Proyecto1.Controllers
{
    public class Ejercicio2Controller : Controller
    {
        private readonly static String[] cadenas =
        {
            "Manzana",
            "Pera",
            "Uva",
            "Kiwi",
            "Mandarina"
        };

        public IActionResult BuscarIndice(int indice)
        {
            if (indice < 1 || indice > cadenas.Length) return Content("Error: indice fuera de rango");
            
            return Content(cadenas[indice-1]);
        }

        public IActionResult BuscarCadena(String cadena)
        {
            if (String.IsNullOrEmpty(cadena))
                return Content("Introduzca una cadena valida!");

            bool existe = false;
            foreach(String c in cadenas)
            {
                String cadenaBusqueda = c.ToUpper();
                if (cadenaBusqueda.Equals(cadena.ToUpper()))
                {
                    existe = true;
                    break;
                }
            }

            if (existe)
                return Content($"La cadena '{cadena}' existe dentro del arreglo.");
            else
                return Content($"La cadena '{cadena}' NO existe dentro del arreglo.");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
