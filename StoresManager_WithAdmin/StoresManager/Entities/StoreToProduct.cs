using StoresManager.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoresManager.Entities
{
    public class StoreToProduct : BaseEntity
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }

        public decimal BasePrice {  get; set; }

        public decimal LoweredPrice { get; set; }

        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
