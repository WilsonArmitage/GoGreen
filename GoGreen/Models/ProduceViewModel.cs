using System;
using System.ComponentModel.DataAnnotations;

namespace GoGreen.Models
{
    public class ProduceViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        [Range(0.0, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal? Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Negative {0} is not supported.")]
        public int? Stock { get; set; }
    }
}
