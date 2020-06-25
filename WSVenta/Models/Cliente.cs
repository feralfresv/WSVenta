using System;
using System.Collections.Generic;

namespace WSVenta.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Venta = new HashSet<Venta>();
        }

        public int Id { get; set; }
        public string Cliente1 { get; set; }

        public ICollection<Venta> Venta { get; set; }
    }
}
