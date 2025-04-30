using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoEstrategia
{
    public class Juego
    {
        private Jugador _jugador;
        private Oponente _oponente;
        private bool _juegoTerminado;

        public Juego(string nombreJugador)
        {
            _jugador = new Jugador(nombreJugador);
            _oponente = new Oponente();
            _juegoTerminado = false;
        }

        public void IniciarJuego()
        {
            Console.WriteLine($"Bienvenido {_jugador.Nombre} al Juego de Estrategia");
            Console.WriteLine("Construye estructuras, crea unidades y derrota a tu oponente");

            while (!_juegoTerminado)
            {
                EjecutarTurnoJugador();

                if (_juegoTerminado) break;

                EjecutarTurnoOponente();

                VerificarEstadoJuego();
            }
        }

        private void EjecutarTurnoJugador()
        {
            Console.WriteLine("\n======= TURNO DEL JUGADOR =======");
            Console.WriteLine($"Turno: {_jugador.TurnosJugados + 1}");
            Console.WriteLine($"Energía: {_jugador.Energia}");

            bool turnoFinalizado = false;

            while (!turnoFinalizado)
            {
                MostrarOpciones();
                string opcion = Console.ReadLine().ToLower();

                switch (opcion)
                {
                    case "1":
                        MostrarEstadoBase();
                        break;
                    case "2":
                        ConstruirEstructura();
                        break;
                    case "3":
                        CrearUnidad();
                        break;
                    case "4":
                        IniciarCombate();
                        _jugador.PasarTurno();
                        turnoFinalizado = true;
                        break;
                    case "salir":
                        Console.WriteLine("Estas seguro que deseas salir? (s/n)");
                        if (Console.ReadLine().ToLower() == "s")
                        {
                            _juegoTerminado = true;
                            turnoFinalizado = true;
                        }
                        break;
                    default:
                        Console.WriteLine("Opcion no valida, intenta de nuevo");
                        break;
                }
            }
        }

        private void MostrarOpciones()
        {
            Console.WriteLine("\nQue deseas hacer?");
            Console.WriteLine("1. Ver tu base (recursos, estructuras y unidades)");
            Console.WriteLine("2. Construir una estructura");
            Console.WriteLine("3. Crear una unidad");
            Console.WriteLine("4. Iniciar combate y pasar turno");
            Console.WriteLine("Escribe 'salir' para terminar el juego");
            Console.Write("> ");
        }

        private void MostrarEstadoBase()
        {
            Console.WriteLine("\n--- ESTADO DE TU BASE ---");
            Console.WriteLine($"Energía: {_jugador.Energia}");

            Console.WriteLine("\nEstructuras:");
            if (_jugador.Estructuras.Count == 0)
            {
                Console.WriteLine("No tienes estructuras");
            }
            else
            {
                foreach (var estructura in _jugador.Estructuras)
                {
                    string estado = estructura.EstaCompleta ?
                        (estructura.EstaDestruido ? "Destruida" : "Completa") :
                        $"En construcción ({estructura.TurnosRestantes} turnos restantes)";

                    Console.WriteLine($"- {estructura.Nombre}: {estado} - Vida: {estructura.Vida}");
                }
            }

            Console.WriteLine("\nUnidades:");
            if (_jugador.Unidades.Count == 0)
            {
                Console.WriteLine("No tienes unidades");
            }
            else
            {
                foreach (var unidad in _jugador.Unidades)
                {
                    string estado = unidad.EstaDestruido ? "Destruida" : "Activa";
                    Console.WriteLine($"- {unidad.Nombre}: {estado} - Vida: {unidad.Vida}, Daño: {unidad.Daño}");
                }
            }

            Console.WriteLine("\nOponente:");
            Console.WriteLine($"Unidades enemigas: {_oponente.Unidades.Count}");
            foreach (var unidad in _oponente.Unidades)
            {
                Console.WriteLine($"- {unidad.Nombre}: Vida: {unidad.Vida}, Daño: {unidad.Daño}");
            }
        }

        private void ConstruirEstructura()
        {
            Console.WriteLine("\n--- CONSTRUIR ESTRUCTURA ---");
            Console.WriteLine("Estructuras disponibles:");
            Console.WriteLine("1. Granja (50 energia, 5 turnos) - Genera 5 energia extra por turno");
            Console.WriteLine("2. Torre (100 energia, 10 turnos) - Inflige daño a las unidades enemigas");
            Console.WriteLine("3. Casa (50 energía, 2 turnos) - Permite crear unidades");
            Console.WriteLine("0. Cancelar");
            Console.Write("> ");

            string opcion = Console.ReadLine();
            string tipoEstructura = "";

            switch (opcion)
            {
                case "1":
                    tipoEstructura = "granja";
                    break;
                case "2":
                    tipoEstructura = "torre";
                    break;
                case "3":
                    tipoEstructura = "casa";
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opcion no valida");
                    return;
            }

            _jugador.ConstruirEstructura(tipoEstructura);
        }

        private void CrearUnidad()
        {
            Console.WriteLine("\n--- CREAR UNIDAD ---");

            bool tieneCasaDisponible = _jugador.Estructuras.Any(e => e is Casa && e.EstaCompleta && !e.EstaDestruido);

            if (!tieneCasaDisponible)
            {
                Console.WriteLine("Necesitas al menos una casa completada para crear unidades");
                return;
            }

            Console.WriteLine("Unidades disponibles:");
            Console.WriteLine("1. Soldado (Gratis, requiere casa) - 30 vida, 8 daño");
            Console.WriteLine("2. Arquero (50 energia, requiere casa) - 20 vida, 12 daño");
            Console.WriteLine("0. Cancelar");
            Console.Write("> ");

            string opcion = Console.ReadLine();
            string tipoUnidad = "";

            switch (opcion)
            {
                case "1":
                    tipoUnidad = "soldado";
                    break;
                case "2":
                    tipoUnidad = "arquero";
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opcion no valida");
                    return;
            }

            _jugador.CrearUnidad(tipoUnidad);
        }

        private void IniciarCombate()
        {
            Console.WriteLine("\n--- INICIANDO COMBATE ---");

            if (_jugador.Unidades.Count == 0 && _jugador.Estructuras.Count(e => e is Torre && e.EstaCompleta && !e.EstaDestruido) == 0)
            {
                Console.WriteLine("No tienes unidades ni torres para atacar");
                return;
            }

            if (_oponente.Unidades.Count == 0)
            {
                Console.WriteLine("No hay unidades enemigas para atacar");
                return;
            }

            foreach (var unidad in _jugador.Unidades.Where(u => !u.EstaDestruido).ToList())
            {
                if (_oponente.Unidades.Count == 0) break;

                var random = new Random();
                var unidadesEnemigasVivas = _oponente.Unidades.Where(u => !u.EstaDestruido).ToList();

                if (unidadesEnemigasVivas.Count > 0)
                {
                    var objetivo = unidadesEnemigasVivas[random.Next(unidadesEnemigasVivas.Count)];
                    unidad.Atacar(objetivo);

                    if (objetivo.EstaDestruido)
                    {
                        Console.WriteLine($"Has destruido a un {objetivo.Nombre} enemigo");
                        _jugador.EnemigosEliminados++;
                        _oponente.Unidades.Remove(objetivo);
                    }
                }
            }

            foreach (var estructura in _jugador.Estructuras.Where(e => e is Torre && e.EstaCompleta && !e.EstaDestruido).ToList())
            {
                if (_oponente.Unidades.Count == 0) break;

                var torre = (Torre)estructura;
                var random = new Random();
                var unidadesEnemigasVivas = _oponente.Unidades.Where(u => !u.EstaDestruido).ToList();

                if (unidadesEnemigasVivas.Count > 0)
                {
                    var objetivo = unidadesEnemigasVivas[random.Next(unidadesEnemigasVivas.Count)];
                    torre.AtacarEnemigo(objetivo);

                    if (objetivo.EstaDestruido)
                    {
                        Console.WriteLine($"Tu torre ha destruido a un {objetivo.Nombre} enemigo");
                        _jugador.EnemigosEliminados++;
                        _oponente.Unidades.Remove(objetivo);
                    }
                }
            }
        }

        private void EjecutarTurnoOponente()
        {
            Console.WriteLine("\n======= TURNO DEL OPONENTE =======");
            _oponente.PasarTurno(_jugador);
        }

        private void VerificarEstadoJuego()
        {
            if (!_jugador.TieneEstructuras())
            {
                Console.WriteLine("\nHas perdido, todas tus estructuras han sido destruidas");
                MostrarResumenFinal();
                _juegoTerminado = true;
            }
        }

        private void MostrarResumenFinal()
        {
            Console.WriteLine("\n======= RESUMEN FINAL =======");
            Console.WriteLine($"Turnos jugados: {_jugador.TurnosJugados}");
            Console.WriteLine($"Enemigos eliminados: {_jugador.EnemigosEliminados}");
            Console.WriteLine($"Estructuras construidas: {_jugador.Estructuras.Count}");
            Console.WriteLine($"Unidades creadas: {_jugador.Unidades.Count}");
        }
    }
}
