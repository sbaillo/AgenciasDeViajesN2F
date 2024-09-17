﻿using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class PaqueteDestino : IValidable
    {
        private Destino _destino;
        private int _dias;

        public PaqueteDestino(Destino destino, int dias)
        {
            _destino = destino;
            _dias = dias;
        }

        public void Validar()
        {
            if (_destino == null) throw new Exception("El destino no puede ser nulo");
            ValidarDias();
        }

        private void ValidarDias()
        {
            if (_dias < 1) throw new Exception("La cantidad de dias para un destino deber ser 1 como minimo");
        }

        public double CalcularCosto()
        {
            return _dias * _destino.PrecioPorDia;
        }
    }
}