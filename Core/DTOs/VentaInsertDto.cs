﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class VentaInsertDto
    {
        //public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public List<ConceptDto> Concepts { get; set; }
    }
}
