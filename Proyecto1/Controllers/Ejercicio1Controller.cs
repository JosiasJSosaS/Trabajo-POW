using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto1.Controllers
{
    public class Ejercicio1Controller : Controller
    {
        // GET: Ejercicio1Controller
        public IActionResult Index()
        {
            return Ok("Hola, mundo!");
        }

        public IActionResult Convertir(String cadena)
        {
            if (String.IsNullOrEmpty(cadena)) 
                return Content("No se proporciono una cadena valida");

            cadena = cadena.ToUpper();

            char[] temp = cadena.ToCharArray();

            Array.Reverse(temp);

            cadena = new String(temp);

            return Content(cadena);
        }

        public IActionResult Comparar(String cadena1, String cadena2)
        {
            if (String.IsNullOrEmpty(cadena1) || String.IsNullOrEmpty(cadena2))
                return Content("No se proporcionaron las sufucientes cadenas validas");

            if (cadena1.Equals(cadena2))
                return Content($"Las cadenas '{cadena1}' y '{cadena2}' son iguales.");
            else
                return Content($"Las cadenas '{cadena1}' y '{cadena2}' NO son iguales.");
        }

        public IActionResult Perfil()
        {
            return View();
        }
    }
}
