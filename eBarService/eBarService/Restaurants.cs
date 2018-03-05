//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Runtime.Serialization;
namespace eBarService
{
    using System;
    using System.Collections.Generic;
    
    [DataContract] 
     public  partial class Restaurants
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Restaurants()
        {
            this.RestaurantAdministrators = new HashSet<RestaurantAdministrators>();
            this.RestaurantLocations = new HashSet<RestaurantLocations>();
            this.RestaurantProducts = new HashSet<RestaurantProducts>();
            this.RestaurantProductsCategories = new HashSet<RestaurantProductsCategories>();
            this.RestaurantTables = new HashSet<RestaurantTables>();
            this.RestaurantTypes = new HashSet<RestaurantTypes>();
        }
    
        [DataMember]
    public int RestaurantId { get; set; }
        [DataMember]
    public string RestaurantName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RestaurantAdministrators> RestaurantAdministrators { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RestaurantLocations> RestaurantLocations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RestaurantProducts> RestaurantProducts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RestaurantProductsCategories> RestaurantProductsCategories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RestaurantTables> RestaurantTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RestaurantTypes> RestaurantTypes { get; set; }
    }
}