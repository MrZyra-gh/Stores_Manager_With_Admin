using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoresManager.ViewModels.Offers
{
    public class CreateVM
    {
        public int Id { get; set; }

        [DisplayName("StoreId: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public int StoreId { get; set; }

        [DisplayName("ProductId: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public int ProductId { get; set; }

        [DisplayName("Default Price: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public decimal BasePrice { get; set; }

        [DisplayName("Current Price: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public decimal LoweredPrice { get; set; }
    }
}
