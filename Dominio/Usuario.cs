using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Usuario : IValidable
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }

        public Usuario(string nombre, string email, string pass)
        {
            Nombre = nombre;
            Email = email;
            Pass = pass;
        }

        public virtual void Validar()
        {
            if (string.IsNullOrEmpty(Pass) || Pass.Length < 6) throw new Exception("La pass debe tener al menos 6 caracteres");
            if (string.IsNullOrEmpty(Email)) throw new Exception("El email no puede ser vacio");
            if (string.IsNullOrEmpty(Nombre)) throw new Exception("El nombre no puede ser vacio");
        }

        public abstract string Rol();

        public void CambiarPass(string passNueva)
        {
            if (string.IsNullOrEmpty(passNueva) || passNueva.Length < 6) throw new Exception("La pass nueva debe tener al menos 6 caracteres");
            if (passNueva == Pass) throw new Exception("La password no puede coincidir con la actual");
            Pass = passNueva;
        }
    }
}
