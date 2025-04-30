using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoEstrategia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido al Juego de Estrategia");
            Console.Write("Ingresa tu nombre: ");
            string nombreJugador = Console.ReadLine();

            Juego juego = new Juego(nombreJugador);
            juego.IniciarJuego();

            Console.WriteLine("Gracias por jugar, presiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
