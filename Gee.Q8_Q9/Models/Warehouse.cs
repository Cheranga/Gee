using System.Collections.Generic;

namespace Gee.Q8_Q9.Models
{
    public class Warehouse
    {
        public int WarehouseId{ get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public List<WarehouseProduct> WarehouseProducts { get; set; }

        public Warehouse()
        {
            WarehouseProducts = new List<WarehouseProduct>();
        }
    }
}