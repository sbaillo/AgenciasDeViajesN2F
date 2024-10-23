using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Agencia : IValidable
    {
        private static int s_ultId = 1;
        private int _id;
        private string _nombre;
        private string _pais;

        public Agencia(string nombre, string pais)
        {
            _id = s_ultId;
            s_ultId++;
            _nombre = nombre;
            _pais = pais;
        }

        public int Id
        {
            get { return _id; }
        }

        public string Pais
        {
            get { return _pais; }
        }

        public virtual void Validar()
        {
            if (string.IsNullOrEmpty(_nombre)) throw new Exception("El nombre no puede ser vacio");
            if (string.IsNullOrEmpty(_pais)) throw new Exception("El pais no puede ser vacio");
        }

        public override string ToString()
        {
            return $"{_id} - Nombre: {_nombre} - Pais: {_pais}";
        }

        //Pueden hacer una u otra opcion para resolver lo mismo
        public abstract double DevolverPorcentaje();

        public abstract double AplicarDescuento(double subtotal);

        public override bool Equals(object? obj)
        {
            Agencia a = obj as Agencia;
            return a != null && _id == a._id;
        }
    }
}
