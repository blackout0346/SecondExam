using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secondExam.Module
{
    internal class TypePartner
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public List<Partner> Partners { get; set; }
        [Required]
        public string NamePartner { get; set; }

     


    }
}
