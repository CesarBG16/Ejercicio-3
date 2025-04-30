using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoEstrategia
{
    public class Jugador
    {
        public string Nombre { get; private set; }
        public int Energia { get; set; }
        public List<Estructura> Estructuras { get; private set; }
        public List<Unidad> Unidades { get; private set; }
        public int TurnosJugados { get; set; }
        public int EnemigosEliminados { get; set; }

        public Jugador(string nombre)
        {
            Nombre = nombre;
            Energia = 200;
            Estructuras = new List<Estructura>();
            Unidades = new List<Unidad>();
            TurnosJugados = 0;
            EnemigosEliminados = 0;
        }

        public bool ConstruirEstructura(string tipo)
        {
            Estructura nuevaEstructura;

            switch (tipo.ToLower())
            {
                case "granja":
                    nuevaEstructura = new Granja();
                    break;
                case "torre":
                    nuevaEstructura = new Torre();
                    break;
                case "casa":
                    nuevaEstructura = new Casa();
                    break;
                default:
                    Console.WriteLine("Tipo de estructura no valida");
                    return false;
            }

            if (Energia >= nuevaEstructura.CostoEnergia)
            {
                Energia -= nuevaEstructura.CostoEnergia;
                Estructuras.Add(nuevaEstructura);
                Console.WriteLine($"Has comenzado a construir {nuevaEstructura.Nombre}, estara lista en {nuevaEstructura.TurnosRestantes} turnos");
                return true;
            }
            else
            {
                Console.WriteLine("No tienes suficiente energia para construir esta estructura");
                return false;
            }
        }

        public bool CrearUnidad(string tipo)
        {
            bool tieneCasaDisponible = Estructuras.Any(e => e is Casa && e.EstaCompleta);

            if (!tieneCasaDisponible)
            {
                Console.WriteLine("Necesitas al menos una casa completada para crear unidades");
                return false;
            }

            Unidad nuevaUnidad;
            bool requiereEnergia = false;

            switch (tipo.ToLower())
            {
                case "soldado":
                    nuevaUnidad = new Soldado();
                    break;
                case "arquero":
                    nuevaUnidad = new Arquero();
                    requiereEnergia = true;
                    break;
                default:
                    Console.WriteLine("Tipo de unidad no valida");
                    return false;
            }

            if (requiereEnergia && Energia < 50)
            {
                Console.WriteLine("No tienes suficiente energia para crear esta unidad");
                return false;
            }

            if (requiereEnergia)
            {
                Energia -= 50;
            }

            Unidades.Add(nuevaUnidad);
            Console.WriteLine($"Has creado un {nuevaUnidad.Nombre}");
            return true;
        }

        public void PasarTurno()
        {
            TurnosJugados++;

            Energia += 20;

            foreach (Estructura estructura in Estructuras)
            {
                if (estructura is Granja granja && estructura.EstaCompleta && !estructura.EstaDestruido)
                {
                    Energia += granja.ProduccionEnergia;
                }

                if (!estructura.EstaCompleta)
                {
                    estructura.AvanzarConstruccion();
                    if (estructura.EstaCompleta)
                    {
                        Console.WriteLine($"Tu {estructura.Nombre} ha sido construida completamente");
                    }
                }
            }

            Console.WriteLine($"Has ganado {20} de energia base este turno");
            Console.WriteLine($"Energia actual: {Energia}");
        }

        public bool TieneEstructuras()
        {
            return Estructuras.Any(e => !e.EstaDestruido);
        }
    }
}

