using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class Ventum
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public decimal? Total { get; set; }

    public int IdCliente { get; set; }

    public virtual ICollection<Concepto> Conceptos { get; set; } = new List<Concepto>();

    public virtual User IdClienteNavigation { get; set; } = null!;
}
