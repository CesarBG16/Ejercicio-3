using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoEstrategia
{
    public class Arquero : Unidad
    {
        public Arquero() : base("Arquero", 20, 12)
        {
        }

        public override string ObtenerDescripcion()
        {
            return $"Arquero: Tiene {Vida} puntos de vida y causa {Daño} de daño por ataque" +
                   "Cuesta 50 de energia si hay una casa vacia luego de 2 turnos";
        }
    }
}
