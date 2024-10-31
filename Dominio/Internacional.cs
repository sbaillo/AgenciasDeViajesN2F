using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Internacional : Agencia
    {
        private int _calificacion;

        public Internacional(string nombre, string pais, int calificacion) : base(nombre, pais)
        {
            _calificacion = calificacion;
        }

        public Internacional():base()
        {

        }

        public int Calificacion
        {
            get { return _calificacion; }
            set { _calificacion = value; }
        }

        private void ValidarCalificacion()
        {
            if (_calificacion < 1 || _calificacion > 5) throw new Exception("La calificacion debe estar entre 1 y 5");
        }

        public override void Validar()
        {
            base.Validar();
            ValidarCalificacion();
        }

        public override double DevolverPorcentaje()
        {
            double porc = 5;
            if (_calificacion > 3) porc = 8;
            return porc;
        }

        public override double AplicarDescuento(double subtotal)
        {
            double retorno = subtotal;
            if (_calificacion <= 3)
            {
                retorno *= 0.95;
            }
            else
            {
                retorno *= 0.92;
            }

            return retorno;
        }
    }
}
