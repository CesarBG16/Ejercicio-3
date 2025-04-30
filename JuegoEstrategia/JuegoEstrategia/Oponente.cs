using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoEstrategia
{
    public class Oponente
    {
        public List<Unidad> Unidades { get; private set; }
        public int TurnoActual { get; private set; }

        private readonly Random _random;
        private readonly int[] _numerosPrimos = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31 };
        private readonly int[] _secuenciaFibonacci = { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 };
        private int _indiceFibonacci;

        public Oponente()
        {
            Unidades = new List<Unidad>();
            TurnoActual = 1;
            _random = new Random();
            _indiceFibonacci = 0;
        }

        public void PasarTurno(Jugador jugador)
        {
            TurnoActual++;

            if (TurnoActual <= 10)
            {
                Console.WriteLine("El oponente esta inactivo durante los primeros 10 turnos");
                return;
            }

            if (Array.IndexOf(_numerosPrimos, TurnoActual) >= 0)
            {
                int fibonacciActual = _secuenciaFibonacci[_indiceFibonacci % _secuenciaFibonacci.Length];
                _indiceFibonacci++;

                if (fibonacciActual % 2 == 0)
                {
                    Unidades.Add(new Soldado());
                    Console.WriteLine("El oponente ha creado un Soldado");
                }
                else
                {
                    Unidades.Add(new Arquero());
                    Console.WriteLine("El oponente ha creado un Arquero");
                }
            }

            foreach (Unidad unidad in Unidades.Where(u => !u.EstaDestruido).ToList())
            {
                IAtacable objetivo = null;
                if (jugador.Unidades.Any(u => !u.EstaDestruido))
                {
                    List<Unidad> unidadesVivas = jugador.Unidades.Where(u => !u.EstaDestruido).ToList();
                    objetivo = unidadesVivas[_random.Next(unidadesVivas.Count)];
                }
                else if (jugador.Estructuras.Any(e => e is Torre && !e.EstaDestruido))
                {
                    List<Estructura> torres = jugador.Estructuras.Where(e => e is Torre && !e.EstaDestruido).ToList();
                    objetivo = torres[_random.Next(torres.Count)];
                }
                else if (jugador.Estructuras.Any(e => !e.EstaDestruido))
                {
                    List<Estructura> estructurasVivas = jugador.Estructuras.Where(e => !e.EstaDestruido).ToList();
                    objetivo = estructurasVivas[_random.Next(estructurasVivas.Count)];
                }

                if (objetivo != null)
                {
                    unidad.Atacar(objetivo);

                    if (objetivo.EstaDestruido)
                    {
                        if (objetivo is Unidad)
                        {
                            Console.WriteLine($"El oponente ha destruido a tu {((Unidad)objetivo).Nombre}");
                        }
                        else if (objetivo is Estructura)
                        {
                            Console.WriteLine($"El oponente ha destruido tu {((Estructura)objetivo).Nombre}");
                        }
                    }
                }
            }

            Unidades.RemoveAll(u => u.EstaDestruido);
        }
    }
}

