using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// Manually added by Herb on 23-07-2018
// according to auto enerated class in Entity Framework

namespace OnBoardingTask2018Jun.Models.New_Models
{
    public partial class Cashier
    {
        // Constructor
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cashier()
        {
            this.ProductSolds = new HashSet<ProductSold>();
        }

        // Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        // Navigation Properties
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSold> ProductSolds { get; set; }
    }
}

