using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secondExam.Module
{
    public class Partner
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int TypeNamePartnerId { get; set; }

        [Required] 
        public string NameParntersPlace { get; set; }
        [Required]
        public string DirectionName { get; set; }
        [Required]
        public string number { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string URPartner { get; set; }
        [Required]
        public string INN { get; set; }
        [Required]
        public string Rate { get; set; }
        [Required]
        public TypePartner typePartners { get; set; }
        [Required]
        public List<PartnerProducts> PartnerProducts { get; set; }
    }
}
