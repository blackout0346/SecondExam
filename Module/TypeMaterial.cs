using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secondExam.Module
{
    class TypeMaterial
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string material { get; set; }
        [Required]
        public decimal Break { get; set; }

    }
}
