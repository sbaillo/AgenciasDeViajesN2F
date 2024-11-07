using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class UsuariosController : Controller
    {
        Sistema miSistema = Sistema.Instancia;

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string pass)
        {
            try
            {
                if (string.IsNullOrEmpty(email)) throw new Exception("Debe ingresar un email");
                if (string.IsNullOrEmpty(pass)) throw new Exception("Debe ingresar una contraseña");
                Usuario usuario = miSistema.Login(email, pass);
                if (usuario == null) throw new Exception("Email o contraseña incorrecta");

                //Declaro variables de session con los datos necesarios para identificar al usuario logueado y su rol
                HttpContext.Session.SetString("email", usuario.Email);
                HttpContext.Session.SetString("rol", usuario.Rol());

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult CambiarPass()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CambiarPass(string pass, string passRepetida)
        {
            if(HttpContext.Session.GetString("email") == null)
            {
                return View("NoAutorizado");
            }

            try
            {
                if (string.IsNullOrEmpty(pass)) throw new Exception("La password no puede ser vacia");
                if (string.IsNullOrEmpty(passRepetida)) throw new Exception("Debe repetir el password");
                if (pass != passRepetida) throw new Exception("Las contraseñas no coinciden");
                miSistema.CambiarPassDeUsuario(HttpContext.Session.GetString("email"), pass);
                ViewBag.Exito = "Contraseña cambiada";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }
    }
}
