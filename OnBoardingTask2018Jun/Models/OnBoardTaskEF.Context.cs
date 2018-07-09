// modify Namespace to new place

namespace OnBoardingTask2018Jun.Models.New_Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    

    // define instance of database (entity framework)
    // can modify default name to other names.
    // in this case, the new name is CustomChangedEntities
    public partial class CustomChangedEntities : DbContext
    {
        public CustomChangedEntities()
            : base("name=OnboardTaskEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductSold> ProductSolds { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
    }
}
