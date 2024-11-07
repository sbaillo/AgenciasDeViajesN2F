using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class PaquetesController : Controller
    {
        Sistema miSistema = Sistema.Instancia;

        [HttpGet]
        public IActionResult Alta()
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Admin")
            {
                return View("NoAutorizado");
            }

            if (TempData["Exito"] != null) ViewBag.Exito = TempData["Exito"];
            if (TempData["Error"] != null) ViewBag.Error = TempData["Error"];
            return View(new Paquete());
           
        }

        [HttpPost]
        public IActionResult Alta(Paquete paquete, int idAgencia)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Admin")
            {
                return View("NoAutorizado");
            }

            try
            {
                Agencia a = miSistema.ObtenerAgenciaPorId(idAgencia);
                if (a == null) throw new Exception("Agencia no encontrada");
                paquete.Agencia = a;
                Sistema.Instancia.AltaPaquete(paquete);
                TempData["Exito"] = "Paquete ingresado correctamente";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Alta");
        }
    }
}
