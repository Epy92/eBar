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
     public  partial class RestaurantProducts
    {
        [DataMember]
    public int ProductId { get; set; }
        [DataMember]
    public int CategoryId { get; set; }
        [DataMember]
    public string ProductName { get; set; }
        [DataMember]
    public decimal ProductPrice { get; set; }
        [DataMember]
    public string ProductMeasurement { get; set; }
        [DataMember]
    public Nullable<int> ProductMeasurementValue { get; set; }
        [DataMember]
    public string ProductMadeOf { get; set; }
        [DataMember]
    public int RestaurantId { get; set; }
    
        public virtual RestaurantProductsCategories RestaurantProductsCategories { get; set; }
        public virtual Restaurants Restaurants { get; set; }
    }
}
