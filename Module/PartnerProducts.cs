using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secondExam.Module
{
    public class PartnerProducts
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int ProductsId { get; set; }
        [Required]
        public int PartnersId { get; set; }
        [Required]
        public int CountProducts { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public DateTime DateSale { get; set; }
        [Required]

        public Partner Partner { get; set; }
        [Required]
        public Products Product { get; set; }


    }
}
