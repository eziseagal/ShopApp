using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class ProductChanges
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public decimal PriceBefore { get; set; }
        public decimal PriceAfter { get; set; }
        public int QuantityBefore { get; set; }
        public int QuantityAfter { get; set; }
    }
}
