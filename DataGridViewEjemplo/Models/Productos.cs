using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridViewEjemplo.Models
{
    public class Productos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }
    }
}
