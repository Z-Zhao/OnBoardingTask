using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
// Manually Created by Herb on 19-06-2018
// According to auto-generated class in Entity Framework

namespace OnBoardingTask2018Jun.Models.New_Models
{
    public partial class Customer
    {
        // Properties
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

        
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.ProductSolds = new HashSet<ProductSold>();
        }
        // Navigation Properties
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSold> ProductSolds { get; set; }
    }
}