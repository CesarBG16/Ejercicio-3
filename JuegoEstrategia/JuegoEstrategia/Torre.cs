using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoEstrategia
{
    public class Torre : Estructura
    {
        public int Daño { get; private set; }

        public Torre() : base("Torre", 100, 100, 10)
        {
            Daño = 10;
        }

        public void AtacarEnemigo(IAtacable objetivo)
        {
            if (EstaCompleta && !EstaDestruido)
            {
                objetivo.RecibirDaño(Daño);
                Console.WriteLine($"Torre ataco y causo {Daño} de daño");
            }
        }

        public override string ObtenerDescripcion()
        {
            return $"Torre: Inflige {Daño} de daño por turno" +
                   $"Cuesta {CostoEnergia} de energia y tarda {TurnosConstruccion} turnos en construirse";
        }
    }
}
