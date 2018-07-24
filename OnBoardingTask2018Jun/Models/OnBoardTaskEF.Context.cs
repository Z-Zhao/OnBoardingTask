// Created by Herb on 19-06-2018
// Modified by Herb on 23-07-2018
namespace OnBoardingTask2018Jun.Models.New_Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    // rename the entity 
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
        public virtual DbSet<Cashier> Cashiers { get; set; }
    }
}
