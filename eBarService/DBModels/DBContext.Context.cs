﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBModels
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBModels : DbContext
    {
        public DBModels()
            : base("name=DBModels")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Languages> Languages { get; set; }
        public virtual DbSet<RestaurantAdministrators> RestaurantAdministrators { get; set; }
        public virtual DbSet<RestaurantDetails> RestaurantDetails { get; set; }
        public virtual DbSet<RestaurantLocations> RestaurantLocations { get; set; }
        public virtual DbSet<RestaurantProducts> RestaurantProducts { get; set; }
        public virtual DbSet<RestaurantProductsCategories> RestaurantProductsCategories { get; set; }
        public virtual DbSet<Restaurants> Restaurants { get; set; }
        public virtual DbSet<RestaurantTables> RestaurantTables { get; set; }
        public virtual DbSet<RestaurantTypes> RestaurantTypes { get; set; }
        public virtual DbSet<UserResetPasswordCodes> UserResetPasswordCodes { get; set; }
        public virtual DbSet<UserTbl> UserTbl { get; set; }
        public virtual DbSet<UserTypes> UserTypes { get; set; }
        public virtual DbSet<CountiesAndCities> CountiesAndCities { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
