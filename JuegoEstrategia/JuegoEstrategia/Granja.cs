using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoEstrategia
{
    public class Granja : Estructura
    {
        public int ProduccionEnergia { get; private set; }

        public Granja() : base("Granja", 30, 50, 5)
        {
            ProduccionEnergia = 5;
        }

        public override string ObtenerDescripcion()
        {
            return $"Granja: Produce {ProduccionEnergia} de energia por turno" +
                   $"Cuesta {CostoEnergia} de energia y tarda {TurnosConstruccion} turnos en construirse";
        }
    }
}
