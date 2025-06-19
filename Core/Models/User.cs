using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? Rol { get; set; }

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
