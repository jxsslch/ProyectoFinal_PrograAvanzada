using System;
using System.Collections.Generic;

namespace G2_ProyectoFinal.Models;

public partial class MetodoPago
{
    public string Id { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Transaccione> Transacciones { get; set; } = new List<Transaccione>();
}
