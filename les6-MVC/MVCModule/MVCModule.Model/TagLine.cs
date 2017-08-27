using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCModule.Models
{
    public class TagLine
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
