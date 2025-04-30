using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoEstrategia
{
    public class Soldado : Unidad
    {
        public Soldado() : base("Soldado", 30, 8)
        {
        }

        public override string ObtenerDescripcion()
        {
            return $"Soldado: Tiene {Vida} puntos de vida y causa {Daño} de daño por ataque" +
                   "Se puede construir gratuitamente si hay una casa vacía luego de 2 turnos";
        }
    }
}
