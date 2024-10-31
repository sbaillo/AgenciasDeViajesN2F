using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Paquete : IValidable
    {
        private static int s_ultId = 1;
        private int _id;
        private DateTime _fecha;
        private Agencia _agencia;
        private static double s_costoBase = 100;
        private List<PaqueteDestino> _destinos = new List<PaqueteDestino>();
        private double _precioFinalGuardado = 0;

        public int Id 
        { 
            get { return _id; } 
        }

        public Agencia Agencia
        {
            get { return _agencia; }
            set { _agencia = value; }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public Paquete(DateTime fecha, Agencia agencia)
        {
            _id = s_ultId;
            s_ultId++;
            _fecha = fecha;
            _agencia = agencia;
        }

        public Paquete()
        {
            _id = s_ultId;
            s_ultId++;
        }

        public void Validar()
        {
            if (_fecha.Date == new DateTime(1, 1, 1)) throw new Exception("Debe ingresar una fecha valida");
            if (_agencia == null) throw new Exception("La agencia no puede ser nula");
        }

        public double CalcularPrecioFinal()
        {
            if(_precioFinalGuardado == 0)
            {
                double suma = s_costoBase;
                foreach (PaqueteDestino pd in _destinos)
                {
                    suma += pd.CalcularCosto();
                }

                double porcentajeDescuento = _agencia.DevolverPorcentaje();
                suma -= suma * porcentajeDescuento / 100;

                //Con el otro metodo sería asi
                //suma = _agencia.AplicarDescuento(suma);

                _precioFinalGuardado = suma;
            }
            
            return _precioFinalGuardado;
        }

        public void AgregarDestinoAlPaquete(PaqueteDestino pd)
        {
            if (pd == null) throw new Exception("El paquete-destino es nulo");
            pd.Validar();
            if (_destinos.Contains(pd)) throw new Exception("El paquete ya contiene el destino");
            _destinos.Add(pd);
        }

        public int ContarTotalDias()
        {
            int total = 0;
            foreach(PaqueteDestino pd in _destinos)
            {
                total += pd.Dias;
            }

            return total;
        }

        public bool ContieneDestino(Destino d)
        {
            bool tiene = false;
            int i = 0;
            while(!tiene && i < _destinos.Count)
            {
                if (_destinos[i].Destino.Equals(d)) tiene = true;
                i++;
            }

            return tiene;
        }
    }
}
