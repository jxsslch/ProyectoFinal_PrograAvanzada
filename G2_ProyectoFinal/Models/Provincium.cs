using System;
using System.Collections.Generic;

namespace G2_ProyectoFinal.Models;

public partial class Provincium
{
    public string Id { get; set; } = null!;

    public string? Nombre { get; set; }

    public virtual ICollection<Canton> Cantons { get; set; } = new List<Canton>();

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
