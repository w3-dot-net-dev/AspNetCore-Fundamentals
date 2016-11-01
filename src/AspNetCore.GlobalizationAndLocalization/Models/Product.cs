namespace AspNetCore.GlobalizationAndLocalization.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public Product()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; set; }

        [Display(Name = "Password")]
        [StringLength(8, ErrorMessage = "The {0} - {1} - {2} must be at least {2} characters long.", MinimumLength = 6)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public string Description { get; set; }
    }
}
