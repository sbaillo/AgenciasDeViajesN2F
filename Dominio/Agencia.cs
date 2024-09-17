using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Agencia : IValidable
    {
        private static int s_ultId = 1;
        private int _id;
        private string _nombre;
        private string _pais;

        public Agencia(int id, string nombre, string pais)
        {
            _id = s_ultId;
            s_ultId++;
            _nombre = nombre;
            _pais = pais;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(_nombre)) throw new Exception("El nombre no puede ser vacio");
            if (string.IsNullOrEmpty(_pais)) throw new Exception("El pais no puede ser vacio");
        }
    }
}
