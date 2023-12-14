using System;
using System.Collections.Generic;

namespace G2_ProyectoFinal.Models
{
    public partial class Transaccione
    {
        public string Id { get; set; } = null!;
        public string? ClienteId { get; set; }
        public string? LenguajeId { get; set; }
        public string? MonedaId { get; set; }
        public string? MetodoPagoId { get; set; }
        public decimal? Monto { get; set; }
        public DateTime? Fecha { get; set; } // Nueva propiedad Fecha de tipo DateTime

        public virtual Cliente? Cliente { get; set; }
        public virtual Lenguaje? Lenguaje { get; set; }
        public virtual MetodoPago? MetodoPago { get; set; }
        public virtual Monedum? Moneda { get; set; }
    }
}
