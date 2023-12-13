using System;
using System.Collections.Generic;

namespace G2_ProyectoFinal.Models;

public partial class Cliente
{
    public string Id { get; set; } = null!;

    public int? Cedula { get; set; }

    public string? Nombre { get; set; }

    public string? Email { get; set; }

    public int? NumTelefono { get; set; }

    public string? EmpresaId { get; set; }

    public string? ProvinciaId { get; set; }

    public string? CantonId { get; set; }

    public virtual Canton? Canton { get; set; }

    public virtual Empresa? Empresa { get; set; }

    public virtual Provincium? Provincia { get; set; }

    public virtual ICollection<Transaccione> Transacciones { get; set; } = new List<Transaccione>();
}
