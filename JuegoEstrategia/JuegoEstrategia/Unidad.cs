using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoEstrategia
{
    public abstract class Unidad : IAtacable
    {
        public string Nombre { get; protected set; }
        public int Vida { get; set; }
        public int Daño { get; protected set; }
        public bool EstaDestruido => Vida <= 0;

        public Unidad(string nombre, int vida, int daño)
        {
            Nombre = nombre;
            Vida = vida;
            Daño = daño;
        }

        public virtual void RecibirDaño(int cantidad)
        {
            Vida -= cantidad;
            if (Vida < 0) Vida = 0;
        }

        public virtual void Atacar(IAtacable objetivo)
        {
            objetivo.RecibirDaño(Daño);
            Console.WriteLine($"{Nombre} ataco y causo {Daño} de daño");
        }

        public abstract string ObtenerDescripcion();
    }
}
