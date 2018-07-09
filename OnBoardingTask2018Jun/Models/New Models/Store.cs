using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

//using OnBoardingTask2018Jun.Models;
// Manually Created by Herb on 19-06-2018
// According to auto-generated class in Entity Framework

namespace OnBoardingTask2018Jun.Models.New_Models
{
    public partial class Store
    {
        // Properties
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }

        // Constructor
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Store()
        {
            this.ProductSolds = new HashSet<ProductSold>();
        }
        // Navigation Properties
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSold> ProductSolds { get; set; }
    }
}