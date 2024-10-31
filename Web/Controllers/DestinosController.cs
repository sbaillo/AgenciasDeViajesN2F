using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class DestinosController : Controller
    {
        Sistema miSistema = Sistema.Instancia;

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

        [HttpGet]
        public IActionResult Modificar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Modificar(string idDestino, double precioNuevo)
        {
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
    }
}
