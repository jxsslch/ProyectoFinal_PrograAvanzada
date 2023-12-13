using System;
using System.Collections.Generic;

namespace G2_ProyectoFinal.Models;

public partial class Empresa
{
    public string Id { get; set; } = null!;

    public string? Nombre { get; set; }

    public int? NumTelefono { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
