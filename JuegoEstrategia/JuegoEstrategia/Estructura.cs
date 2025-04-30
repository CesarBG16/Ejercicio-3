using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoEstrategia
{
    public abstract class Estructura : IAtacable
    {
        public string Nombre { get; protected set; }
        public int Vida { get; set; }
        public int CostoEnergia { get; protected set; }
        public int TurnosConstruccion { get; protected set; }
        public int TurnosRestantes { get; set; }
        public bool EstaCompleta => TurnosRestantes <= 0;
        public bool EstaDestruido => Vida <= 0;

        public Estructura(string nombre, int vida, int costoEnergia, int turnosConstruccion)
        {
            Nombre = nombre;
            Vida = vida;
            CostoEnergia = costoEnergia;
            TurnosConstruccion = turnosConstruccion;
            TurnosRestantes = turnosConstruccion;
        }

        public void AvanzarConstruccion()
        {
            if (!EstaCompleta)
            {
                TurnosRestantes--;
            }
        }

        public virtual void RecibirDaño(int cantidad)
        {
            Vida -= cantidad;
            if (Vida < 0) Vida = 0;
        }

        public abstract string ObtenerDescripcion();
    }
}
