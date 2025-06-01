using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoresManager.Entities
{
    public class Store : BaseEntity
    {
        
        public int OwnerId { get; set; }
        public string Chain { get; set; }
        public string Address { get; set; }
        public int? ManagerId { get; set; }

        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; }

        [ForeignKey("ManagerId")]
        public virtual User Manager { get; set; }

    }
}
