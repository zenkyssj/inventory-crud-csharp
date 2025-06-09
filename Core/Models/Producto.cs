using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal PrecioUnitario { get; set; }

    public decimal Costo { get; set; }

    public int IdCategoria { get; set; }

    public virtual ICollection<Concepto> Conceptos { get; set; } = new List<Concepto>();

    public virtual Categorium IdCategoriaNavigation { get; set; } = null!;
}
