using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class Concepto
{
    public int Id { get; set; }

    public int IdVenta { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal Importe { get; set; }

    public int IdProducto { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Ventum IdVentaNavigation { get; set; } = null!;
}
