using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secondExam.Module
{
    class Products
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int TypeProductsId { get; set; }
        [Required]
        public int Article { get; set; }
        [Required]
        public decimal MinPriceForPartner { get; set; }
        [Required]
        public TypeProduct typeProduct { get; set; }
        [Required]
        public List<PartnerProducts> PartnerProducts { get; set; }

    }
}
