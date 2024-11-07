using Dominio;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace Web.Controllers
{
    public class AgenciasController : Controller
    {
        private Sistema miSistema = Sistema.Instancia;

        [HttpGet]
        public IActionResult Listado()
        {
            if(HttpContext.Session.GetString("email") == null)
            {
                return View("NoAutorizado");
            }

            List<Agencia> todasLasAgenciasDeSistema = miSistema.Agencias;
            ViewBag.Listado = todasLasAgenciasDeSistema;
            return View();
        }

        [HttpGet]
        public IActionResult PorPais()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return View("NoAutorizado");
            }

            List<Agencia> todasLasAgenciasDeSistema = miSistema.Agencias;
            ViewBag.Listado = todasLasAgenciasDeSistema;
            return View();
        }

        [HttpGet]
        public IActionResult ProcesarPorPais(string pais)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return View("NoAutorizado");
            }

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

        [HttpGet]
        public IActionResult AltaNacional()
        {
            if(HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Admin")
            {
                return View("NoAutorizado");
            }

            return View();
        }

        [HttpPost]
        public IActionResult AltaNacional(string nombre, string pais, string rut, int anio)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Admin")
            {
                return View("NoAutorizado");
            }

            try
            {
                if (string.IsNullOrEmpty(nombre)) throw new Exception("El nombre no puede ser vacio");
                if (string.IsNullOrEmpty(pais)) throw new Exception("El pais no debe estar vacio");
                if (string.IsNullOrEmpty(rut)) throw new Exception("El RUT no puede ser vacio");
                if (anio <= 0) throw new Exception("El año debe ser positivo");

                Agencia miAgencia = new Nacional(nombre, pais, rut, anio);
                miSistema.AltaAgencia(miAgencia);
                ViewBag.Exito = $"Se dio de alta correctamente la agencia {nombre}";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Nombre = nombre;
                ViewBag.Pais = pais;
                ViewBag.Rut = rut;
                ViewBag.Anio = anio;
            }

            return View("AltaNacional");
        }

        [HttpGet]
        public IActionResult AltaInternacional()
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Admin")
            {
                return View("NoAutorizado");
            }

            return View(new Internacional());
        }

        [HttpPost]
        public IActionResult AltaInternacional(Internacional agencia)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Admin")
            {
                return View("NoAutorizado");
            }

            try
            {
                if (string.IsNullOrEmpty(agencia.Nombre)) throw new Exception("El nombre no puede ser vacio");
                if (string.IsNullOrEmpty(agencia.Pais)) throw new Exception("El pais no puede ser nulo");
                if (agencia.Calificacion <= 0) throw new Exception("La calificacion tiene que ser positiva");
                miSistema.AltaAgencia(agencia);
                ViewBag.Exito = $"Agencia internacional {agencia.Nombre} dada de alta correctamente";
                return View(new Internacional());
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(agencia);
            }
        }

        
    }
}
