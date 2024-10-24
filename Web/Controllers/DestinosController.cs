using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class DestinosController : Controller
    {
        public IActionResult Listado()
        {
            ViewBag.Listado = Sistema.Instancia.Destinos;
            return View();
        }

        public IActionResult Detalle(string id)
        {
            Destino d = Sistema.Instancia.ObtenerDestinoPorId(id);
            ViewBag.Destino = d;
            return View();
        }
    }
}
