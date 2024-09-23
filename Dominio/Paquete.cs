using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Paquete : IValidable
    {
        private static int s_ultId = 1;
        private int _id;
        private DateTime _fecha;
        private Agencia _agencia;
        private static double s_costoBase = 100;
        private List<PaqueteDestino> _destinos = new List<PaqueteDestino>();

        public Paquete(DateTime fecha, Agencia agencia)
        {
            _id = s_ultId;
            s_ultId++;
            _fecha = fecha;
            _agencia = agencia;
        }

        public void Validar()
        {
            if (_fecha.Date == new DateTime(1, 1, 1)) throw new Exception("Debe ingresar una fecha valida");
            if (_agencia == null) throw new Exception("La agencia no puede ser nula");
        }

        public double CalcularPrecioFinal()
        {
            double suma = s_costoBase;
            foreach(PaqueteDestino pd in _destinos)
            {
                suma += pd.CalcularCosto();
            }

            return suma;
        }
    }
}
