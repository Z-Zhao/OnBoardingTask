using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
// Manually Created by Herb on 19-06-2018
// According to auto-generated class in Entity Framework
// Data Annotation and Validation Implemented.


namespace OnBoardingTask2018Jun.Models.New_Models
{
    public partial class Product : IValidatableObject
    {
        // Properties
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Nullable<decimal> Price { get; set; }

        // Constructor
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.ProductSolds = new HashSet<ProductSold>();
        }

        // Navigation Properties
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSold> ProductSolds { get; set; }

        // Property Validation
        public IEnumerable<ValidationResult> Validate(ValidationContext ValiCotext)
        {
            if (Price < 0)
            {// Property Validation 1: Price value should not be negative.
                yield return new ValidationResult("Price value should not be negative", new[] { "Price" });
            }
        }
    }
}