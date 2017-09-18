using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gee.Q8_Q9.DTO
{
    [DebuggerDisplay("{Name} - {Price} - {IsSoldOut}")]
    public class ProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsSoldOut { get; set; }
    }
}
