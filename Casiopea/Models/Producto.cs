﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Casiopea.Models
{
    class Producto
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Precio { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public string Categoria { get; set; }
    }
}
