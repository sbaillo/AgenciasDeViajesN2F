using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class DestinosController : Controller
    {
        Sistema miSistema = Sistema.Instancia;

        public IActionResult Listado()
        {
            if (TempData["Exito"] != null) ViewBag.Exito = TempData["Exito"];
            if (TempData["Error"] != null) ViewBag.Error = TempData["Error"];

            ViewBag.Listado = Sistema.Instancia.Destinos;
            return View();
        }

        public IActionResult Detalle(string id)
        {
            Destino d = Sistema.Instancia.ObtenerDestinoPorId(id);
            ViewBag.Destino = d;
            return View();
        }

        [HttpGet]
        public IActionResult Modificar()
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Admin")
            {
                return View("NoAutorizado");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Modificar(string idDestino, double precioNuevo)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Admin")
            {
                return View("NoAutorizado");
            }

            //Incluir validaciones
            try
            {
                miSistema.CambiarPrecioDestino(idDestino, precioNuevo);
                ViewBag.Exito = "Precio cambiado correctamente";
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }

        [HttpGet]
        public IActionResult CambiarPrecio(string id)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Admin")
            {
                return View("NoAutorizado");
            }

            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public IActionResult CambiarPrecio(string idDestino, double precioNuevo)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Admin")
            {
                return View("NoAutorizado");
            }

            //Incluir validaciones
            try
            {
                miSistema.CambiarPrecioDestino(idDestino, precioNuevo);
                TempData["Exito"] = "Precio cambiado correctamente";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Listado");
        }
    }
}
