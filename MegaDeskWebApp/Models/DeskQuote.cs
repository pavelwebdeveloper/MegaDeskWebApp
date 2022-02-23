using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MegaDeskWebApp.Models
{
    public class DeskQuote
    {
        public int ID { get; set; }

        [StringLength(70, MinimumLength = 3)]
        [Required]
        public string CustomerName { get; set; } = string.Empty;

        [Range(24, 96)]
        [Required]
        public int DeskWidth { get; set; }

        [Range(12, 48)]
        [Required]
        public int DeskDepth { get; set; }

        [Range(1, 7)]
        [Required]
        public int NumberOfDrawers { get; set; }

        [Required]
        public string DesktopMaterial { get; set; }

        [Required]
        public int RushOrder { get; set; }

        [Display(Name = "DeskQuote Date")]
        [DataType(DataType.Date)]
        public DateTime DeskQuoteDate { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DeskQuotePrice { get; set; }
    }
    
}
