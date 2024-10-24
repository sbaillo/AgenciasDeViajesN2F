using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AgenciasController : Controller
    {
        private Sistema miSistema = Sistema.Instancia;

        public IActionResult Listado()
        {
            List<Agencia> todasLasAgenciasDeSistema = miSistema.Agencias;
            ViewBag.Listado = todasLasAgenciasDeSistema;
            return View();
        }

        public IActionResult PorPais()
        {
            List<Agencia> todasLasAgenciasDeSistema = miSistema.Agencias;
            ViewBag.Listado = todasLasAgenciasDeSistema;
            return View();
        }

        public IActionResult ProcesarPorPais(string pais)
        {
            try
            {
                if (string.IsNullOrEmpty(pais)) throw new Exception("Debe ingresar un pais");
                List<Agencia> listado = miSistema.AgenciasPorPais(pais);
                if (listado.Count == 0) throw new Exception($"No se encontraron agencias de {pais}");
                ViewBag.Listado = listado;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View("PorPais");
        }


    }
}
