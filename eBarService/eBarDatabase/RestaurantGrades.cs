//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eBarDatabase
{
    using System;
    using System.Collections.Generic;
    
    public partial class RestaurantGrades
    {
        public int RestaurantGradeID { get; set; }
        public int UserID { get; set; }
        public int RestaurantId { get; set; }
        public int Grade { get; set; }
    
        public virtual Restaurants Restaurants { get; set; }
        public virtual UserTbl UserTbl { get; set; }
    }
}