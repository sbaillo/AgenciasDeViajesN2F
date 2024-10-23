using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Sistema
    {
        private List<Agencia> _agencias = new List<Agencia>();
        private List<Destino> _destinos = new List<Destino>();
        private List<Paquete> _paquetes = new List<Paquete>();

        public Sistema()
        {
            PrecargarAgencias();
            PrecargarDestinos();
            PrecargarPaquetes();
            PrecargarDestinosAPaquete();
        }


        private void PrecargarAgencias()
        {
            AltaAgencia(new Nacional("Rutatour", "Uruguay", "0515189981", 1999));
            AltaAgencia(new Nacional("Buquebus", "Uruguay", "5515155879", 1992));
            AltaAgencia(new Internacional("Le route", "Francia", 4));
        }

        private void PrecargarDestinos()
        {
            AltaDestino(new Destino("ASDC1589", "Europa Antigua", "Recorré toda Europa", 150, TipoDestino.LOCALIDAD));
            AltaDestino(new Destino("CSIO2151", "America Loca", "Vivi America como nunca pensaste", 80, TipoDestino.LOCALIDAD));
            AltaDestino(new Destino("189198AF", "Paseo en globo en Turquia", "Anda en globo", 200, TipoDestino.ACTIVIDAD));
            AltaDestino(new Destino("18118AFG", "Puente de Londres", "Camina por el puente", 15, TipoDestino.PUNTOINTERES));
            AltaDestino(new Destino("CSIO2171", "Machu Pichu", "Arriba Machu Pichu", 100, TipoDestino.PUNTOINTERES));
            AltaDestino(new Destino("ARIO2171", "Antartida", "Explorá la antartida de noche", 100, TipoDestino.LOCALIDAD));
            AltaDestino(new Destino("ARIO2188", "Aurora Boreal", "Avistamiento de aurora boreal", 50, TipoDestino.ACTIVIDAD));
        }

        private void PrecargarPaquetes()
        {
            AltaPaquete(new Paquete(new DateTime(2024, 12, 20), ObtenerAgenciaPorId(1)));
            AltaPaquete(new Paquete(new DateTime(2025, 02, 15), ObtenerAgenciaPorId(2)));
            AltaPaquete(new Paquete(new DateTime(2023, 07, 5), ObtenerAgenciaPorId(3)));
        }

        private void PrecargarDestinosAPaquete()
        {
            AgregarDestinoAPaquete(1, "ASDC1589", 5);
            AgregarDestinoAPaquete(1, "189198AF", 1);
            AgregarDestinoAPaquete(1, "18118AFG", 1);
            AgregarDestinoAPaquete(2, "CSIO2151", 10);
            AgregarDestinoAPaquete(2, "CSIO2171", 1);
            AgregarDestinoAPaquete(3, "ARIO2171", 5);
            AgregarDestinoAPaquete(3, "ARIO2188", 1);
        }

        public void AltaAgencia(Agencia agencia)
        {
            if (agencia == null) throw new Exception("La agencia ingresada es nula");
            agencia.Validar();
            _agencias.Add(agencia);
        }

        public void AltaDestino(Destino destino)
        {
            if (destino == null) throw new Exception("El destino no puede ser nulo");
            destino.Validar();
            if (_destinos.Contains(destino)) throw new Exception("Ya existe un destino con el id dado");
            _destinos.Add(destino);
        }

        public void AltaPaquete(Paquete paquete)
        {
            if (paquete == null) throw new Exception("El paquete no puede estar vacio");
            paquete.Validar();
            _paquetes.Add(paquete);
        }

        public Agencia ObtenerAgenciaPorId(int id)
        {
            Agencia buscada = null;
            int i = 0;
            while(i < _agencias.Count && buscada == null)
            {
                if (_agencias[i].Id == id) buscada = _agencias[i];
                i++;
            }

            return buscada;
        }

        public void AgregarDestinoAPaquete(int idPaquete, string idDestino, int dias)
        {
            Paquete paqueteBuscado = ObtenerPaquetePorId(idPaquete);
            if (paqueteBuscado == null) throw new Exception("No se encontro el paquete por ese id");
            Destino destinoBuscado = ObtenerDestinoPorId(idDestino);
            if (destinoBuscado == null) throw new Exception("No se encontró un destino con el id dado");
            PaqueteDestino pd = new PaqueteDestino(destinoBuscado, dias);
         }

        public Destino ObtenerDestinoPorId(string id)
        {
            Destino buscado = null;
            int i = 0;
            while (i < _destinos.Count && buscado == null)
            {
                if (_destinos[i].Id == id) buscado = _destinos[i];
                i++;
            }

            return buscado;
        }

        public Paquete ObtenerPaquetePorId(int id)
        {
            Paquete buscado = null;
            int i = 0;
            while (i < _paquetes.Count && buscado == null)
            {
                if (_paquetes[i].Id == id) buscado = _paquetes[i];
                i++;
            }

            return buscado;
        }

        public List<Agencia> AgenciasPorPais(string pais)
        {
            List<Agencia> buscadas = new List<Agencia> ();
            foreach(Agencia a in _agencias)
            {
                if (a.Pais.ToUpper() == pais.ToUpper()) buscadas.Add(a);
            }

            return buscadas;
        }

        public List<Paquete> PaquetesConDuracionMayorA(int dias)
        {
            List<Paquete> buscados = new List<Paquete> ();
            foreach(Paquete p in _paquetes)
            {
                if(p.ContarTotalDias() >= dias) buscados.Add(p);
            }

            return buscados;
        }

        public List<Paquete> PaquetesPrecioSuperiorA(double monto)
        {
            List<Paquete> buscados = new List<Paquete>();
            foreach(Paquete p in _paquetes)
            {
                if(p.CalcularPrecioFinal() >= monto) buscados.Add(p);
            }

            return buscados;
        }

        public List<Paquete> PaquetesConDestino(string id)
        {
            Destino d = ObtenerDestinoPorId(id);
            List<Paquete> buscados = new List<Paquete>();
            foreach(Paquete p in _paquetes)
            {
                if(p.ContieneDestino(d)) buscados.Add(p);
            }

            return buscados;
        }

        public List<Paquete> PaquetesConMayorDuracion()
        {
            List<Paquete> mayores = new List<Paquete>();
            int cantMayor = int.MinValue;

            foreach(Paquete p in _paquetes)
            {
                int duracion = p.ContarTotalDias();
                if(duracion > cantMayor)
                {
                    cantMayor = duracion;
                    mayores.Clear();
                    mayores.Add(p);
                }
                else if(duracion ==  cantMayor)
                {
                    mayores.Add(p);
                }
            }

            return mayores;
        }

        public List<Agencia> AgenciasConPaquetes()
        {
            List<Agencia> listado = new List<Agencia>();
            foreach(Paquete p in _paquetes)
            {
                if(!listado.Contains(p.Agencia)) listado.Add(p.Agencia);
            }

            return listado;
        }

        public List<Agencia> AgenciasSinPaquetes()
        {
            List<Agencia> listado = new List<Agencia>();
            List<Agencia> agenciasConPaquetes = AgenciasConPaquetes();
            foreach (Agencia a in _agencias)
            {
                if(!agenciasConPaquetes.Contains(a)) listado.Add(a);
            }

            return listado;
        }

        public bool TienePaquete(Agencia a)
        {
            bool tiene = false;
            int i = 0;
            while(i < _paquetes.Count && !tiene)
            {
                if (_paquetes[i].Agencia.Equals(a)) tiene = true;
                i++;
            }

            return tiene;
        }

        public List<Agencia> AgenciaSinPaquetesVersionDos()
        {
            List<Agencia> listado = new List<Agencia>();
            foreach(Agencia a in _agencias)
            {
                if(TienePaquete(a)) listado.Add(a);
            }

            return listado;
        }
    }
}
