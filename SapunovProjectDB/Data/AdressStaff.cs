//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SapunovProjectDB.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class AdressStaff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AdressStaff()
        {
            this.Staff = new HashSet<Staff>();
        }
    
        public int IdAdressStaff { get; set; }
        public string CityNameStaff { get; set; }
        public string StreetNameStaff { get; set; }
        public int HouseNumberStaff { get; set; }
        public int ApartmentNumberStaff { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Staff> Staff { get; set; }
    }
}