using System;
using System.Collections.Generic;

namespace G2_ProyectoFinal.Models;

public partial class Canton
{
    public string Id { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? ProvinciaId { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual Provincium? Provincia { get; set; }
}
