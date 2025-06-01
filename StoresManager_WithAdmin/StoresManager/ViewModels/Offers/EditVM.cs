using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoresManager.ViewModels.Offers
{
    public class EditVM
    {

        public int Id { get; set; }
        

        [DisplayName("Base Price: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public decimal BasePrice { get; set; }

        [DisplayName("Lowered Price: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public decimal LoweredPrice { get; set; }
    }
}
