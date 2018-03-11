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
     public  partial class UserTbl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserTbl()
        {
            this.RestaurantAdministrators = new HashSet<RestaurantAdministrators>();
            this.UserResetPasswordCodes = new HashSet<UserResetPasswordCodes>();
        }
    
        [DataMember]
    public int UserID { get; set; }
        [DataMember]
    public string Username { get; set; }
        [DataMember]
    public string UserPassword { get; set; }
        [DataMember]
    public string Email { get; set; }
        [DataMember]
    public string Name { get; set; }
        [DataMember]
    public int UserTypeId { get; set; }
        [DataMember]
    public string UserPreferredLanguage { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RestaurantAdministrators> RestaurantAdministrators { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserResetPasswordCodes> UserResetPasswordCodes { get; set; }
        public virtual UserTypes UserTypes { get; set; }
    }
}
