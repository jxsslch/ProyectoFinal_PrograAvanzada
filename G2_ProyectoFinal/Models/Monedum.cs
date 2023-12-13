using System;
using System.Collections.Generic;

namespace G2_ProyectoFinal.Models;

public partial class Monedum
{
    public string Id { get; set; } = null!;

    public string? Nombre { get; set; }

    public virtual ICollection<Transaccione> Transacciones { get; set; } = new List<Transaccione>();
}
