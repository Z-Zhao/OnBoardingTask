using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
// Manually Created by Herb on 19-06-2018
// According to auto-generated class in Entity Framework
// Data Annotation and Validation Implemented.
// Updated on 23-07-2018, Add cashier and cashierId

namespace OnBoardingTask2018Jun.Models.New_Models
{
    public partial class ProductSold : IValidatableObject
    {
        // Properties
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int StoreId { get; set; }
        [Required]
        public int CashierId { get; set; }

        [DataType(DataType.Date)] 
        // format datetime to dd/MM/yyyy (without dafault format followed by hh/mm/ss)
        // Nullable datatype (DateTime?), because DateSold could be Null in database.
        public Nullable<DateTime> DateSold { get; set; } 

        // Navigation Properties
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
        public virtual Cashier Cashier { get; set; }

        // Property Validation
        public IEnumerable<ValidationResult> Validate(ValidationContext ValiCotext)
        {
            if (DateSold > DateTime.Now)
            { // Property Validation 1: Sales date can not be future date.
                yield return new ValidationResult("Future date cannot be accepted.", new[] { "DateSold" });
            }
        }
    }
}