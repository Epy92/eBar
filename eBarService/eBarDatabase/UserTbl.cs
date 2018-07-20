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
    
    public partial class UserTbl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserTbl()
        {
            this.RestaurantAdministrators = new HashSet<RestaurantAdministrators>();
            this.UserResetPasswordCodes = new HashSet<UserResetPasswordCodes>();
            this.RestaurantFavorite = new HashSet<RestaurantFavorite>();
            this.RestaurantReview = new HashSet<RestaurantReview>();
        }
    
        public int UserID { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int UserTypeId { get; set; }
        public string UserPreferredLanguage { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RestaurantAdministrators> RestaurantAdministrators { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserResetPasswordCodes> UserResetPasswordCodes { get; set; }
        public virtual UserTypes UserTypes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RestaurantFavorite> RestaurantFavorite { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RestaurantReview> RestaurantReview { get; set; }
    }
}
