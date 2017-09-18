using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gee.Q8_Q9.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }

        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
